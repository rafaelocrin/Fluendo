using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Fluendo.FluendoPlatform.Infrastructure.Common
{
    public class HttpUtility : IHttpUtility
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        private const string tokenAuthenticationSchema = "Bearer";

        public HttpUtility(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<object> GetAsync(Uri uri)
        {
            object resultRequest;

            try
            {
                var client = _httpClientFactory.CreateClient();

                PrepareRequest(client, null);

                resultRequest = await InvokeGetRequest(client, uri);
            }
            catch (Exception ex)
            {
                // TODO: log exception

                throw new HttpRequestException($"Error in request to {uri}");
            }

            return resultRequest;
        }

        public async Task<HttpResponseMessage> GetAsync(Uri uri, string authorizationKey)
        {
            HttpResponseMessage resultRequest;

            try
            {
                var client = _httpClientFactory.CreateClient();

                PrepareRequest(client, authorizationKey);

                resultRequest = await InvokeGetRequest(client, uri);
            }
            catch (Exception ex)
            {
                // TODO: log exception

                throw new HttpRequestException($"Error in request to {uri}");
            }

            return resultRequest;
        }

        public async Task<object> PostAsync(Uri uri, HttpContent content)
        {
            object resultRequest;

            try
            {
                var client = _httpClientFactory.CreateClient();

                PrepareRequest(client, null);

                resultRequest = await InvokePostRequest(client, uri, content);
            }
            catch (Exception ex)
            {
                // TODO: log exception

                throw new HttpRequestException($"Error in request to {uri}");
            }

            return resultRequest;
        }

        public async Task<object> PostAsync(Uri uri, string authorizationKey, HttpContent content)
        {
            object resultRequest;

            try
            {
                var client = _httpClientFactory.CreateClient();

                PrepareRequest(client, authorizationKey);

                resultRequest = await InvokePostRequest(client, uri, content);
            }
            catch (Exception ex)
            {
                // TODO: log exception

                throw new HttpRequestException($"Error in request to {uri}");
            }

            return resultRequest;
        }

        private void PrepareRequest(HttpClient httpClient, string authorizationKey)
        {
            if (authorizationKey != null)
            {
                var authSchema = (authorizationKey.IndexOf(tokenAuthenticationSchema) >= 0) ? tokenAuthenticationSchema : string.Empty;

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authSchema, authorizationKey.Split(" ")[1]);
            }

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.api+json"));
        }

        private async Task<HttpResponseMessage> InvokeGetRequest(HttpClient httClient, Uri uri)
        {
            var result = await httClient.GetAsync(uri);

            return result;
        }

        private async Task<object> InvokePostRequest(HttpClient httClient, Uri uri, HttpContent content)
        {
            using (var result = await httClient.PostAsync(uri, content))
            {
                if (!result.IsSuccessStatusCode)
                    throw new HttpRequestException($"Error in request to {uri} : {result.StatusCode}");

                return JsonConvert.DeserializeObject<object>(result.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
