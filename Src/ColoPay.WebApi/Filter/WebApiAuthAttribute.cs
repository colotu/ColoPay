using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using YSWL.Accounts.Bus;
using ColoPay.WebApi.Common;
using ColoPay.WebApi.Models;

namespace ColoPay.WebApi.Filter
{
    public class WebApiAuthAttribute : ActionFilterAttribute
    {
        protected ColoPay.Model.Pay.Enterprise CurrEnterprise;
        private ColoPay.BLL.Pay.Enterprise enterpriseBll = new BLL.Pay.Enterprise();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string appid =HttpHelper.GetHeaderValue(actionContext.Request, "appid");
            string secrit = HttpHelper.GetHeaderValue(actionContext.Request, "secrit");
            string num = HttpHelper.GetHeaderValue(actionContext.Request, "bnum");
            HttpResponseMessage responseMsg = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new ResponseResult
            {
                Status = ResultStatus.Error,
                Result = new FailResult { Code = ResponseCode.Unauthorized, Msg = "身份验证失败" }
            });
            if (String.IsNullOrWhiteSpace(appid) || String.IsNullOrWhiteSpace(secrit) || String.IsNullOrWhiteSpace(num))
            {
                actionContext.Response = responseMsg;
                return;
            }
            bool isSuccess = enterpriseBll.Verification(num, appid, secrit);
            if (!isSuccess)
            {
                actionContext.Response = responseMsg;
                return;
            }
             
            base.OnActionExecuting(actionContext);
        }
 
    }
}