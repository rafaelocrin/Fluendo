using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fluendo.FluendoPlatform.Core.App.Services
{
    public interface ILeaderboardService
    {
        Task<HttpResponseMessage> GetAsync(string gamemode, string authorizationToken);
        Task<object> GetCacheAsync(string gamemode);
        void SetCacheAsync(string gameMode, string content);
    }
}
