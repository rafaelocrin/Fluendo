using System;
using System.Collections.Generic;
using System.Text;

namespace Fluendo.FluendoPlatform.Infrastructure.Common
{
    public class ResultRequest<T>
    {
        public T Result { get; set; }
        public Error Error { get; set; }
    }

    public class Error
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
    }
}

