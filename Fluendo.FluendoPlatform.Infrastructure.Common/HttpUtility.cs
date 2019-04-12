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

        public HttpUtility(IHttpClientFactory htppClientFactory)
        {
            _httpClientFactory = htppClientFactory;
        }


        public async Task<object> GetAsync(Uri uri)
        {
            object resultRequest;

            try
            {
                var client = _httpClientFactory.CreateClient();

                PrepareRequest(client, null);

                resultRequest = await InvokeRequest(client, uri);
            }
            catch (Exception ex)
            {
                // TODO: log exception

                throw new HttpRequestException($"Error in request to {uri}");
            }

            return resultRequest;
        }

        public async Task<object> GetAsync(Uri uri, string authorizationKey)
        {
            object resultRequest;

            try
            {
                var client = _httpClientFactory.CreateClient();

                PrepareRequest(client, authorizationKey);

                resultRequest = await InvokeRequest(client, uri);
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
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationKey);

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.api+json"));
        }

        private async Task<object> InvokeRequest(HttpClient httClient, Uri uri)
        {
            using (var result = await httClient.GetAsync(uri))
            {

                if (!result.IsSuccessStatusCode)
                    throw new HttpRequestException($"Error in request to {uri} : {result.StatusCode}");

                return JsonConvert.DeserializeObject<object>(result.Content.ReadAsStringAsync().Result);
            }
        }

        //public async Task<ResultRequest<T>> GetAsync(string uri)
        //{
        //    var resulRequest = new ResultRequest<T>();

        //    try
        //    {
        //        var client = _httpClientFactory.CreateClient();

        //        using (var result = await client.GetAsync(uri))
        //        {
        //            if (!result.IsSuccessStatusCode)
        //            {
        //                //if (resulRequest.Error == null)
        //                //{
        //                //    resulRequest.Error = new Error
        //                //    {
        //                //        ErrorCode = result.StatusCode.ToString()
        //                //    };
        //                //}
        //                //else
        //                //{
        //                    resulRequest.Error?.ErrorCode = result.StatusCode.ToString();
        //                //}
        //            }
        //                //throw new HttpRequestException($"Error in request to {uri} : {result.StatusCode}");

        //            //var ret = JsonConvert.DeserializeObject<string>(result.Content.ReadAsStringAsync().Result);



        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return resulRequest;
        //}
    }
}
