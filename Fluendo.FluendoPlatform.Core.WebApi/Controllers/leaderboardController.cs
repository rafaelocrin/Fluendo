using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Fluendo.FluendoPlatform.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        // GET api/leaderboard/test
        [HttpGet("{gamemode}")]
        public ActionResult<string> Get(string gamemode)
        {
            return "100 players from a given game mode";
        }
    }
}
