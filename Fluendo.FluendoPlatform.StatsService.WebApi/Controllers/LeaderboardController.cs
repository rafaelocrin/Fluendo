using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Fluendo.FluendoPlatform.StatsService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        // GET api/leaderboard/test
        [HttpGet("{gamemode}")]
        public ActionResult<string> Get(string gamemode)
        {
            // TODO: Call to PUGB API
            return "100 players from a given game mode";
        }
    }
}
