using System.Web;
using System.Web.Http;
using ColoPay.WebApi.Common;
using ColoPay.WebApi.Filter;
using ColoPay.WebApi.Models;

namespace ColoPay.WebApi.Controllers
{
    [WebApiAuth]
    [JsonCallback]
    public class ApiControllerBase : ApiController
    {

        public HttpServerUtility Server => HttpContext.Current.Server;

        /// <summary>
        /// 企业表示的key
        /// </summary>
        private readonly string EnterPriseKey = "YSWL_SAAS_EnterpriseID";

        /// <summary>
        /// 用户标识的key
        /// </summary>
        private readonly string UserKey = "YSWL_SAAS_UserName";

        /// <summary>
        /// 获取企业ID
        /// </summary>
        public long EnterpiseId
        {
            get
            {
                var request = HttpContext.Current.Request;
                string enterValue = "";
                string userAgent = request.UserAgent;
                if (userAgent != null && userAgent.Contains("ys56"))
                {
                    enterValue = request.Headers[EnterPriseKey];
                }
                else
                {
                    var httpCookie = request.Cookies[EnterPriseKey];
                    if (httpCookie != null) enterValue = httpCookie.Value;
                }
                long enterprise = YSWL.Common.DEncrypt.DEncrypt.ConvertToNumber(enterValue);
                return enterprise;
            }
        }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public YSWL.Accounts.Bus.User CurrentUser
        {
            get
            {
                var request = HttpContext.Current.Request;
                string enterValue = "";
                string userValue;
                string userAgent = request.UserAgent;

                if (userAgent != null && userAgent.Contains("ys56"))
                {
                    enterValue = request.Headers[EnterPriseKey];
                    string userName = request.Headers[UserKey];
                    userValue = StringHelper.Decode(userName);
                }
                else
                {
                    var httpCookie = request.Cookies[EnterPriseKey];
                    if (httpCookie != null) enterValue = httpCookie.Value;
                    userValue = HttpContext.Current.User.Identity.Name;
                }

                long enterprise = YSWL.Common.DEncrypt.DEncrypt.ConvertToNumber(enterValue);
                string redisKey = $"{enterprise}_{userValue}"; ;
                return RedisHelper.GetInstance().GetCache<YSWL.Accounts.Bus.User>(redisKey);
            }
        }

        /// <summary>
        /// 成功状态返回结果
        /// </summary>
        /// <param name="result">返回的数据</param>
        /// <returns></returns>
        protected ResponseResult SuccessResult(object result)
        {
            return new ResponseResult { Status = ResultStatus.Success, Result = result };
        }

        /// <summary>
        /// 成功状态返回结果
        /// </summary>
        /// <param name="result">返回的数据</param>
        /// <returns></returns>
        protected ResponseResult SuccessResult(string result)
        {
            return new ResponseResult { Status = ResultStatus.Success, Result = new { Code = ResponseCode.OK, Msg = result } };
        }

        /// <summary>
        /// 自定义状态返回结果
        /// </summary>
        /// <param name="status"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public ResponseResult Result(ResultStatus status, object result)
        {
            return new ResponseResult { Status = status, Result = result };
        }

        /// <summary>
        /// 失败状态返回结果
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">失败信息</param>
        /// <returns></returns>
        protected ResponseResult FailResult(ResponseCode code,string msg=null)
        {
            return new ResponseResult { Status = ResultStatus.Fail, Result = new FailResult { Code = code, Msg = msg} };
        }

        /// <summary>
        /// 异常状态返回结果
        /// </summary>
        /// <param name="code">状态码</param>
        /// <param name="msg">异常信息</param>
        /// <returns></returns>
        protected ResponseResult ErrorResult(ResponseCode code, string msg = null)
        {
            return new ResponseResult { Status = ResultStatus.Error, Result = new FailResult { Code = code, Msg = msg } };
        }
    }
}