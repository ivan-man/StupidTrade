using Common.Interfaces;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// HttpClient send requests with rate limit.
    /// </summary>
    public class RateLimitHttpClient : HttpClient, IRateLimitHttpClient
    {
        private int _minInterval;//Calculated based on rpm limit in constructor
        private Semaphore _concurrentRequestsSemaphore;
        private object _lockObject = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minInterval">Milliseconds</param>
        /// <param name="maxConcurrentRequests"></param>
        internal RateLimitHttpClient(int minInterval, int maxConcurrentRequests)
        {
            Init(minInterval, maxConcurrentRequests);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minInterval">Milliseconds</param>
        /// <param name="maxConcurrentRequests"></param>
        /// <param name="handler"></param>
        internal RateLimitHttpClient(int minInterval, int maxConcurrentRequests, HttpMessageHandler handler) : base(handler)
        {
            Init(minInterval, maxConcurrentRequests);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minInterval">Milliseconds</param>
        /// <param name="maxConcurrentRequests"></param>
        /// <param name="handler"></param>
        /// <param name="disposeHandler"></param>
        internal RateLimitHttpClient(int minInterval, int maxConcurrentRequests, HttpMessageHandler handler, bool disposeHandler)
            : base(handler, disposeHandler)
        {
            Init(minInterval, maxConcurrentRequests);
        }


        private void Init(int minInterval, int maxConcurrentRequests)
        {
            _minInterval = minInterval;

            _concurrentRequestsSemaphore = new Semaphore(maxConcurrentRequests, maxConcurrentRequests);
        }


        public async Task<HttpResponseMessage> SendWithLimitAsync(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;
            _concurrentRequestsSemaphore.WaitOne();

            try
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                response = await SendAsync(request);
                watch.Stop();

                var elapsedMs = watch.ElapsedMilliseconds;
                if (elapsedMs < _minInterval)
                {
                    await Task.Delay(unchecked((int)(_minInterval - elapsedMs)));
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
            Timeout = timeSpan;
        }
    }
}
