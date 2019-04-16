using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fluendo.FluendoPlatform.Infrastructure.Common;

namespace Fluendo.FluendoPlatform.Core.App.Cache
{
    public class RedisCache : IRedisCache
    {
        protected readonly IDistributedCache _distributedCache;

        public RedisCache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<Dictionary<string, object>> GetAsync(string key)
        {
            var result = await _distributedCache.GetAsync(key);

            return (result != null) ? Utils.ParseByteToJson(result) : null;
        }

        public void SetAsync(string key, string content, int timeToLiveInSeconds)
        {
            byte[] encodedResult = Utils.ParseStringToBytes(content);

            var options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(timeToLiveInSeconds));

            _distributedCache.SetAsync(key, encodedResult, options);
        }
    }
}
