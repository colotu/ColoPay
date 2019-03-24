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
    public class FengHe
    {
        private const string gatewayUrl = "http://fenghetc.com/Pay_Index.html";
        private const int merchantId = 10035;
        private const string notify = "/fhpay/notify";
        private const string returnurl = "/fhpay/notify";
        private const string apikey = "5153cadm6z59otmqq1qkf0mepdvls6iu";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderCode"></param>
        /// <param name="amount"></param>
        /// <param name="paymentGateway"></param>
        /// <param name="isCode"></param>
        /// <param name="remark"></param>
        public static string PayRequest(string orderCode, decimal amount, string bankcode,  string remark = "")
        {
            int get_code = 0;
            //-------记录请求参数
            YSWL.Log.LogHelper.AddInfoLog("FengHe-->PayRequest", String.Format("参数为：orderCode-->{0},amount-->{1},bankcode-->{2},remark-->{3},get_code-->{4}", orderCode, amount, bankcode, remark, get_code));

            //网银，快捷支付
            if (bankcode.Equals("wangyin"))
            {
                bankcode = "907";   //'银行编码
            }
            else if (bankcode.Equals("kuaijie"))
            {
                bankcode = "911";   //'银行编码
            }
            string amountStr = amount.ToString("F");
            string notifyurl = "http://" + YSWL.Common.Globals.DomainFullName + notify;
            string callbackurl = "http://" + YSWL.Common.Globals.DomainFullName + returnurl;
            string dateStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string signStr = StringHelper.GetMD5(String.Format("pay_amount={0}&pay_applydate={1}&pay_bankcode={2}&pay_callbackurl={3}&pay_memberid={4}&pay_notifyurl={5}&pay_orderid={6}&key={7}", amountStr, dateStr, bankcode, callbackurl, merchantId, notifyurl, orderCode, apikey)).ToUpper();

            YSWL.Log.LogHelper.AddInfoLog("FengHe-->signStrToMD5", String.Format("pay_amount={0}&pay_applydate={1}&pay_bankcode={2}&pay_callbackurl={3}&pay_memberid={4}&pay_notifyurl={5}&pay_orderid={6}&key={7}", amountStr, dateStr, bankcode, callbackurl, merchantId, notifyurl, orderCode, apikey));

            YSWL.Log.LogHelper.AddInfoLog("FengHe-->signStr", signStr);

         

            StringBuilder builder = new StringBuilder();
            builder.Append(StringHelper.CreateField("pay_memberid", merchantId.ToString(), get_code));
            builder.Append(StringHelper.CreateField("pay_orderid", orderCode, get_code));
            builder.Append(StringHelper.CreateField("pay_applydate", dateStr, get_code));
            builder.Append(StringHelper.CreateField("pay_bankcode", bankcode, get_code));
            builder.Append(StringHelper.CreateField("pay_amount", amountStr, get_code));
            builder.Append(StringHelper.CreateField("pay_notifyurl", notifyurl, get_code));
            builder.Append(StringHelper.CreateField("pay_callbackurl", callbackurl, get_code)); 
            builder.Append(StringHelper.CreateField("pay_reserved1", "", get_code));
            builder.Append(StringHelper.CreateField("pay_reserved2", "", get_code));
            builder.Append(StringHelper.CreateField("pay_reserved3", "", get_code));
            builder.Append(StringHelper.CreateField("pay_productname", remark, get_code));
            builder.Append(StringHelper.CreateField("pay_productnum", "", get_code));
            builder.Append(StringHelper.CreateField("pay_productdesc", remark, get_code));
            builder.Append(StringHelper.CreateField("pay_producturl", "", get_code));
            builder.Append(StringHelper.CreateField("pay_md5sign", signStr, get_code));

            StringHelper.SubmitPaymentForm(StringHelper.CreateForm(builder.ToString(), gatewayUrl));
            return "";


        }

        public static bool VerifyNotify(FengHeNotify notifyinfo)
        {

            //-------记录请求参数
            YSWL.Log.LogHelper.AddInfoLog("FengHe-->VerifyNotify", String.Format("参数为：memberid-->{0}&returncode-->{1}&orderid-->{2}&amount-->{3}&datetime-->{4}&transaction_id-->{5}&attach-->{6}&sign--->{7}", notifyinfo.memberid, notifyinfo.returncode, notifyinfo.orderid, notifyinfo.amount, notifyinfo.datetime, notifyinfo.transaction_id, notifyinfo.attach, notifyinfo.sign));

            if (String.IsNullOrWhiteSpace(notifyinfo.memberid) || String.IsNullOrWhiteSpace(notifyinfo.orderid))
            {
                ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单【FengHe支付回调通知失败"), "参数错误");
                return false;
            }


            string signStr = StringHelper.GetMD5(String.Format("amount={0}&datetime={1}&memberid={2}&orderid={3}&returncode={4}&transaction_id={5}&key={6}", notifyinfo.amount, notifyinfo.datetime, notifyinfo.memberid, notifyinfo.orderid, notifyinfo.returncode, notifyinfo.transaction_id, apikey)).ToUpper(); 
            if (signStr == notifyinfo.sign)
            {
                if (notifyinfo.returncode == "00")
                {
                    ColoPay.BLL.Pay.Order orderBll = new BLL.Pay.Order();
                    ColoPay.Model.Pay.Order orderInfo = orderBll.GetModel(notifyinfo.transaction_id);
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
                            ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单【{0}】FengHe支付回调通知失败：{1}", orderInfo.OrderCode, ex.Message), ex.StackTrace);
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
                YSWL.Log.LogHelper.AddInfoLog(String.Format("订单【{0}】FengHe支付验证失败", notifyinfo.orderid), String.Format("参数为：memberid-->{0}&returncode-->{1}&orderid-->{2}&amount-->{3}&datetime-->{4}&transaction_id-->{5}&attach-->{6}&sign--->{7}", notifyinfo.memberid, notifyinfo.returncode, notifyinfo.orderid, notifyinfo.amount, notifyinfo.datetime, notifyinfo.transaction_id, notifyinfo.attach, notifyinfo.sign));
                //验证失败，记录日志
                ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单【{0}】FengHe支付验证失败", notifyinfo.orderid), String.Format("参数为：memberid-->{0}&returncode-->{1}&orderid-->{2}&amount-->{3}&datetime-->{4}&transaction_id-->{5}&attach-->{6}&sign--->{7}", notifyinfo.memberid, notifyinfo.returncode, notifyinfo.orderid, notifyinfo.amount, notifyinfo.datetime, notifyinfo.transaction_id, notifyinfo.attach, notifyinfo.sign));
                return false;
            }
        }
    }
}