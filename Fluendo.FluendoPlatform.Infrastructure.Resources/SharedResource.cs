using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fluendo.FluendoPlatform.Infrastructure.Resources
{
    //public class FluendoResource : IFluendoResource
    //{
    //    private readonly IStringLocalizer<FluendoResource> _localizer;

    //    public FluendoResource(IStringLocalizer<FluendoResource> localizer) =>
    //        _localizer = localizer;

    //    public string Error_Repository_Update_Leaderboard => GetString(nameof(Error_Repository_Update_Leaderboard));

    //    private string GetString(string name) =>
    //        _localizer[name];
    //}


    public class SharedResource : ISharedResource
    {
        private readonly IStringLocalizer _localizer;

        public SharedResource(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }

        public string this[string index]
        {
            get
            {
                return _localizer[index];
            }
        }
    }
}
