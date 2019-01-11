/**
* PaymentNotifyHandler.cs
*
* 功 能： 支付异步通知处理
* 类 名： PaymentNotifyHandler
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


using ColoPay.Model.Pay;
namespace ColoPay.Web.Handlers.Pay
{
    /// <summary>
    /// 支付异步通知处理
    /// </summary>
    public class PaymentNotifyHandler : YSWL.Payment.Handler.PaymentReturnHandlerBase<Order>
    {
        /// <summary>
        /// 构造回调 设置为异步模式
        /// </summary>
        public PaymentNotifyHandler() : base(new PaymentOption(), true) { }
    }
}