using System;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fluendo.FluendoPlatform.StatsService.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : BaseController, IBaseController
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
        public async Task<ActionResult<object>> GetAsync([FromHeader(Name = "Authorization")] string authorizationToken, string accountId)
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
