using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Oiler.Infrastructure.Utilities
{
    public interface IRestClient
    {
        Task<RestResponse<TContent>> PostAsync<TRequest, TContent>(TRequest request,
            string uri,
            List<KeyValuePair<string, string>> headers = null,
            TimeSpan? timeout = null,
            CancellationToken cancellationToken = new CancellationToken());

        Task<RestResponse<TContent>> PutAsync<TRequest, TContent>(TRequest request,
            string uri,
            List<KeyValuePair<string, string>> headers = null,
            TimeSpan? timeout = null,
            CancellationToken cancellationToken = new CancellationToken());

        Task<RestResponse> PostAsync<TRequest>(TRequest request,
            string uri,
            List<KeyValuePair<string, string>> headers = null,
            TimeSpan? timeout = null,
            CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="headers"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="timeout">Timeout in milliseconds to be used for request. This timeout value overrides the value set by HttpClient</param>
        /// <returns></returns>
        Task<RestResponse<TContent>> GetAsync<TContent>(string uri,
            List<KeyValuePair<string, string>> headers = null,
            TimeSpan? timeout = null,
            CancellationToken cancellationToken = new CancellationToken());
    }
}
