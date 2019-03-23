using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class HttpClientWithRateLimit : IDisposable
    {
        private HttpClient _client;
        private TimeSpan _minRequestInterval;//Calculated based on rpm limit in constructor
        private Semaphore _concurrentRequestsSemaphore;
        private object _lockObject = new object();

        internal HttpClientWithRateLimit(HttpClient client, int maxRequestPerMinutes, int maxConcurrentRequests)
        {
            _client = client;
            _minRequestInterval = new TimeSpan(0, 0, 0, 0, 60000 / maxRequestPerMinutes);
            _concurrentRequestsSemaphore = new Semaphore(maxConcurrentRequests, maxConcurrentRequests);
        }

        public void Dispose()
        {
            _concurrentRequestsSemaphore.Dispose();
        }


        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;
            _concurrentRequestsSemaphore.WaitOne();
            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                response = await _client.SendAsync(request);
                watch.Stop();

                var elapsedMs = watch.ElapsedMilliseconds;
                if (elapsedMs < _minRequestInterval.Milliseconds)
                {
                    await Task.Delay(unchecked((int)(_minRequestInterval.Milliseconds - elapsedMs)));
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _concurrentRequestsSemaphore.Release();
            }

            return response;
        }
       
        public void SetTimeOut(TimeSpan timeSpan)
        {
            _client.Timeout = timeSpan;
        }
    }
}
