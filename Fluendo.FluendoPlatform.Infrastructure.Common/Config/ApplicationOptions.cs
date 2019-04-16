using System;
using System.Collections.Generic;
using System.Text;

namespace Fluendo.FluendoPlatform.Infrastructure.Common.Config
{
    public class ApplicationOptions
    {
        public IDictionary<string, string> ConnectionString { get; set; } = new Dictionary<string, string>();
        public IDictionary<string, string> Endpoints { get; set; } = new Dictionary<string, string>();
        public IDictionary<string, string> RedisCache { get; set; } = new Dictionary<string, string>();
    }
}
