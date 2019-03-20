/**
* SendPaymentHandler.cs
*
* 功 能： 发送支付请求
* 类 名： SendPaymentHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/05/14 17:28:15  Ben    初版
*
* Copyright (c) 2012 YS56 Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：云商未来（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using YSWL.Web;


using ColoPay.Model.Pay;
namespace ColoPay.Web.Handlers.Pay
{
    /// <summary>
    /// 发送支付请求
    /// </summary>
    public class SendPaymentHandler : YSWL.Payment.Handler.SendPaymentHandlerBase<Order>
    {
        #region 成员
        private readonly BLL.Pay.Order _orderManage = new BLL.Pay.Order();
        private const string MSG_ERRORLOG =
            "SendPaymentHandler >> Verificationn[{0}] 操作用户[{1}] 已阻止非法方式支付订单!";
        public const string KEY_ORDERID = "SendPayment_OrderId";
        public const string KEY_ACCESSMETHOD = "SendPayment_AccessMethod";//访问方式  手机还是电脑

        #endregion

        #region 构造
        public SendPaymentHandler()
            : base(new PaymentOption())
        {
            #region 设置网站名称
          
            base.HostName = "银河支付";
            #endregion
        }
        #endregion

        #region 验证请求是否合法
        /// <summary>
        /// 验证请求是否合法
        /// </summary>
        protected override bool VerifySendPayment(System.Web.HttpContext context)
        {
            #region 验证请求是否合法
            string[] orderIds = YSWL.Payment.OrderProcessor.GetQueryString4OrderIds(context.Request);
            if (orderIds == null || orderIds.Length < 1) return false;
            int orderId = YSWL.Common.Globals.SafeInt(orderIds[0], -1);
            if (orderId < -1) return false;

          
           Model.Pay.Order orderInfo = _orderManage.GetModel(orderId);
           

            YSWL.Payment.Model.PaymentModeInfo paymentMode =
                YSWL.Payment.BLL.PaymentModeManage.GetPaymentModeById(orderInfo.PaymentTypeId);
            if (paymentMode == null)
            {
                Web.LogHelp.AddErrorLog(string.Format(MSG_ERRORLOG, orderId, -1),
                    "非法操作订单", "Shop >> SendPaymentHandler >> Verification >> PaymentModeInfo Is NULL");
                context.Response.Redirect("/");
                return false;
            }
            #endregion

            string basePath = "/";
            string u = context.Request.ServerVariables["HTTP_USER_AGENT"];

            string area = context.Request.QueryString["Area"];
            if (!string.IsNullOrWhiteSpace(area))
            {
                basePath = string.Format("/{0}/", area);
            }
            //向网关写入请求发起源的Area
#pragma warning disable CS0612 // “SendPaymentHandlerBase<Order>.GatewayDatas”已过时
            this.GatewayDatas.Add(area);
#pragma warning restore CS0612 // “SendPaymentHandlerBase<Order>.GatewayDatas”已过时

            #region 支付宝银联

            if (paymentMode.Gateway == "alipaybank")
            {
                /** 
                * 关于银行编码：
                * 如： 招商银行【CMB】、中国建设银行【CCB】、中国工商银行【ICBCB2C】
                * 注意：优先使用B2C通道
                * 混合渠道: https://doc.open.alipay.com/doc2/detail.htm?spm=0.0.0.0.Nz80L8&treeId=63&articleId=103763&docType=1 
                * 纯借记卡渠道: https://doc.open.alipay.com/doc2/detail.htm?spm=0.0.0.0.1NpxKf&treeId=63&articleId=103764&docType=1
                **/

                string bankCode = context.Request.QueryString["BankCode"];
                if (!string.IsNullOrWhiteSpace(bankCode))
                {
                    //向网关写入用户选择的银行编码
#pragma warning disable CS0612 // “SendPaymentHandlerBase<Order>.GatewayDatas”已过时
                    this.GatewayDatas.Add(bankCode);
#pragma warning restore CS0612 // “SendPaymentHandlerBase<Order>.GatewayDatas”已过时
                }

            }

            #endregion

            //微信支付 向网关写入 APPID OPENID
            //if (paymentMode.Gateway.StartsWith("wechat"))
            //{
            //    string action = context.Request.QueryString["action"];
            //    //微信支付电脑端定向到
            //    if (action != "qr" && !u.ToLower().Contains("android") && !u.ToLower().Contains("mobile"))
            //    {
            //        context.Response.Redirect(MvcApplication.GetCurrentRoutePath(AreaRoute.Shop) + "PayWeChat/Pay/"+ orderId);
            //        return false;
            //    }

            //    //微信支付app端定向
            //    if (string.IsNullOrWhiteSpace(action) && u.ToLower().Contains("ys56"))
            //    {
            //        context.Response.Redirect($"/pay/certification{orderId}/{area}?action=app");
            //        return false;
            //    }

            //    string weChatAppId = YSWL.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", -1, "AA");
            //    if (string.IsNullOrWhiteSpace(weChatAppId))
            //    {
            //        context.Response.Clear();
            //        context.Response.Write("NO WECHAT_APPID > WECHAT APPID IS NULL!");
            //        return false;
            //    }
            //    this.GatewayDatas.Add(weChatAppId);

            //    if (string.IsNullOrWhiteSpace(action) || action == "show")
            //    {
            //        #region 获取微信用户OpenId
            //        //获取微信用户OpenId
            //        string weChatOpenId = YSWL.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, "AA");
            //        string weChatAppSercet = YSWL.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", -1, "AA");
            //        if (string.IsNullOrWhiteSpace(weChatOpenId) || string.IsNullOrWhiteSpace(weChatAppSercet))
            //        {
            //            context.Response.Clear();
            //            context.Response.Write("NO WECHATINFO > WECHAT WECHAT_OPENID OR WECHAT_APPSERCET IS NULL!");
            //            return false;
            //        }
            //        string authorizeCode = context.Request.QueryString["code"];
            //        if (string.IsNullOrWhiteSpace(authorizeCode))
            //        {
            //            string authorizeUrl =
            //               string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state={2}#wechat_redirect"
            //               , weChatAppId, Common.Globals.UrlEncode(context.Request.Url.ToString()), "YS56BEN");
            //            context.Response.Redirect(authorizeUrl);
            //            return false;
            //        }

            //        string userOpenId = YSWL.WeChat.BLL.Core.Utils.GetUserOpenId(weChatAppId, weChatAppSercet, authorizeCode);
            //        if (string.IsNullOrWhiteSpace(userOpenId))
            //        {
            //            context.Response.Clear();
            //            context.Response.Write("NO USEROPENID > WECHAT USEROPENID IS NULL!");
            //            return false;
            //        }
            //        this.GatewayDatas.Add(userOpenId);
            //        #endregion
            //    }
            //}

            if (u.ToLower().Contains("android") || u.ToLower().Contains("mobile"))//手机访问
            {
                if (!paymentMode.DrivePath.Contains("|2|"))//不能手机支付
                {
                    context.Session[KEY_ORDERID] = orderInfo.OrderId.ToString();
                    context.Response.Redirect("/m/PayResult/MFail");
                    return false;
                }
            }
            else//电脑访问
            {
                if (!paymentMode.DrivePath.Contains("|1|")) //不能电脑支付
                {
                    context.Session[KEY_ORDERID] = orderInfo.OrderId.ToString();
                    context.Response.Redirect(MvcApplication.GetCurrentRoutePath(AreaRoute.Shop) + "PayResult/MFail");
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 美化交易信息

        protected override YSWL.Payment.Model.TradeInfo GetTrade(string orderIdStr,
            decimal orderTotal, Order order)
        {
            //return base.GetTrade(orderIdStr, orderTotal, order);

            YSWL.Payment.Model.TradeInfo info = new YSWL.Payment.Model.TradeInfo();
            info.Body = HostName + "订单-订单号：[" + order.OrderCode + "]";
            info.BuyerEmailAddress = order.BuyerEmail;
            info.Date = order.OrderDate;
            info.OrderId = orderIdStr;
            info.Showurl = YSWL.Common.Globals.HostPath(System.Web.HttpContext.Current.Request.Url);
            info.Subject = HostName + "订单-订单号：[" + order.OrderCode + "]-" +
                           "在线支付-订单支付金额" +
                           "：" + orderTotal.ToString("0.00") + "元";
            info.TotalMoney = orderTotal;
            return info;
        }

        #endregion
    }
}