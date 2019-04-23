using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public async Task<ActionResult<HttpResponseMessage>> GetAsync([FromHeader(Name = "Authorization")] string authorizationToken, string gamemode)
        {
            object resultContent = null;

            var uri = new Uri(string.Format(_appOptions.Value.Endpoints["StatsService_Leaderboard"], gamemode));

            var result = await _httpUtility.GetAsync(uri, pugbApiKey);

            resultContent = JsonConvert.DeserializeObject<object>(result.Content.ReadAsStringAsync().Result);

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
                var processLeaderboardRes = _leaderboardService.ProcessLeaderboard(resultContent.ToString());

                if (processLeaderboardRes == Enums.ResultStatus.Failed)
                    return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError));
            }

            return Ok(resultContent);
        }
    }
}
