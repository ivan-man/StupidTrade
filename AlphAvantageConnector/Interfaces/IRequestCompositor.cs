using AlphaVantageConnector.Enums;
using System.Collections.Generic;
using System.Net.Http;

namespace AlphaVantageConnector.Interfaces
{
    public interface IRequestCompositor
    {
        HttpRequestMessage ComposeHttpRequest(
            string apiKey,
            ApiFunctions function,
            IDictionary<ApiParameters, string> parameters = null,
            HttpMethod httpMethod = null
            );

        string ComposeUrl(
            string apiKey,
            ApiFunctions function,
            IDictionary<ApiParameters, string> parameters = null
            );
    }
}
