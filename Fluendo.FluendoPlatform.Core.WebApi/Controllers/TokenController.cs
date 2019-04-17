using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fluendo.FluendoPlatform.Infrastructure.Common;
using Fluendo.FluendoPlatform.Infrastructure.Common.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Fluendo.FluendoPlatform.Core.WebApi.Controllers
{
    public class TokenController : Controller
    {
        protected readonly IHttpUtility _httpUtility;
        protected readonly IOptions<ApplicationOptions> _appOptions;

        public TokenController(IOptions<ApplicationOptions> appOptions, IHttpUtility httpUtility) {
            _httpUtility = httpUtility;
            _appOptions = appOptions;
        }

        [HttpPost]
        [Route("generate")]
        public async Task<ActionResult<object>> GenerateToken()
        {
            var uri = new Uri(_appOptions.Value.Endpoints["Core_Token"]);

            var stringContent = new StringContent(string.Empty, UnicodeEncoding.UTF8, "application/json");

            return await _httpUtility.PostAsync(uri, stringContent);
        }
    }
}