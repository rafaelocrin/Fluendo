using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fluendo.FluendoPlatform.Infrastructure.Common
{
    public interface IHttpUtility
    {
        Task<object> GetAsync(Uri uri);
        Task<HttpResponseMessage> GetAsync(Uri uri, string authorizationKey);
        Task<object> PostAsync(Uri uri, HttpContent httpContent);
        Task<object> PostAsync(Uri uri, string authorizationKey, HttpContent httpContent);
    }
}
