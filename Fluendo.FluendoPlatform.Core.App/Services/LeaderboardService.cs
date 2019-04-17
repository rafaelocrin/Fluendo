using Fluendo.FluendoPlatform.Core.App.Cache;
using Fluendo.FluendoPlatform.Infrastructure.Common;
using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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

        public async Task<ActionResult<object>> GetAsync(string gamemode)
        {
            object result = null;
            var cacheKey = string.Format(_appOptions.Value.RedisCache["CacheKey_Leaderboard"], gamemode);

            var leaderboardCached = await _redisCache.GetAsync(cacheKey);

            if (leaderboardCached != null)
            {
                result = leaderboardCached;
            }
            else
            {
                var uri = new Uri(string.Format(_appOptions.Value.Endpoints["Core_Leaderboard"], gamemode));

                result = await _httpUtility.GetAsync(uri);

                var cacheTTL = _appOptions.Value.RedisCache["CacheTTL"];

                _redisCache.SetAsync(cacheKey, result.ToString(), Convert.ToInt32(cacheTTL));
            }

            return result;
        }
    }
}
