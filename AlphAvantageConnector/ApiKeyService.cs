using AlphaVantageConnector.Interfaces;

namespace AlphaVantageConnector
{
    /// <summary>
    /// Service for getting of user Api Key.
    /// </summary>
    public class ApiKeyService : IApiKeyService
    {
        private readonly string _apiKey = Properties.Resources.ApiKey;

        public string GetKey()
        {
            return _apiKey;
        }
    }
}