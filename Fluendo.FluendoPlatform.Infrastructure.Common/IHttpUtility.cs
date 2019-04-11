using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fluendo.FluendoPlatform.Infrastructure.Common
{
    public interface IHttpUtility<T>
    {
        Task<T> GetAsync(string uri);
    }
}
