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
    public class DaDaBank
    {
        private const string gatewayUrl = "http://39.108.126.141/pay/unifiedorder";
        private const string AppId = "115443746068290615";
        private const string notify = "/dapay/notify";
        private const string returnurl = "";
        private const string apikey = "1b39478bf2aef372cc066fa59be728a7";
        private const string sign_type = "MD5";
        private const string paytype = "100050";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderCode"></param>
        /// <param name="amount"></param>
        /// <param name="paymentGateway"></param>
        /// <param name="isCode"></param>
        /// <param name="remark"></param>
        public static string PayRequest(string orderCode, decimal amount, string bankcode, string remark = "")
        {
            //-------记录请求参数
            YSWL.Log.LogHelper.AddInfoLog("DaDaBank-->PayRequest", String.Format("参数为：orderCode-->{0},amount-->{1},bankcode-->{2},remark-->{3}", orderCode, amount, bankcode, remark));

            string amountStr = YSWL.Common.Globals.SafeDecimal(amount, 0).ToString("F2");

            string notifyurl = "http://" + YSWL.Common.Globals.DomainFullName + notify;

            string spbill_create_ip = getRealIp();
            string successurl = returnurl;
            if (String.IsNullOrWhiteSpace(returnurl))
            {
                successurl = notifyurl;
            }
            string signStr = GetSignStr(orderCode, remark, amountStr, spbill_create_ip, notifyurl, AppId, paytype, successurl, bankcode);
            int get_code = 1;
            StringBuilder builder = new StringBuilder();
            builder.Append(StringHelper.CreateField("out_trade_no", orderCode, get_code));
            builder.Append(StringHelper.CreateField("order_name", remark, get_code));
            builder.Append(StringHelper.CreateField("total_amount", amountStr, get_code));
            builder.Append(StringHelper.CreateField("notify_url", notifyurl, get_code));
            builder.Append(StringHelper.CreateField("successurl", successurl, get_code));
            builder.Append(StringHelper.CreateField("paytype", paytype, get_code));
            builder.Append(StringHelper.CreateField("bankcode", bankcode, get_code));
            builder.Append(StringHelper.CreateField("spbill_create_ip", spbill_create_ip, get_code));
            builder.Append(StringHelper.CreateField("appid", AppId, get_code));
            builder.Append(StringHelper.CreateField("sign_type", sign_type, get_code));
            builder.Append(StringHelper.CreateField("sign", signStr, get_code));

            var request = (HttpWebRequest)WebRequest.Create(gatewayUrl);

            string postData = builder.ToString().Substring(1);

            //-------记录请求参数
            YSWL.Log.LogHelper.AddInfoLog("DaDaBank-->PayRequest", String.Format("参数为：postData-->{0}", postData));

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

        public static string[] BubbleSort(string[] r)
        {
            for (int i = 0; i < r.Length; i++)
            {
                bool flag = false;
                for (int j = r.Length - 2; j >= i; j--)
                {
                    if (string.CompareOrdinal(r[j + 1], r[j]) < 0)
                    {
                        string str = r[j + 1];
                        r[j + 1] = r[j];
                        r[j] = str;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    return r;
                }
            }
            return r;
        }

        public static string GetSignStr(string out_trade_no, string order_name, string total_amount, string spbill_create_ip, string notify_url, string appid, string paytype, string successurl, string bankcode)
        {
            int num;
            string[] strArray;
            strArray = new string[] { "out_trade_no=" + out_trade_no, "order_name=" + order_name, "total_amount=" + total_amount, "spbill_create_ip=" + spbill_create_ip, "notify_url=" + notify_url, "appid=" + appid, "paytype=" + paytype, "successurl=" + successurl, "bankcode=" + bankcode };
            string[] strArray2 = BubbleSort(strArray);
            StringBuilder builder = new StringBuilder();
            for (num = 0; num < strArray2.Length; num++)
            {
                if (num == (strArray2.Length - 1))
                {
                    builder.Append(strArray2[num]);
                }
                else
                {
                    builder.Append(strArray2[num] + "&");
                }
            }
            builder.Append("&key=" + apikey);
            string str = StringHelper.GetMD5(builder.ToString()).ToLower();
            //char[] separator = new char[] { '=' };
            //StringBuilder builder2 = new StringBuilder();
            //builder2.Append(gateway);
            //for (num = 0; num < strArray2.Length; num++)
            //{
            //    builder2.Append(strArray2[num].Split(separator)[0] + "=" + HttpUtility.UrlEncode(strArray2[num].Split(separator)[1]) + "&");
            //}
            //builder2.Append("sign=" + str + "&sign_type=" + sign_type);
            return str;
        }

        private static string getRealIp()
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static string GetNotifySignStr(string order, decimal mount, string code, string msg, string time)
        {
            int num;
            string[] strArray;
            strArray = new string[] { "order=" + order, "mount=" + mount, "code=" + code, "msg=" + msg, "time=" + time};
            string[] strArray2 = BubbleSort(strArray);
            StringBuilder builder = new StringBuilder();
            for (num = 0; num < strArray2.Length; num++)
            {
                if (num == (strArray2.Length - 1))
                {
                    builder.Append(strArray2[num]);
                }
                else
                {
                    builder.Append(strArray2[num] + "&");
                }
            }
            builder.Append("&key=" + apikey);
            string str = StringHelper.GetMD5(builder.ToString()).ToLower();
            return str;
        }

        public static bool VerifyNotify(DaNotify notifyinfo)
        {
            //-------记录请求参数
            YSWL.Log.LogHelper.AddInfoLog("DaDaBank-->VerifyNotify", String.Format("参数为：order-->{0}&code-->{1}&mount-->{2}&msg-->{3}&time-->{4}&sign-->{5}", notifyinfo.order, notifyinfo.code, notifyinfo.mount, notifyinfo.msg, notifyinfo.time,notifyinfo.sign));

            if (String.IsNullOrWhiteSpace(notifyinfo.order) || String.IsNullOrWhiteSpace(notifyinfo.code))
            {
                ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单【DaPay支付回调通知失败"), "参数错误");
                return false;
            }
            string signStr = GetNotifySignStr(notifyinfo.order,notifyinfo.mount,notifyinfo.code,notifyinfo.msg,notifyinfo.time); //StringHelper.GetMD5(String.Format("mch_id={0}&status={1}&sdpayno={2}&order_no={3}&money={4}&paytype={5}&{6}", notifyinfo.mch_id, notifyinfo.status, notifyinfo.sdpayno, notifyinfo.order_no, notifyinfo.money, notifyinfo.paytype, apikey));
            if (signStr == notifyinfo.sign)
            {
                if (notifyinfo.code == "200")
                {
                    ColoPay.BLL.Pay.Order orderBll = new BLL.Pay.Order();
                    ColoPay.Model.Pay.Order orderInfo = orderBll.GetModel(notifyinfo.order);
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
                            ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单【{0}】DaPay支付回调通知失败：{1}", orderInfo.OrderCode, ex.Message), ex.StackTrace);
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
                ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单DaPay支付回调通知失败"), String.Format("加密签名错误：接口签名字段为【{0}】---生成的签名为【{1}】", notifyinfo.sign, signStr));
                //验证失败，记录日志
                ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单【{0}】DaPay支付验证失败", notifyinfo.order), String.Format("参数为：order-->{0}&code-->{1}&mount-->{2}&msg-->{3}&time-->{4}", notifyinfo.order, notifyinfo.code, notifyinfo.mount, notifyinfo.msg, notifyinfo.time));
                return false;
            }
        }


    }
}