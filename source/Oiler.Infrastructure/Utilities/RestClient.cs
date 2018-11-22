using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;

namespace Oiler.Infrastructure.Utilities
{
    public sealed class RestClient : IRestClient
    {
        private const string JsonHeader = "application/json";
        private static readonly JsonMediaTypeFormatter Formatter;
        private readonly string _baseUrl;

        static RestClient()
        {
            Formatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                }
            };
        }

        public RestClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<RestResponse<TContent>> PostAsync<TRequest, TContent>(TRequest request,
            string uri,
            List<KeyValuePair<string, string>> headers = null,
            TimeSpan? timeout = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            //using (_performanceProfiler.Monitor(string.Format("{0} | {1}", uri, request)))
            {
                return await PostAsyncWithResponseContent<TRequest, TContent>(request,
                    uri,
                    headers,
                    timeout,
                    cancellationToken)
                    .ContinueWith(t => ContinuationFunction(t), cancellationToken);
            }
        }

        public async Task<RestResponse<TContent>> PutAsync<TRequest, TContent>(TRequest request,
            string uri,
            List<KeyValuePair<string, string>> headers = null,
            TimeSpan? timeout = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            //using (_performanceProfiler.Monitor(string.Format("{0} | {1}", uri, request)))
            {
                return await PutAsyncWithResponseContent<TRequest, TContent>(request,
                        uri,
                        headers,
                        timeout,
                        cancellationToken)
                    .ContinueWith(t => ContinuationFunction(t), cancellationToken);
            }
        }

        public async Task<RestResponse> PostAsync<TRequest>(TRequest request,
            string uri,
            List<KeyValuePair<string, string>> headers = null,
            TimeSpan? timeout = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            //using (_performanceProfiler.Monitor(string.Format("{0} | {1}", uri, request)))
            {
                return await PostAsyncWithRestResponse(request, uri, headers, timeout, cancellationToken)
                            .ContinueWith(t => ContinuationFunction(t), cancellationToken);
            }
        }

        public async Task<RestResponse<TContent>> GetAsync<TContent>(string uri,
            List<KeyValuePair<string, string>> headers = null,
            TimeSpan? timeout = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            //using (_performanceProfiler.Monitor(uri))
            {
                return await GetAsyncWithResponseContent<TContent>(uri, headers, timeout, cancellationToken)
                    .ContinueWith(t => ContinuationFunction(t), cancellationToken);
            }
        }

        private async Task<RestResponse<TContent>> PostAsyncWithResponseContent<TRequest, TContent>(TRequest request,
            string uri,
            List<KeyValuePair<string, string>> headers,
            TimeSpan? timeout,
            CancellationToken cancellationToken)
        {
            using (var client = CreateClient(headers, timeout))
            {
                var response = await PostAsyncInternal(request, uri, client, cancellationToken);
                return await GetContentFromResponse<TContent>(response);
            }
        }

        private async Task<RestResponse<TContent>> PutAsyncWithResponseContent<TRequest, TContent>(TRequest request,
            string uri,
            List<KeyValuePair<string, string>> headers,
            TimeSpan? timeout,
            CancellationToken cancellationToken)
        {
            using (var client = CreateClient(headers, timeout))
            {
                var response = await PutAsyncInternal(request, uri, client, cancellationToken);
                return await GetContentFromResponse<TContent>(response);
            }
        }

        private async Task<RestResponse> PostAsyncWithRestResponse<TRequest>(TRequest request,
            string uri,
            List<KeyValuePair<string, string>> headers,
            TimeSpan? timeout,
            CancellationToken cancellationToken)
        {
            using (var client = CreateClient(headers, timeout))
            {
                var response = await PostAsyncInternal(request, uri, client, cancellationToken);
                return new RestResponse
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    Status = response.StatusCode
                };
            }
        }

        private async Task<RestResponse<TContent>> GetAsyncWithResponseContent<TContent>(string uri,
            List<KeyValuePair<string, string>> headers,
            TimeSpan? timeout,
            CancellationToken cancellationToken)
        {
            using (var client = CreateClient(headers, timeout))
            {
                var response = await GetAsyncInternal(uri, client, cancellationToken);
                return await GetContentFromResponse<TContent>(response);
            }
        }

        private async Task<HttpResponseMessage> PostAsyncInternal<TRequest>(TRequest request,
            string uri,
            HttpClient client,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var response =
                await client.PostAsync(uri, request, Formatter, cancellationToken);

            if (IsJsonResponse(response)) return response;

            var errorResponse = await response.Content.ReadAsStringAsync();
            return response;
        }

        private async Task<HttpResponseMessage> PutAsyncInternal<TRequest>(TRequest request,
            string uri,
            HttpClient client,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var response =
                await client.PutAsync(uri, request, Formatter, cancellationToken);

            if (IsJsonResponse(response)) return response;

            var errorResponse = await response.Content.ReadAsStringAsync();
            return response;
        }

        private async Task<HttpResponseMessage> GetAsyncInternal(string uri,
            HttpClient client,
            CancellationToken cancellationToken)
        {
            var response = await client.GetAsync(uri, cancellationToken);

            if (IsJsonResponse(response)) return response;

            var errorResponse = await response.Content.ReadAsStringAsync();
            return response;
        }

        private HttpClient CreateClient(List<KeyValuePair<string, string>> header, TimeSpan? timeout)
        {
            var client = GetClient();
            client.BaseAddress = new Uri(_baseUrl);

            if (timeout.HasValue)
                client.Timeout = timeout.Value;

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonHeader));
            if (header == null) return client;

            foreach (var keyValuePair in header)
                client.DefaultRequestHeaders.Add(keyValuePair.Key, keyValuePair.Value);

            return client;
        }

        private HttpClient GetClient()
        {
            return new HttpClient();
        }

        private bool IsJsonResponse(HttpResponseMessage response)
        {
            return response.Content.Headers.Contains("Content-Type") &&
                   response.Content.Headers.GetValues("Content-Type").First().Contains(JsonHeader);
        }

        private async Task<RestResponse<TContent>> GetContentFromResponse<TContent>(HttpResponseMessage response)
        {
            var result = default(TContent);
            if (IsJsonResponse(response))
                result = await response.Content.ReadAsAsync<TContent>(new[] { Formatter });

            return new RestResponse<TContent>
            {
                IsSuccess = response.IsSuccessStatusCode,
                Status = response.StatusCode,
                Content = result
            };
        }

        private RestResponse<TContent> ContinuationFunction<TContent>(Task<RestResponse<TContent>> task)
        {
            return task.Result;
        }

        private RestResponse ContinuationFunction(Task<RestResponse> task)
        {
            return task.Result;
        }
    }
}