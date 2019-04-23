using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fluendo.FluendoPlatform.StatsService.WebApi.Controllers
{
    public interface IBaseController
    {
        Task<ActionResult<HttpResponseMessage>> GetAsync([FromHeader(Name = "Authorization")] string authHeader, string gamemode);
    }
}
