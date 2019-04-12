using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Fluendo.FluendoPlatform.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fluendo.FluendoPlatform.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        protected readonly IHttpUtility _httpUtility;

        public LeaderboardController(IHttpUtility httpUtility)
        {
            _httpUtility = httpUtility;
        }

        // GET api/leaderboard/test
        [HttpGet("{gamemode}")]
        public async Task<ActionResult<object>> GetAsync(string gamemode)
        {
            var uri = new Uri($"http://localhost:51433/api/leaderboard/{gamemode}");

            var result = await _httpUtility.GetAsync(uri);

            return result;
        }
    }
}
