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

        public async Task<ActionResult<object>> GetAsync(string gamemode)
        {
            object result = null;
            var cacheKey = _appOptions.Value.RedisCache["CacheKey_PlayerStats"];
            var cacheTTL = _appOptions.Value.RedisCache["CacheTTL"];

            var leaderboardCached = _redisCache.GetAsync(cacheKey);

            if (leaderboardCached != null)
            {
                result = leaderboardCached;
            }
            else
            {
                var uri = new Uri(string.Format(_appOptions.Value.Endpoints["Core_PlayerLifetime"], gamemode));

                result = await _httpUtility.GetAsync(uri);

                _redisCache.SetAsync(cacheKey, result.ToString(), Convert.ToInt32(cacheTTL));
            }

            return result;
        }
    }
}
