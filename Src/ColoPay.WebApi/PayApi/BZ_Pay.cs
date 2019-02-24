using ColoPay.WebApi.Common;
using ColoPay.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using YSWL.Json;
using YSWL.Json.Conversion;

namespace ColoPay.WebApi.PayApi
{
    public class BZ_Pay
    {
        private const string gatewayUrl = "http://www.bz-pospay.cn/apisubmit";
        private const string mch_id = "11478";
        private const string notify = "/bzpay/notify";
        private const string returnurl = "";
        private const string apikey = "0332043ef455e55d72cf2f16df5951a99f7ed5f1";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderCode"></param>
        /// <param name="amount"></param>
        /// <param name="paymentGateway"></param>
        /// <param name="isCode"></param>
        /// <param name="remark"></param>
        public static string PayRequest(string orderCode, int amount, string paymentGateway, int get_code, string remark = "")
        {
            string amountStr = YSWL.Common.Globals.SafeInt(amount, 0).ToString();
            string notifyurl = "http://" + YSWL.Common.Globals.DomainFullName + notify;
            string signStr = StringHelper.GetMD5(String.Format("mch_id={0}&money={1}&order_no={2}&notifyurl={3}&{4}", mch_id, amountStr, orderCode, notifyurl, apikey));
            StringBuilder builder = new StringBuilder();
            builder.Append(StringHelper.CreateField("mch_id", mch_id, get_code));
            builder.Append(StringHelper.CreateField("order_no", orderCode, get_code));
            builder.Append(StringHelper.CreateField("remark", remark, get_code));
            builder.Append(StringHelper.CreateField("money", amountStr, get_code));
            builder.Append(StringHelper.CreateField("notifyurl", notifyurl, get_code));
            builder.Append(StringHelper.CreateField("returnurl", returnurl, get_code));
            builder.Append(StringHelper.CreateField("paytype", paymentGateway, get_code));
            builder.Append(StringHelper.CreateField("bankcode", "", get_code));
            builder.Append(StringHelper.CreateField("get_code", get_code.ToString(), get_code));
            builder.Append(StringHelper.CreateField("sign", signStr, get_code));
            if (get_code == 1)
            {
                var request = (HttpWebRequest)WebRequest.Create(gatewayUrl);

                string postData = builder.ToString().Substring(1);
                var data = Encoding.UTF8.GetBytes(postData);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                string responseString = "";
                var response = (HttpWebResponse)request.GetResponse();
                responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            else
            {
                StringHelper.SubmitPaymentForm(StringHelper.CreateForm(builder.ToString(), gatewayUrl));
                return "";
            }

        }

        public static bool VerifyNotify(BZNotify notifyinfo)
        {
            if (String.IsNullOrWhiteSpace(notifyinfo.mch_id) || String.IsNullOrWhiteSpace(notifyinfo.sdpayno) || String.IsNullOrWhiteSpace(notifyinfo.order_no) || String.IsNullOrWhiteSpace(notifyinfo.paytype))
            {
                ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单【BZPay支付回调通知失败"), "参数错误");
                return false;
            }
            string signStr = StringHelper.GetMD5(String.Format("mch_id={0}&status={1}&sdpayno={2}&order_no={3}&money={4}&paytype={5}&{6}", notifyinfo.mch_id, notifyinfo.status, notifyinfo.sdpayno, notifyinfo.order_no, notifyinfo.money, notifyinfo.paytype, apikey));
            if (signStr == notifyinfo.sign)
            {
                if (notifyinfo.status == 1)
                {
                    ColoPay.BLL.Pay.Order orderBll = new BLL.Pay.Order();
                    ColoPay.Model.Pay.Order orderInfo = orderBll.GetModel(notifyinfo.order_no);
                    if (orderInfo == null)
                    {
                        return false;
                    }
                    if (orderInfo.PaymentStatus == 2)
                    {
                        return false;
                    }
                    bool isSuccess = orderBll.CompleteOrder(orderInfo);
                    if (isSuccess)//成功之后需要回调商家回调地址
                    {
                        try
                        {
                            EnterpriseNotify.Notify(orderInfo);
                        }
                        catch (Exception ex)
                        {
                            ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单【{0}】BZPay支付回调通知失败：{1}", orderInfo.OrderCode, ex.Message), ex.StackTrace);
                            return isSuccess;
                        }
                    }

                    return isSuccess;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                //验证失败，记录日志
                ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单【{0}】BZPay支付验证失败", notifyinfo.order_no), String.Format("参数为：mch_id-->{0}&status-->{1}&sdpayno-->{2}&order_no-->{3}&money-->{4}&paytype-->{5}&apikey-->{6}", notifyinfo.mch_id, notifyinfo.status, notifyinfo.sdpayno, notifyinfo.order_no, notifyinfo.money, notifyinfo.paytype, apikey));
                return false;
            }
        }

        public static bool HasPaid(string order_no)
        {
            string requestUrl = "http://www.bz-pospay.cn/apiorderquery";
            var request = (HttpWebRequest)WebRequest.Create(requestUrl);
            StringBuilder builder = new StringBuilder();
            builder.Append(StringHelper.CreateField("mch_id", mch_id));
            builder.Append(StringHelper.CreateField("sdorderno", order_no));
            string postData = builder.ToString().Substring(1);
            var data = Encoding.UTF8.GetBytes(postData);
            try
            {
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();
                string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                if (!String.IsNullOrWhiteSpace(responseString))
                {
                    YSWL.Json.JsonObject jsonObject = JsonConvert.Import<JsonObject>(responseString);
                    if (jsonObject["status"] == null || YSWL.Common.Globals.SafeInt(jsonObject["status"].ToString(),0)!=200)
                    {
                        return false;
                    }
                    if (jsonObject["pay_state"] != null && YSWL.Common.Globals.SafeInt(jsonObject["ay_state"].ToString(), 0) == 1)//支付成功了
                    {
                        return true;
                    }
                }
              
            }
            catch (Exception ex)
            {
                ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("查询订单【{0}】支付状态失败：{1}", order_no, ex.Message), ex.StackTrace);
            }

            return false;

        }

    }
}