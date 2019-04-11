using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fluendo.FluendoPlatform.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        // GET api/leaderboard/test
        [HttpGet("{gamemode}")]
        public async Task<ActionResult<string>> GetAsync(string gamemode)
        {
            var client = HttpClientFactory.Create();

            var uri = new Uri($"http://localhost:51433/api/leaderboard/{gamemode}");

            using (var result = await client.GetAsync(uri))
            {
                if (!result.IsSuccessStatusCode)
                    throw new HttpRequestException($"Error in request to {uri} : {result.StatusCode}");

                var ret = JsonConvert.DeserializeObject<string>(result.Content.ReadAsStringAsync().Result);
                

                return ret;
            }

            //return "100 players from a given game mode";
        }
    }
}
