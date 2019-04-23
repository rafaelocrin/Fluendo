using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Fluendo.FluendoPlatform.Core.App.Services
{
    public interface IPlayerStatsService
    {
        Task<HttpResponseMessage> GetAsync(string accountid, string authorizationToken);
        Task<object> GetCacheAsync(string accountid);
        void SetCacheAsync(string accountid, string content);
    }
}
