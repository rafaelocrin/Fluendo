﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Fluendo.FluendoPlatform.Core.App.Services;
using Fluendo.FluendoPlatform.Infrastructure.Common;
using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Fluendo.FluendoPlatform.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        protected readonly IHttpUtility _httpUtility;
        protected readonly IOptions<ApplicationOptions> _appOptions;
        protected readonly IPlayerStatsService _playerStatsService;

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
            return await _playerStatsService.GetAsync(accountId);
        }
    }
}
