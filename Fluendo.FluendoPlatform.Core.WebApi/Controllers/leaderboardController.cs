using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fluendo.FluendoPlatform.Core.App.Services;
using Fluendo.FluendoPlatform.Infrastructure.Common;
using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Fluendo.FluendoPlatform.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
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
        public async Task<ActionResult<object>> GetAsync(string gamemode)
        {
            return await _leaderboardService.GetAsync(gamemode);
        }

    }
}
