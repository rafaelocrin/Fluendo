﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Fluendo.FluendoPlatform.Infrastructure.Common;
using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Fluendo.FluendoPlatform.StatsService.Persistence;
using Fluendo.FluendoPlatform.StatsService.Persistence.Repositories;
using Fluendo.FluendoPlatform.StatsService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fluendo.FluendoPlatform.StatsService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        protected readonly IHttpUtility _httpUtility;
        protected readonly IOptions<ApplicationOptions> _appOptions;
        protected readonly IPlayerStatsService _playerStatsService;

        private const string pugbApiKey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiIxMWU5NDY2MC0zZTY0LTAxMzctYTUxNC0wNzQyYmM5YmRiYzAiLCJpc3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNTU0OTcyMzIzLCJwdWIiOiJibHVlaG9sZSIsInRpdGxlIjoicHViZyIsImFwcCI6Ii0wYjhhMzkyZC1lNjQ1LTRlYzktYTJiNC0yNmMyMmJmN2VmNDkifQ.y6A5spdAMmgl44reEEhj6i27k8v299wUI57PM0Da0NE";

        public PlayerController(IOptions<ApplicationOptions> appOptions, IHttpUtility httpUtility, IPlayerStatsService playerStatsService)
        {
            _httpUtility = httpUtility;
            _appOptions = appOptions;
            _playerStatsService = playerStatsService;
        }

        // GET api/player/{accountId}/seasons/lifetime"
        [HttpGet("{accountId}/seasons/lifetime")]
        public async Task<ActionResult<object>> GetAsync(string accountId)
        {
            var uri = new Uri(string.Format(_appOptions.Value.Endpoints["StatsService_PlayerLifetime"], accountId));

            var result = await _httpUtility.GetAsync(uri, pugbApiKey);

            var processPlayerStatsRes = _playerStatsService.ProcessPlayerStats(result.ToString());

            if (processPlayerStatsRes == Enums.ResultStatus.Failed)
                result = "Error trying to process PlayersStats Results";

            return result;
        }
    }
}