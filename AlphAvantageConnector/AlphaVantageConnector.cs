using Common;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using AlphaVantageConnector.Enums;
using Newtonsoft.Json;
using System.Net.Http;
using AlphaVantageConnector.Interfaces;
using AlphaVantageConnector.Resources;
using AlphaVantageConnector.Helpers;
using AlphaVantageDto;
using System.Linq;

namespace AlphaVantageConnector
{
    /// <summary>
    /// Core of requests to API.
    /// </summary>
    public class AlphaVantageConnector : IAlphaVantageConnector
    {
        private readonly IApiKeyService _apiKeyService;
        private readonly IRequestCompositor _requestCompositor;

        /// <summary>
        /// One for all to save sockets.
        /// </summary>
        private readonly HttpClient _apiHttpClient;

        public AlphaVantageConnector(
            IApiKeyService apiKeyService,
            IRequestCompositor requestCompositor,
            HttpClient httpClient
            )
        {
            _apiHttpClient = httpClient;

            _apiKeyService = apiKeyService;
            _requestCompositor = requestCompositor;
        }


        public async Task<(MetaData MetaData, TData Data)> RequestApiAsync<TData>(ApiFunctions function, IDictionary<ApiParameters, string> parameters = null)
        {
            var request = _requestCompositor.ComposeHttpRequest(_apiKeyService.GetKey(), function, parameters);
            var response = await _apiHttpClient.SendAsync(request);

            var jsonString = await response.Content.ReadAsStringAsync();

            jsonString = PreDeserializationHelper.ClearResponse(jsonString);

            var jObject = (JObject)JsonConvert.DeserializeObject(jsonString);

            MetaData metaData = null;

            if (jObject.ContainsKey(AvResources.MetaDataHeader))
            {
                var metaDataJT = jObject[AvResources.MetaDataHeader];
                metaData = metaDataJT.ToObject<MetaData>();
            }

            AssertNotBadRequest(jObject);

            JToken jData = null;

            var properties = jObject.Children().Select(ch => (JProperty)ch).ToArray();

            if (properties.Length > 1)
            {
                jData = properties[1].Value;
            }
            else if (properties?.Any() == true)
            {
                jData = jObject;
            }

            try
            {
                var data = jData.ToObject<TData>();
                return (metaData, data);
            }
            catch (Exception e)
            {
                throw;
            }
        }


        /// <summary>
        /// Check responce from API.
        /// </summary>
        /// <param name="jObject"></param>
        private void AssertNotBadRequest(JObject jObject)
        {
            if (jObject.ContainsKey(AvResources.BadRequestToken))
            {
                throw new Exception(jObject[AvResources.BadRequestToken].ToString());
            }
        }
    }
}
