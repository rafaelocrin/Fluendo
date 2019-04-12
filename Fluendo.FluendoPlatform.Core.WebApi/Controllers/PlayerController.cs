﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Fluendo.FluendoPlatform.Infrastructure.Common;
using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Fluendo.FluendoPlatform.Core.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        protected readonly IHttpUtility _httpUtility;
        protected readonly IOptions<ApplicationOptions> _appOptions;

        public PlayerController(IOptions<ApplicationOptions> appOptions, IHttpUtility httpUtility)
        {
            _httpUtility = httpUtility;
            _appOptions = appOptions;
        }

        // GET api/player/{accountId}/seasons/lifetime"
        [HttpGet("{accountId}/seasons/lifetime")]
        public async Task<ActionResult<object>> GetAsync(string accountId)
        {
            var uri = new Uri(string.Format(_appOptions.Value.Endpoints["Core_PlayerLifetime"], accountId));

            var result = await _httpUtility.GetAsync(uri);

            return result;
        }
    }
}
