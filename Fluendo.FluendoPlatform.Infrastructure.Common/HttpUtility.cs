using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fluendo.FluendoPlatform.Infrastructure.Common
{
    public class HttpUtility<T> : IHttpUtility<T>
    {
        protected HttpClient _httpClient;

        public HttpUtility(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetAsync(string uri)
        {
            using (var result = await _httpClient.GetAsync(uri))
            {
                if (!result.IsSuccessStatusCode)
                    throw new HttpRequestException($"Error in request to {uri} : {result.StatusCode}");

                return JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
