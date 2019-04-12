using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fluendo.FluendoPlatform.Infrastructure.Common
{
    public interface IHttpUtility
    {
        Task<object> GetAsync(Uri uri);
        Task<object> GetAsync(Uri uri, string authorizationKey);
    }
}
