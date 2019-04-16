using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fluendo.FluendoPlatform.Infrastructure.Common
{
    public static class Utils
    {
        public static Dictionary<String, Object> ParseByteToJson(byte[] json)
        {
            string jsonStr = Encoding.UTF8.GetString(json);
            return JsonConvert.DeserializeObject<Dictionary<String, Object>>(jsonStr);
        }

        public static byte[] ParseStringToBytes(string content)
        {
            return Encoding.UTF8.GetBytes(content);
        }
    }
}
