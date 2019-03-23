using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace Common
{
    /// <summary>
    /// Distribution of HttpClient
    /// </summary>
    public class HttpClientManager
    {
        private Dictionary<string, HttpClient> _clients = new Dictionary<string, HttpClient>();

        private static ReaderWriterLockSlim _locking = new ReaderWriterLockSlim();

        public HttpClient GetClient(string baseUrl)
        {
            _locking.EnterReadLock();

            if (!_clients.TryGetValue(baseUrl, out var client))
            {
                _locking.ExitReadLock();

                _locking.EnterWriteLock();

                client = new HttpClient
                {
                    BaseAddress = new Uri(baseUrl)
                };

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                _clients.Add(baseUrl, client);

                _locking.ExitWriteLock();
            }
            else
            {
                _locking.ExitReadLock();
            }

            return client;
        }
    }
}
