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

        private ColoPay.BLL.Pay.Enterprise bll = new BLL.Pay.Enterprise();
        public HttpServerUtility Server => HttpContext.Current.Server;

        /// <summary>
        /// 当前企业用户
        /// </summary>
        public ColoPay.Model.Pay.Enterprise CurrEnterprise
        {
            get
            {
                var request = HttpContext.Current.Request;
            
                string appid=request.Headers["appid"];
                string secrit = request.Headers["secrit"];
                string bnum = request.Headers["bnum"];
                return bll.GetEnterpriseInfo(bnum, appid, secrit);
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