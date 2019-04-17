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
    public class LoginController : Controller
    {
        protected readonly IHttpUtility _httpUtility;

        public LoginController(IHttpUtility httpUtility) {
            _httpUtility = httpUtility;
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<ActionResult<object>> Authenticate([FromHeader(Name = "User")] string user, [FromHeader(Name = "Pasword")] string password)
        {
            var uri = new Uri("http://localhost:50541/api/token");

            var loginModel = new LoginModel()
            {
                Username = user,
                Password = password
            };

            var loginModelJson = JsonConvert.SerializeObject(loginModel);

            var stringContent = new StringContent(loginModelJson, UnicodeEncoding.UTF8, "application/json");

            return await _httpUtility.PostAsync(uri, stringContent);
        }
    }
}