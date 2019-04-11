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
    public class PlayerController : ControllerBase
    {
        // accountId: account.d50fdc18fcad49c691d38466bed6f8fd
        // GET api/player/{accountId}/seasons/lifetime"
        [HttpGet("stats/{accountId}")]
        public async Task<ActionResult<string>> GetAsync(string accountId)
        {
            var client = HttpClientFactory.Create();

            var uri = new Uri($"http://localhost:51433/api/player/{accountId}/seasons/lifetime");

            using (var result = await client.GetAsync(uri))
            {
                if (!result.IsSuccessStatusCode)
                    throw new HttpRequestException($"Error in request to {uri} : {result.StatusCode}");

                var ret = JsonConvert.DeserializeObject<string>(result.Content.ReadAsStringAsync().Result);
                

                return ret;
            }
        }
    }
}
