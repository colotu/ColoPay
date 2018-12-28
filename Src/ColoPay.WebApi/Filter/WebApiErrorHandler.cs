using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using ColoPay.WebApi.Models;

namespace ColoPay.WebApi.Filter
{
    /// <summary>
    /// WebApi错误处理
    /// </summary>
    public class WebApiErrorHandlerAttribute: ExceptionFilterAttribute
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            base.OnException(context);
            var responseMsg = new ResponseResult { Status = ResultStatus.Error, Result = new FailResult { Code = ResponseCode.InternalServerError } };
            YSWL.Log.LogHelper.AddTextLog("全局异常处理", "错误:" + context.Exception.Message + "-----" +
                context.Exception.StackTrace);

            // 返回http返回信息
            context.Response = new HttpResponseMessage()
            {
                Content = new StringContent(YSWL.Json.Conversion.JsonConvert.ExportToString(responseMsg)),
                StatusCode = HttpStatusCode.InternalServerError
            };
        }
    }
}