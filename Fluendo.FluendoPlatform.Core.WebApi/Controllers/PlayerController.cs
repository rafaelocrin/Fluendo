using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Fluendo.FluendoPlatform.Core.App.Services;
using Fluendo.FluendoPlatform.Infrastructure.Common;
using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Fluendo.FluendoPlatform.Core.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        protected readonly IPlayerStatsService _playerStatsService;

        public PlayerController(IPlayerStatsService playerStatsService)
        {
            _playerStatsService = playerStatsService;
        }

        // GET api/player/{accountId}/seasons/lifetime"
        [HttpGet("{accountId}/seasons/lifetime")]
        public async Task<ActionResult<object>> GetAsync([FromHeader(Name = "Authorization")] string authorizationToken, string accountId)
        {
            object resultContent = null;

            var playerstatsCached = await _playerStatsService.GetCacheAsync(accountId);

            if (playerstatsCached != null)
            {
                resultContent = playerstatsCached;
            }
            else
            {
                var result = await _playerStatsService.GetAsync(accountId, authorizationToken);

                resultContent = JsonConvert.DeserializeObject<object>(result.Content.ReadAsStringAsync().Result);

                if (result.IsSuccessStatusCode)
                {
                    _playerStatsService.SetCacheAsync(accountId, resultContent.ToString());
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
                    _playerStatsService.SetCacheAsync(accountId, resultContent.ToString());
                }
            }

            return Ok(resultContent);
        }
    }
}
