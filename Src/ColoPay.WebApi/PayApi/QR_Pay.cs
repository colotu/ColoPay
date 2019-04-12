using ColoPay.WebApi.Common;
using ColoPay.WebApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ColoPay.WebApi.PayApi
{
    public class QR_Pay
    {
        private const string gatewayUrl = "http://47.98.62.6/apisubmit";
        private const int customerid = 10022;
        private const string notify = "/qrpay/notify";
        private const string returnurl = "/qrpay/notify";
        private const string apikey = "28af59ba7c06bc89644c6459c302a1b273b15159";
        private const string version = "1.0";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderCode"></param>
        /// <param name="amount"></param>
        /// <param name="paymentGateway"></param>
        /// <param name="isCode"></param>
        /// <param name="remark"></param>
        public static string PayRequest(string orderCode, decimal amount, string paymentGateway, int get_code, string remark = "")
        {
            //启润支付，全部是POST跳转，不需要返回二维码
            get_code = 0;
            //-------记录请求参数
            YSWL.Log.LogHelper.AddInfoLog("QR_Pay-->PayRequest", String.Format("参数为：orderCode-->{0},amount-->{1},paymentGateway-->{2},remark-->{3},get_code-->{4}", orderCode, amount, paymentGateway, remark,get_code));

            string amountStr = amount.ToString("F");
            string notifyurl = "http://" + YSWL.Common.Globals.DomainFullName + notify;
            string returnurl = "";
            string signStr = StringHelper.GetMD5(String.Format("version={0}&customerid={1}&total_fee={2}&sdorderno={3}&notifyurl={4}&returnurl={5}&{6}", version, customerid, amountStr, orderCode, notifyurl, returnurl, apikey));

            YSWL.Log.LogHelper.AddInfoLog("QR_Pay-->signStrToMD5", String.Format("version={0}&customerid={1}&total_fee={2}&sdorderno={3}&notifyurl={4}&returnurl={5}&{6}", version, customerid, amountStr, orderCode, notifyurl, returnurl, apikey));

            YSWL.Log.LogHelper.AddInfoLog("QR_Pay-->signStr", signStr);

            StringBuilder builder = new StringBuilder();
            builder.Append(StringHelper.CreateField("version", version, get_code));
            builder.Append(StringHelper.CreateField("customerid", customerid.ToString(), get_code));
            builder.Append(StringHelper.CreateField("sdorderno", orderCode, get_code));
            builder.Append(StringHelper.CreateField("total_fee", amountStr, get_code));
            builder.Append(StringHelper.CreateField("notifyurl", notifyurl, get_code));
            builder.Append(StringHelper.CreateField("returnurl", returnurl, get_code));
            builder.Append(StringHelper.CreateField("paytype", paymentGateway, get_code));
            builder.Append(StringHelper.CreateField("bankcode", "", get_code));
            builder.Append(StringHelper.CreateField("get_code", get_code.ToString(), get_code));
            builder.Append(StringHelper.CreateField("sign", signStr, get_code));
            builder.Append(StringHelper.CreateField("remark", remark, get_code));
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

        public static bool VerifyNotify(QrNotify notifyinfo)
        {

            //-------记录请求参数
            YSWL.Log.LogHelper.AddInfoLog("QR_Pay-->VerifyNotify", String.Format("参数为：customerid-->{0}&status-->{1}&sdpayno-->{2}&sdorderno-->{3}&total_fee-->{4}&paytype-->{5}&apikey-->{6}&sign--->{7}", notifyinfo.customerid, notifyinfo.status, notifyinfo.sdpayno, notifyinfo.sdorderno, notifyinfo.total_fee, notifyinfo.paytype, apikey, notifyinfo.sign));

            if (notifyinfo.customerid==0 || String.IsNullOrWhiteSpace(notifyinfo.sdpayno) || String.IsNullOrWhiteSpace(notifyinfo.sdorderno) || String.IsNullOrWhiteSpace(notifyinfo.paytype))
            {
                ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单【QRPay支付回调通知失败"), "参数错误");
                return false;
            }
             

            string signStr = StringHelper.GetMD5(String.Format("customerid={0}&status={1}&sdpayno={2}&sdorderno={3}&total_fee={4}&paytype={5}&{6}", notifyinfo.customerid, notifyinfo.status, notifyinfo.sdpayno, notifyinfo.sdorderno, notifyinfo.total_fee, notifyinfo.paytype, apikey));
            if (signStr == notifyinfo.sign)
            {
                if (notifyinfo.status == 1)
                {
                    ColoPay.BLL.Pay.Order orderBll = new BLL.Pay.Order();
                    ColoPay.Model.Pay.Order orderInfo = orderBll.GetModel(notifyinfo.sdorderno);
                    if (orderInfo == null)
                    {
                        return false;
                    }
                    if (orderInfo.PaymentStatus == 2)
                    {
                        return true;
                    }
                    bool isSuccess = orderBll.CompleteOrder(orderInfo);
                    if (isSuccess)//成功之后需要回调商家回调地址
                    {
                        try
                        {
                            ColoPay.BLL.Pay.Enterprise.Notify(orderInfo);
                        }
                        catch (Exception ex)
                        {
                            ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单【{0}】QrPay支付回调通知失败：{1}", orderInfo.OrderCode, ex.Message), ex.StackTrace);
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
                YSWL.Log.LogHelper.AddInfoLog(String.Format("订单【{0}】QrPay支付验证失败", notifyinfo.sdorderno), String.Format("参数为：customerid-->{0}&status-->{1}&sdpayno-->{2}&sdorderno-->{3}&total_fee-->{4}&paytype-->{5}&apikey-->{6}&sign--->{7}", notifyinfo.customerid, notifyinfo.status, notifyinfo.sdpayno, notifyinfo.sdorderno, notifyinfo.total_fee, notifyinfo.paytype, apikey, notifyinfo.sign));
                //验证失败，记录日志
                ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单【{0}】QrPay支付验证失败", notifyinfo.sdorderno), String.Format("参数为：customerid-->{0}&status-->{1}&sdpayno-->{2}&sdorderno-->{3}&total_fee-->{4}&paytype-->{5}&apikey-->{6}&sign--->{7}", notifyinfo.customerid, notifyinfo.status, notifyinfo.sdpayno, notifyinfo.sdorderno, notifyinfo.total_fee, notifyinfo.paytype, apikey,notifyinfo.sign));
                return false;
            }
        }
    }
}