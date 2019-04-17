using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fluendo.FluendoPlatform.Core.App.Services
{
    public interface ILeaderboardService
    {
        Task<ActionResult<object>> GetAsync(string gamemode, string authHeader);
    }
}
