using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fluendo.FluendoPlatform.Core.App.Services;
using Fluendo.FluendoPlatform.Infrastructure.Common;
using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Fluendo.FluendoPlatform.Core.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        protected readonly ILeaderboardService _leaderboardService;
        private readonly ILogger _logger;

        public LeaderboardController(ILeaderboardService leaderboardService, ILogger<LeaderboardController> logger)
        {
            _leaderboardService = leaderboardService;
            _logger = logger;
        }

        // GET api/leaderboard/test
        [HttpGet("{gameMode}")]
        public async Task<ActionResult<HttpResponseMessage>> GetAsync([FromHeader(Name = "Authorization")] string authorizationToken, string gameMode)
        {
            object resultContent = null;

            var leaderboardCached = await _leaderboardService.GetCacheAsync(gameMode);

            if (leaderboardCached != null)
            {
                resultContent = leaderboardCached;
            }
            else
            {
                var result = await  _leaderboardService.GetAsync(gameMode, authorizationToken);

                resultContent = JsonConvert.DeserializeObject<object>(result.Content.ReadAsStringAsync().Result);

                if (result.IsSuccessStatusCode)
                {
                    _leaderboardService.SetCacheAsync(gameMode, resultContent.ToString());
                }

                if (!result.IsSuccessStatusCode)
                {
                    switch (result.StatusCode)
                    {
                        case HttpStatusCode.NoContent:
                            return NoContent();
                        case HttpStatusCode.NotFound:
                            return NotFound();
                    }
                }
                else
                {
                    _leaderboardService.SetCacheAsync(gameMode, resultContent.ToString());
                }
            }

            return Ok(resultContent);
        }
    }
}
