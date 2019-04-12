using System;
using System.Collections.Generic;
using System.Text;

namespace Fluendo.FluendoPlatform.Infrastructure.Common.Config
{
    public class ApplicationOptions
    {
        public Dictionary<string, string> ConnectionStrings { get; set; }
        public IDictionary<string, string> Endpoints { get; set; } = new Dictionary<string, string>();
    }
}
