//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Text;
//using Microsoft.AspNetCore.Mvc.Controllers;

//namespace Fluendo.FluendoPlatform.Infrastructure.Common
//{
//    public static class HttpResponseHelper
//    {
//        public static HttpResponseMessage ResponseMessage(string content, HttpStatusCode statusCode)
//        {
//            //HttpResponseMessage responseMessage = new HttpResponseMessage();

//            var resultMsg = new HttpResponseMessage();
//            resultMsg.Content = new StringContent(content);
//            resultMsg.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
//            resultMsg.StatusCode = statusCode;

//            result = resultMsg;

//            switch (httpResponse.StatusCode)
//            {
//                case HttpStatusCode.InternalServerError:
//                    resultMsg.StatusCode =
//                    responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
//                    break;
//                case HttpStatusCode.NotModified:
//                    responseMessage = new HttpResponseMessage(HttpStatusCode.NotModified);
//                    break;
//                case HttpStatusCode.NotFound:
//                    responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
//                    break;
//            }

//            responseMessage.Content = httpResponse.Content;

//            return responseMessage;
//        }
//    }
//}
