using System.Net.Http;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IRateLimitHttpClient
    {
        Task<HttpResponseMessage> SendWithLimitAsync(HttpRequestMessage request);
    }
}
