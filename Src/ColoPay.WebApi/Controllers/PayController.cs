using ColoPay.WebApi.Common;
using ColoPay.WebApi.Models;
using ColoPay.WebApi.PayApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using YSWL.Json;
using YSWL.Json.Conversion;

namespace ColoPay.WebApi.Controllers
{
    public class PayController : ApiControllerBase
    {
        private ColoPay.BLL.Pay.PaymentTypes typeBll = new BLL.Pay.PaymentTypes();
        private ColoPay.BLL.Pay.EnterprisePayFee feeBll = new BLL.Pay.EnterprisePayFee();
        private ColoPay.BLL.Pay.Order orderBll = new BLL.Pay.Order();
        private ColoPay.BLL.Pay.Enterprise bll = new BLL.Pay.Enterprise();
        /// <summary>
        /// 支付 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("apipay")]
        public ResponseResult SubmitOrder([FromBody]PayInfo payinfo)
        {

            //YSWL.Common.DEncrypt.DEncrypt.GetMD5FromStr
            //验证是否数据安全性
            if (payinfo.amount < 0)
            {
                return FailResult(ResponseCode.ParamError, "amount is illegal");
            }
            if (String.IsNullOrWhiteSpace(payinfo.order_no))
            {
                return FailResult(ResponseCode.ParamError, "ordercode is illegal");
            }


            ColoPay.Model.Pay.Enterprise CurrEnterprise = bll.GetEnterpriseInfo(payinfo.appid, payinfo.secrit);
            if (CurrEnterprise == null)
            {
                return FailResult(ResponseCode.ParamError, "appid or secrit is illegal");
            }
            //判断订单是否存在
            ColoPay.Model.Pay.Order orderInfo = orderBll.GetModelEx(payinfo.order_no, CurrEnterprise.EnterpriseID);

            if (orderInfo == null)
            {
                //创建订单
                orderInfo = new Model.Pay.Order();
                orderInfo.Agentd = CurrEnterprise.AgentId;
                orderInfo.Amount = payinfo.amount;
                orderInfo.AppId = CurrEnterprise.AppId;
                orderInfo.AppReturnUrl = String.IsNullOrWhiteSpace(payinfo.return_url) ? CurrEnterprise.AppReturnUrl : payinfo.return_url;
                orderInfo.AppSecrit = CurrEnterprise.AppSecrit;
                orderInfo.AppUrl = HttpContext.Current.Request.Url.ToString();
                orderInfo.CreatedTime = DateTime.Now;
                orderInfo.EnterOrder = payinfo.order_no;
                orderInfo.EnterpriseID = CurrEnterprise.EnterpriseID;
                orderInfo.OrderCode = "P" + CurrEnterprise.EnterpriseID + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                //获取支付方式
                ColoPay.Model.Pay.PaymentTypes typeInfo = typeBll.GetPaymentInfo(payinfo.paytype);
                if (typeInfo == null)
                {
                    return FailResult(ResponseCode.ParamError, "paytype is illegal");
                }
                //获取支付费率

                ColoPay.Model.Pay.EnterprisePayFee feeInfo = feeBll.GetModel(CurrEnterprise.EnterpriseID, typeInfo.ModeId);
                if (feeInfo == null)
                {
                    return FailResult(ResponseCode.ParamError, "paytype is illegal");
                }
                orderInfo.FeeRate = feeInfo.FeeRate;
                orderInfo.PaymentFee = payinfo.amount * (feeInfo.FeeRate / 100);
                orderInfo.OrderAmount = payinfo.amount - orderInfo.PaymentFee;
                orderInfo.PaymentGateway = typeInfo.Gateway;
                orderInfo.PaymentStatus = 0;
                orderInfo.AppNotifyUrl = CurrEnterprise.AppReturnUrl;
                orderInfo.PaymentTypeName = typeInfo.Name;
                orderInfo.PayModeId = typeInfo.ModeId;
                orderInfo.OrderInfo = String.IsNullOrWhiteSpace(payinfo.remark) ? "" : payinfo.remark;
                orderInfo.OrderId = orderBll.Add(orderInfo);
                if (orderInfo.OrderId == 0)//创建订单失败
                {
                    return FailResult(ResponseCode.ServiceUnavailable, "payorder is error");
                }
            }
            else //订单已经存在了
            {
                if (orderInfo.Amount != payinfo.amount)//金额不一样，说明订单不一样
                {
                    return FailResult(ResponseCode.OrderExists, "order_no has exist");
                }
                if (orderInfo.PaymentStatus == 2)
                {
                    return FailResult(ResponseCode.HasPaid, "order has paid");
                }
            }
            string resullt = "";
            //BZ 支付金额必须要为整数，有点扯淡
            //开始支付
            if (!payinfo.istest)
            {
                //tuzh BZ_Pay 支付接口已失效 
                // resullt = ColoPay.WebApi.PayApi.BZ_Pay.PayRequest(orderInfo.OrderCode, payinfo.amount, orderInfo.PaymentGateway, payinfo.get_code, orderInfo.OrderInfo);
                resullt = ColoPay.WebApi.PayApi.QR_Pay.PayRequest(orderInfo.OrderCode, payinfo.amount, orderInfo.PaymentGateway, payinfo.get_code, orderInfo.OrderInfo);

            }
            else //测试支付
            {
                bool isSuccess = orderBll.CompleteOrder(orderInfo);
                if (isSuccess)//成功之后需要回调商家回调地址
                {
                    try
                    {
                        orderInfo.PaymentStatus = 2;
                        EnterpriseNotify.Notify(orderInfo);
                    }
                    catch (Exception ex)
                    {
                        ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单【{0}】BZPay支付回调通知失败：{1}", orderInfo.OrderCode, ex.Message), ex.StackTrace);
                    }
                }
            }

            return SuccessResult(resullt);
        }

        /// <summary>
        /// 支付 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("apipay/bank")]
        public HttpResponseMessage PayBankOrder([FromBody]PayInfo payinfo)
        {
            //YSWL.Common.DEncrypt.DEncrypt.GetMD5FromStr
            //验证是否数据安全性
            if (payinfo.amount < 0)
            {
                return new HttpResponseMessage { Content = new StringContent("amount is illegal", Encoding.GetEncoding("UTF-8"), "text/plain") };
            }
            if (String.IsNullOrWhiteSpace(payinfo.order_no))
            {
                return new HttpResponseMessage { Content = new StringContent("ordercode is illegal", Encoding.GetEncoding("UTF-8"), "text/plain") };
            }


            ColoPay.Model.Pay.Enterprise CurrEnterprise = bll.GetEnterpriseInfo(payinfo.appid, payinfo.secrit);
            if (CurrEnterprise == null)
            {
                return new HttpResponseMessage { Content = new StringContent("appid or secrit is illegal", Encoding.GetEncoding("UTF-8"), "text/plain") };
            }
            //判断订单是否存在
            ColoPay.Model.Pay.Order orderInfo = orderBll.GetModelEx(payinfo.order_no, CurrEnterprise.EnterpriseID);

            if (orderInfo == null)
            {
                //创建订单
                orderInfo = new Model.Pay.Order();
                orderInfo.Agentd = CurrEnterprise.AgentId;
                orderInfo.Amount = payinfo.amount;
                orderInfo.AppId = CurrEnterprise.AppId;
                orderInfo.AppReturnUrl = String.IsNullOrWhiteSpace(payinfo.return_url) ? CurrEnterprise.AppReturnUrl : payinfo.return_url;
                orderInfo.AppSecrit = CurrEnterprise.AppSecrit;
                orderInfo.AppUrl = HttpContext.Current.Request.Url.ToString();
                orderInfo.CreatedTime = DateTime.Now;
                orderInfo.EnterOrder = payinfo.order_no;
                orderInfo.EnterpriseID = CurrEnterprise.EnterpriseID;
                orderInfo.OrderCode = "P" + CurrEnterprise.EnterpriseID + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                //获取支付方式
                ColoPay.Model.Pay.PaymentTypes typeInfo = typeBll.GetPaymentInfo(payinfo.paytype);
                if (typeInfo == null)
                {
                    return new HttpResponseMessage { Content = new StringContent("paytype is illegal", Encoding.GetEncoding("UTF-8"), "text/plain") };

                }
                //获取支付费率

                ColoPay.Model.Pay.EnterprisePayFee feeInfo = feeBll.GetModel(CurrEnterprise.EnterpriseID, typeInfo.ModeId);
                if (feeInfo == null)
                {
                    return new HttpResponseMessage { Content = new StringContent("paytype is illegal", Encoding.GetEncoding("UTF-8"), "text/plain") };
                }
                orderInfo.FeeRate = feeInfo.FeeRate;
                orderInfo.PaymentFee = payinfo.amount * (feeInfo.FeeRate / 100);
                orderInfo.OrderAmount = payinfo.amount - orderInfo.PaymentFee;
                orderInfo.PaymentGateway = typeInfo.Gateway;
                orderInfo.PaymentStatus = 0;
                orderInfo.AppNotifyUrl = CurrEnterprise.AppReturnUrl;
                orderInfo.PaymentTypeName = typeInfo.Name;
                orderInfo.PayModeId = typeInfo.ModeId;
                orderInfo.OrderInfo = String.IsNullOrWhiteSpace(payinfo.remark) ? "" : payinfo.remark;
                orderInfo.OrderId = orderBll.Add(orderInfo);
                if (orderInfo.OrderId == 0)//创建订单失败
                {
                    return new HttpResponseMessage { Content = new StringContent("payorder is error", Encoding.GetEncoding("UTF-8"), "text/plain") };
                }
            }
            else //订单已经存在了
            {
                if (orderInfo.Amount != payinfo.amount)//金额不一样，说明订单不一样
                {
                    return new HttpResponseMessage { Content = new StringContent("order_no has exist", Encoding.GetEncoding("UTF-8"), "text/plain") };
                }
                if (orderInfo.PaymentStatus == 2)
                {
                    return new HttpResponseMessage { Content = new StringContent("order has paid", Encoding.GetEncoding("UTF-8"), "text/plain") };
                }
            }

            string resullt = ColoPay.WebApi.PayApi.DaDaBank.PayRequest(orderInfo.OrderCode, payinfo.amount, payinfo.bankcode, orderInfo.OrderInfo);
            YSWL.Json.JsonObject jsonObject = JsonConvert.Import<JsonObject>(resullt);
            if (jsonObject["bxstatus"].ToString() == "SUCCESS")
            {
                string pay_url = jsonObject["pay_url"].ToString();
                HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.Moved);
                resp.Headers.Location = new Uri(pay_url);
                return resp;
            }
            else
            {
                return new HttpResponseMessage { Content = new StringContent(jsonObject["bxmsg"].ToString(), Encoding.GetEncoding("UTF-8"), "text/plain") };
            }



        }

        /// <summary>
        /// 支付异步通知
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("bzpay/notify")]
        public HttpResponseMessage Notify([FromBody]BZNotify notifyinfo)
        {
            bool isSuccess = ColoPay.WebApi.PayApi.BZ_Pay.VerifyNotify(notifyinfo);
            string responseStr = isSuccess ? "success" : "fail";
            // HttpContext.Current.Response.Write(responseStr);
            HttpResponseMessage responseMessage = new HttpResponseMessage { Content = new StringContent(responseStr, Encoding.GetEncoding("UTF-8"), "text/plain") };
            return responseMessage;
        }

        /// <summary>
        /// 支付异步通知 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("dapay/notify")]
        public HttpResponseMessage DaNotify([FromBody]DaNotify notifyinfo)
        {
            bool isSuccess = ColoPay.WebApi.PayApi.DaDaBank.VerifyNotify(notifyinfo);
            string responseStr = isSuccess ? "success" : "fail";
            // HttpContext.Current.Response.Write(responseStr);
            HttpResponseMessage responseMessage = new HttpResponseMessage { Content = new StringContent(responseStr, Encoding.GetEncoding("UTF-8"), "text/plain") };
            return responseMessage;
        }


        /// <summary>
        /// 支付异步通知 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("qrpay/notify")]
        public HttpResponseMessage QrNotify([FromBody]QrNotify notifyinfo)
        {
            bool isSuccess = ColoPay.WebApi.PayApi.QR_Pay.VerifyNotify(notifyinfo);
            string responseStr = isSuccess ? "success" : "fail";
            // HttpContext.Current.Response.Write(responseStr);
            HttpResponseMessage responseMessage = new HttpResponseMessage { Content = new StringContent(responseStr, Encoding.GetEncoding("UTF-8"), "text/plain") };
            return responseMessage;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("query")]
        public ResponseResult Query([FromBody]QueryInfo query)
        {

            ColoPay.Model.Pay.Enterprise CurrEnterprise = bll.GetEnterpriseInfo(query.appid, query.secrit);
            if (CurrEnterprise == null)
            {
                return FailResult(ResponseCode.ParamError, "appid or secrit is illegal");
            }
            //判断订单是否存在
            ColoPay.Model.Pay.Order orderInfo = orderBll.GetModelEx(query.order_no, CurrEnterprise.EnterpriseID);
            if (orderInfo.PaymentStatus != 2)//订单状态未支付，则去支付平台查询一次
            {
                bool hasPaid = ColoPay.WebApi.PayApi.BZ_Pay.HasPaid(orderInfo.OrderCode);
                if (hasPaid)
                {
                    bool isSuccess = orderBll.CompleteOrder(orderInfo);
                    if (isSuccess)
                    {
                        orderInfo.PaymentStatus = 2;
                        orderInfo.OrderStatus = 1;
                        orderBll.HasNotify(orderInfo.OrderId);
                    }
                }
            }
            YSWL.Json.JsonObject jsonObject = new JsonObject();
            jsonObject["appid"] = query.appid;
            jsonObject["secrit"] = query.secrit;
            jsonObject["order_no"] = orderInfo.EnterOrder;
            jsonObject["amount"] = orderInfo.Amount.ToString();
            jsonObject["sdorder_no"] = orderInfo.OrderCode;
            jsonObject["paytype"] = orderInfo.PaymentGateway;
            jsonObject["status"] = orderInfo.PaymentStatus.ToString();
            return SuccessResult(jsonObject);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage Test()
        {
            HttpResponseMessage resp = new HttpResponseMessage(HttpStatusCode.Moved);
            resp.Headers.Location = new Uri("http://www.baidu.com");
            
            return resp;
            //return SuccessResult("测试接口成功");
        }

    }


}