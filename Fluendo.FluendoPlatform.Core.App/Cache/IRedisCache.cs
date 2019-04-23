using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fluendo.FluendoPlatform.Core.App.Cache
{
    public interface IRedisCache
    {
        Task<object> GetAsync(string key);

        void SetAsync(string key, string content, int timeToLiveInSeconds);
    }
}
