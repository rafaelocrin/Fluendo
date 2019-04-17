using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Fluendo.FluendoPlatform.Infrastructure.Common;
using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Fluendo.FluendoPlatform.StatsService.Persistence;
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
    public class LeaderboardController : BaseController, IBaseController
    {
        protected readonly IHttpUtility _httpUtility;
        protected readonly IOptions<ApplicationOptions> _appOptions;
        protected readonly ILeaderboardService _leaderboardService;

        public LeaderboardController(IOptions<ApplicationOptions> appOptions, IHttpUtility httpUtility, ILeaderboardService leaderboardService)
        {
            _httpUtility = httpUtility;
            _appOptions = appOptions;
            _leaderboardService = leaderboardService;
        }

        // GET api/leaderboard/test
        [HttpGet("{gamemode}")]
        public async Task<ActionResult<object>> GetAsync([FromHeader(Name = "Authorization")] string authorizationToken, string gamemode)
        {
            var uri = new Uri(string.Format(_appOptions.Value.Endpoints["StatsService_Leaderboard"], gamemode));

            var result = await _httpUtility.GetAsync(uri, pugbApiKey);

            var processLeaderboardRes = _leaderboardService.ProcessLeaderboard(result.ToString());

            if (processLeaderboardRes == Enums.ResultStatus.Failed)
                result = "Error trying to process Leaderboard Results";

            return result;
        }
    }
}
