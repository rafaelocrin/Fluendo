using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fluendo.FluendoPlatform.StatsService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardController : ControllerBase
    {
        // GET api/leaderboard/test
        [HttpGet("{gamemode}")]
        public async Task<ActionResult<object>> GetAsync(string gamemode)
        {
            // TODO: Call to PUGB API

            var client = HttpClientFactory.Create();

            var uri = new Uri($"https://api.pubg.com/shards/steam/leaderboards/{gamemode}");
            client.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiIxMWU5NDY2MC0zZTY0LTAxMzctYTUxNC0wNzQyYmM5YmRiYzAiLCJpc3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNTU0OTcyMzIzLCJwdWIiOiJibHVlaG9sZSIsInRpdGxlIjoicHViZyIsImFwcCI6Ii0wYjhhMzkyZC1lNjQ1LTRlYzktYTJiNC0yNmMyMmJmN2VmNDkifQ.y6A5spdAMmgl44reEEhj6i27k8v299wUI57PM0Da0NE");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.api+json"));

            using (var result = await client.GetAsync(uri))
            {
                if (!result.IsSuccessStatusCode)
                    throw new HttpRequestException($"Error in request to {uri} : {result.StatusCode}");

                var ret = JsonConvert.DeserializeObject<object>(result.Content.ReadAsStringAsync().Result);


                return ret;
            }
        }
    }
}
