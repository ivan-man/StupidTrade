
namespace AlphaVantageConnector.Interfaces
{
    /// <summary>
    /// Service for getting of user Api Key.
    /// </summary>
    public interface IApiKeyService
    {
        /// <summary>
        /// Get key.
        /// </summary>
        /// <returns></returns>
        string GetKey();
    }
}
