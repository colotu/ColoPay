using ColoPay.WebApi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ColoPay.WebApi.PayApi
{
    public class EnterpriseNotify
    {

        public static void Notify(ColoPay.Model.Pay.Order orderInfo)
        {

            var request = (HttpWebRequest)WebRequest.Create(orderInfo.AppNotifyUrl);
            StringBuilder builder = new StringBuilder();
            builder.Append(StringHelper.CreateField("appid", orderInfo.AppId));
            builder.Append(StringHelper.CreateField("secrit", orderInfo.AppSecrit));
            builder.Append(StringHelper.CreateField("order_no", orderInfo.EnterOrder));
            builder.Append(StringHelper.CreateField("amount", orderInfo.Amount.ToString()));
            builder.Append(StringHelper.CreateField("sdorder_no", orderInfo.OrderCode));
            builder.Append(StringHelper.CreateField("paytype", orderInfo.PaymentGateway));
            builder.Append(StringHelper.CreateField("status", orderInfo.PaymentStatus.ToString()));
            string postData = builder.ToString().Substring(1);
            var data = Encoding.UTF8.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            } 
            var response = (HttpWebResponse)request.GetResponse();
            string responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
            if (responseString == "success")//如果是返回成功，则说明已经异步通知了，需要更新本地的订单状态
            {
                ColoPay.BLL.Pay.Order orderBll = new ColoPay.BLL.Pay.Order();
                //更新同步状态
                orderBll.HasNotify(orderInfo.OrderId);
            }
            return ;
        }
    }
}