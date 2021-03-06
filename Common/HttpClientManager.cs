﻿using Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace Common
{
    /// <summary>
    /// Управление соединениями, с возможностью настройки каждого, 
    /// чтобы не попадать под ограничения демо-версий.
    /// </summary>
    public class HttpClientManager
    {
        private static ConcurrentDictionary<string, HttpClient> _clients = new ConcurrentDictionary<string, HttpClient>();

        private static ReaderWriterLockSlim _locking = new ReaderWriterLockSlim();

        public static IRateLimitHttpClient GetRateLimitClient(string baseUrl, int rateLimit, int maxConcurrentRequests)
        {
            return BuildClient(baseUrl, rateLimit, maxConcurrentRequests) as IRateLimitHttpClient;
        }

        public HttpClient GetClient(string baseUrl)
        {
            return BuildClient(baseUrl);
        }

        private static HttpClient BuildClient(string baseUrl, int rateLimit = 0, int maxConcurrentRequests = 0)
        {
            _locking.EnterReadLock();

            if (!_clients.TryGetValue(baseUrl, out var client))
            {
                _locking.ExitReadLock();

                _locking.EnterWriteLock();

                client = rateLimit > 0
                    ? new RateLimitHttpClient(rateLimit, maxConcurrentRequests) { BaseAddress = new Uri(baseUrl) }
                    : new HttpClient { BaseAddress = new Uri(baseUrl) };

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                _clients.TryAdd(baseUrl, client);

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
