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
using Fluendo.FluendoPlatform.StatsService.Persistence.Repositories;
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
    public class PlayerController : BaseController, IBaseController
    {
        protected readonly IHttpUtility _httpUtility;
        protected readonly IOptions<ApplicationOptions> _appOptions;
        protected readonly IPlayerStatsService _playerStatsService;

        public PlayerController(IOptions<ApplicationOptions> appOptions, IHttpUtility httpUtility, IPlayerStatsService playerStatsService)
        {
            _httpUtility = httpUtility;
            _appOptions = appOptions;
            _playerStatsService = playerStatsService;
        }

        // GET api/player/{accountId}/seasons/lifetime"
        [HttpGet("{accountId}/seasons/lifetime")]
        public async Task<ActionResult<HttpResponseMessage>> GetAsync([FromHeader(Name = "Authorization")] string authorizationToken, string accountId)
        {
            object resultContent = null;

            var uri = new Uri(string.Format(_appOptions.Value.Endpoints["StatsService_PlayerLifetime"], accountId));

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
                var processPlayerStatsRes = _playerStatsService.ProcessPlayerStats(resultContent.ToString());

                if (processPlayerStatsRes == Enums.ResultStatus.Failed)
                    return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError));
            }

            return Ok(resultContent);
        }
    }
}
