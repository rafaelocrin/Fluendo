using Fluendo.FluendoPlatform.Core.App.Cache;
using Fluendo.FluendoPlatform.Infrastructure.Common;
using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Fluendo.FluendoPlatform.Core.App.Services
{
    public class LeaderboardService : ILeaderboardService
    {
        protected readonly IRedisCache _redisCache;
        protected readonly IOptions<ApplicationOptions> _appOptions;
        protected readonly IHttpUtility _httpUtility;

        public LeaderboardService(IRedisCache redisCache, IOptions<ApplicationOptions> appOptions, IHttpUtility httpUtility)
        {
            _redisCache = redisCache;
            _appOptions = appOptions;
            _httpUtility = httpUtility;
        }

        public async Task<HttpResponseMessage> GetAsync(string gamemode, string authorizationToken)
        {
            var uri = new Uri(string.Format(_appOptions.Value.Endpoints["Core_Leaderboard"], gamemode));

            return await _httpUtility.GetAsync(uri, authorizationToken);
        }

        public async Task<object> GetCacheAsync(string gamemode)
        {
            var cacheKey = string.Format(_appOptions.Value.RedisCache["CacheKey_Leaderboard"], gamemode);

            return await _redisCache.GetAsync(cacheKey);
        }

        public void SetCacheAsync(string gameMode, string content)
        {
            var cacheKey = string.Format(_appOptions.Value.RedisCache["CacheKey_Leaderboard"], gameMode);
            var cacheTTL = _appOptions.Value.RedisCache["CacheTTL"];

            _redisCache.SetAsync(cacheKey, content, Convert.ToInt32(cacheTTL));
        }
    }
}
