using System.ComponentModel;
using ColoPay.WebApi.Common;

namespace ColoPay.WebApi.Models
{
    public class FailResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        private ResponseCode _code = ResponseCode.OK;
        public ResponseCode Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }

        /// <summary>
        /// 消息描述
        /// </summary>
        private string _msg;
        public string Msg
        {
            get
            {
                return !string.IsNullOrEmpty(_msg) ? _msg : EnumHelper.GetDescription(Code);
            }
            set
            {
                _msg = value;
            }
        }
    }

    public enum ResponseCode
    {
        [Description("请求成功")]
        OK = 200,
        NonAuthoritativeInformation = 203,
        Moved = 301,
        Redirect = 302,
        [Description("权限不足")]
        Unauthorized = 401,
        Forbidden = 403,
        [Description("请求资源不存在")]
        NotFound = 404,
        [Description("服务端内部异常")]
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,

        #region 客户处理状态

        [Description("订单号已存在")]
        OrderExists = 1002,
        [Description("订单已经支付")]
        HasPaid = 1003,
        [Description("客户已存在")]
        CustomerExists =1003,

        #endregion
        [Description("参数错误")]
        ParamError =2000,
        [Description("请求失败")]
        ExecuteError =2001,

        [Description("库存不足")]
        NoHaveStock=1500
    }
}