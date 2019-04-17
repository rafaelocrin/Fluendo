using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Fluendo.FluendoPlatform.Infrastructure.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Fluendo.FluendoPlatform.Infrastructure.Authentication.Controllers.TokenController;

namespace Fluendo.FluendoPlatform.Core.WebApi.Controllers
{
    public class TokenController : Controller
    {
        protected readonly IHttpUtility _httpUtility;

        public TokenController(IHttpUtility httpUtility) {
            _httpUtility = httpUtility;
        }

        [HttpPost]
        [Route("generate")]
        public async Task<ActionResult<object>> GenerateToken()
        {
            var uri = new Uri("http://localhost:50541/api/token");

            var stringContent = new StringContent(string.Empty, UnicodeEncoding.UTF8, "application/json");

            return await _httpUtility.PostAsync(uri, stringContent);
        }
    }
}