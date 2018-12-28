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
        protected AccountsPrincipal UserPrincipal;

        protected User CurrentUser;

        /// <summary>
        /// 企业表示的key
        /// </summary>
        private readonly string EnterPriseKey = "YSWL_SAAS_EnterpriseID";
        /// <summary>
        /// 用户标识的key
        /// </summary>
        private readonly string UserKey = "YSWL_SAAS_UserName";
        /// <summary>
        /// redis key
        /// </summary>
        private string _redisKey = string.Empty;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string userAgent = HttpContext.Current.Request.UserAgent;
            string enterPriseValue = HttpHelper.GetHeaderValue(actionContext.Request, EnterPriseKey);
            string userValue = HttpHelper.GetHeaderValue(actionContext.Request, UserKey);
            HttpResponseMessage responseMsg = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new ResponseResult
            {
                Status = ResultStatus.Error,
                Result = new FailResult { Code = ResponseCode.Unauthorized, Msg = "身份验证失败" }
            });

            if (!string.IsNullOrEmpty(userAgent) && userAgent.Contains("ys56")
                && !string.IsNullOrEmpty(enterPriseValue) && !string.IsNullOrEmpty(userValue))
            {
                string username = StringHelper.Decode(userValue);
                long enterprise = YSWL.Common.DEncrypt.DEncrypt.ConvertToNumber(enterPriseValue);
                YSWL.Common.CallContextHelper.SetAutoTag(enterprise);

                if (!AppIsOpen(enterprise, actionContext))
                {
                    return;
                }
                if (!AuthByApp(actionContext, enterprise, username))
                {
                    return;
                }
            }
            else
            {
                enterPriseValue = HttpHelper.GetCookieValue(actionContext.Request, EnterPriseKey);
                userValue = HttpHelper.GetCookieValue(actionContext.Request, UserKey);

                if (string.IsNullOrEmpty(enterPriseValue) || string.IsNullOrEmpty(userValue))
                {
                    actionContext.Response = responseMsg;
                    return;
                }
                string username = StringHelper.Decode(userValue);
                long enterprise = YSWL.Common.DEncrypt.DEncrypt.ConvertToNumber(enterPriseValue);
                YSWL.Common.CallContextHelper.SetAutoTag(enterprise);

                if (!AppIsOpen(enterprise, actionContext))
                {
                    return;
                }
                if (!AuthByWeb(actionContext, enterprise, username))
                {
                    return;
                }
            }
            CurrentUser = new User(UserPrincipal);
            RedisHelper.GetInstance().SetCache(_redisKey, CurrentUser, DateTime.Now.AddMonths(3), TimeSpan.Zero);
            base.OnActionExecuting(actionContext);
        }

        /// <summary>
        /// web端调用验证
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="enterPriseValue"></param>
        /// <param name="userValue"></param>
        /// <returns></returns>
        private bool AuthByWeb(HttpActionContext actionContext, long enterPriseValue, string userValue)
        {
            //if (!HttpContext.Current.User.Identity.IsAuthenticated) return false;
            //string userValue = HttpContext.Current.User.Identity.Name;

            _redisKey = $"{enterPriseValue}_{userValue}";
            if (RedisHelper.GetInstance().GetCache<User>(_redisKey) == null) return false;
            try
            {
                UserPrincipal = new AccountsPrincipal(userValue);
            }
            catch (System.Security.Principal.IdentityNotMappedException)
            {
                //用户在DB中不存在 退出
                System.Web.Security.FormsAuthentication.SignOut();
                RedisHelper.GetInstance().DeleteCache(_redisKey);

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new ResponseResult
                {
                    Status = ResultStatus.Error,
                    Result = new FailResult { Code = ResponseCode.Unauthorized, Msg = "用户不存在" }
                });
                return false;
            }
            return true;
        }

        /// <summary>
        /// app端调用验证
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="enterPriseValue"></param>
        /// <param name="userValue"></param>
        /// <returns></returns>
        private bool AuthByApp(HttpActionContext actionContext, long enterPriseValue, string userValue)
        {
            _redisKey = $"{enterPriseValue}_{userValue}";
            if (RedisHelper.GetInstance().GetCache<User>(_redisKey) == null) return false;
            try
            {
                UserPrincipal = new AccountsPrincipal(userValue);
            }
            catch (System.Security.Principal.IdentityNotMappedException)
            {
                //用户在DB中不存在 退出
                RedisHelper.GetInstance().DeleteCache(_redisKey);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new ResponseResult
                {
                    Status = ResultStatus.Error,
                    Result = new FailResult { Code = ResponseCode.Unauthorized, Msg = "用户不存在" }
                });
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断应用是否开通
        /// </summary>
        /// <returns></returns>
        private bool AppIsOpen(long enterpriseId, HttpActionContext actionContext)
        {
            string tag = YSWL.Common.ConfigHelper.GetConfigString("SystemFlag");
            bool isOpen = YSWL.SAAS.BLL.SAASInfo.AppIsOpenCache(tag, YSWL.Common.Globals.SafeInt(enterpriseId, 0));
            if (!isOpen)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new ResponseResult
                {
                    Status = ResultStatus.Error,
                    Result = new FailResult { Code = ResponseCode.Unauthorized, Msg = "应用未开通或已过期" }
                });
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取企业标识
        /// </summary>
        /// <param name="actionContext"></param>
        private bool GetAutoTag(HttpActionContext actionContext)
        {
            string headerValue = HttpHelper.GetHeaderValue(actionContext.Request, EnterPriseKey);
            if (!string.IsNullOrEmpty(headerValue))
            {
                string yswlSaasEnterpriseId = HttpHelper.GetCookieValue(actionContext.Request, EnterPriseKey);
                if (!string.IsNullOrEmpty(yswlSaasEnterpriseId))
                {
                    YSWL.Common.CallContextHelper.SetAutoTag(YSWL.Common.DEncrypt.DEncrypt.ConvertToNumber(yswlSaasEnterpriseId));
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, new ResponseResult
                    {
                        Status = ResultStatus.Error,
                        Result = new FailResult { Code = ResponseCode.Unauthorized, Msg = "企业标识不能为空" }
                    });
                    return false;
                }
            }
            else
            {
                YSWL.Common.CallContextHelper.SetAutoTag(YSWL.Common.DEncrypt.DEncrypt.ConvertToNumber(headerValue));
            }
            return true;
        }
    }
}