using Fluendo.FluendoPlatform.Core.App.Cache;
using Fluendo.FluendoPlatform.Infrastructure.Common;
using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fluendo.FluendoPlatform.Core.App.Services
{
    public class PlayerStatsService : IPlayerStatsService
    {
        protected readonly IRedisCache _redisCache;
        protected readonly IOptions<ApplicationOptions> _appOptions;
        protected readonly IHttpUtility _httpUtility;

        public PlayerStatsService(IRedisCache redisCache, IOptions<ApplicationOptions> appOptions, IHttpUtility httpUtility)
        {
            _redisCache = redisCache;
            _appOptions = appOptions;
            _httpUtility = httpUtility;
        }

        public async Task<HttpResponseMessage> GetAsync(string accountid, string authorizationToken)
        {
            var uri = new Uri(string.Format(_appOptions.Value.Endpoints["Core_PlayerLifetime"], accountid));

            return await _httpUtility.GetAsync(uri, authorizationToken);
        }

        public async Task<object> GetCacheAsync(string accountid)
        {
            var cacheKey = string.Format(_appOptions.Value.RedisCache["CacheKey_PlayerStats"], accountid);

            return await _redisCache.GetAsync(cacheKey);
        }

        public void SetCacheAsync(string accountid, string content)
        {
            var cacheKey = string.Format(_appOptions.Value.RedisCache["CacheKey_PlayerStats"], accountid);
            var cacheTTL = _appOptions.Value.RedisCache["CacheTTL"];

            _redisCache.SetAsync(cacheKey, content, Convert.ToInt32(cacheTTL));
        }
    }
}
