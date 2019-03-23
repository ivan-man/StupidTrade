using AlphaVantageConnector.Helpers;
using AlphaVantageConnector.Enums;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using AlphaVantageConnector.Resources;
using AlphaVantageConnector.Dictionaries;
using System.Linq;
using AlphaVantageConnector.Interfaces;

namespace AlphaVantageConnector
{
    /// <summary>
    /// Compositor of requests.
    /// </summary>
    public class RequestCompositor : IRequestCompositor
    {
        private readonly IApiCallValidator _apiValidator;

        public RequestCompositor(IApiCallValidator apiValidator)
        {
            _apiValidator = apiValidator;
        }

        /// <summary>
        /// Composing of request to API.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="function"></param>
        /// <param name="parameters"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public HttpRequestMessage ComposeHttpRequest(
            string apiKey,
            ApiFunctions function,
            IDictionary<ApiParameters, string> parameters = null,
            HttpMethod httpMethod = null
            )
        {
            var uri = new Uri(ComposeUrl(apiKey, function, parameters));

            var request = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = httpMethod ?? HttpMethod.Get
            };

            return request;
        }

        /// <summary>
        /// Build URL with parameters.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="function"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string ComposeUrl(
            string apiKey,
            ApiFunctions function,
            IDictionary<ApiParameters, string> parameters = null
            )
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentNullException(AvResources.ApiKeyIsNullException);
            }

            if (function == ApiFunctions.Unknown)
            {
                throw new ArgumentException(AvResources.UnknownApiFunctionException);
            }


            var urlParameters = new Dictionary<string, string>
            {
                { ApiParametersDic.GetWord(ApiParameters.Function), function.ToString() }, //func is only first, because validation
                { ApiParametersDic.GetWord(ApiParameters.ApiKey), apiKey }
            };

            if (parameters?.Any() == true)
            {
                foreach (var pair in parameters)
                {
                    urlParameters.Add(ApiParametersDic.GetWord(pair.Key), pair.Value);
                }
            }

            var stringUrl = QueryHelpers.AddQueryString(AlphaVantageConstants.BaseAddress, urlParameters);

            _apiValidator.Validate(stringUrl);

            return stringUrl;
        }
    }
}
