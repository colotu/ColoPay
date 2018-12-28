﻿/**
* SendRechargeHandlerBase.cs
*
* 功 能： 充值接口抽象基类
* 类 名： SendRechargeHandlerBase
*
* Ver   变更日期    部门      担当者 变更内容
* ─────────────────────────────────
* V0.01 2012/01/13  研发部    姚远   初版
* V1.50 2012/10/11  研发部    姚远   增加接口实现类, 减少其他模块耦合度
* V1.82 2013/05/07  研发部    姚远   增加测试模式
* V2.02 2014/01/10  研发部    姚远   新增网关自定义(动态)参数功能
*                                    并对网关信息以特殊Base64密文传输
* V2.31 2016/08/18  研发部    姚远   升级项目至.NET Framework 4.5.2
*                                    支付模块扩展改造, 支持二维码支付等功能
*                                    优化支付/充值验证逻辑
*
* Copyright (c) 2017 YSWL Corporation. All rights reserved.
*┌─────────────────────────────────┐
*│ 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露． │
*│ 版权所有：云商未来（北京）科技有限公司                           │
*└─────────────────────────────────┘
*/
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using YSWL.Payment.BLL;
using YSWL.Payment.Configuration;
using YSWL.Payment.Core;
using YSWL.Payment.Model;

namespace YSWL.Payment.Handler
{

    /// <summary>
    /// 支付请求接口抽象基类
    /// </summary> 
    public abstract class SendRechargeHandlerBase :
        SendRechargeHandlerBase<RechargeRequestInfo, UserInfo>
    {
        /// <summary>
        /// 构造充值请求接口
        /// </summary>
        /// <param name="option">充值网关回调URL参数</param>
        protected SendRechargeHandlerBase(
            IRechargeOption<RechargeRequestInfo, UserInfo> option)
            : base(option)
        {
        }
    }

    /// <summary>
    /// 充值接口抽象基类
    /// </summary>
    /// <typeparam name="T">充值信息</typeparam>
    /// <typeparam name="U">用户信息</typeparam>
    public abstract class SendRechargeHandlerBase<T, U> : IHttpHandler, IRequiresSessionState
        where T : class, IRechargeRequest, new()
        where U : class, IUserInfo, new()
    {
        protected string HostName = "YSWL ";
        protected IRechargeOption<T, U> Option;
        protected List<string> GatewayDatas = new List<string>();

        /// <summary>
        /// 构造充值请求接口
        /// </summary>
        /// <param name="option">充值网关回调URL参数</param>
        protected SendRechargeHandlerBase(IRechargeOption<T, U> option)
        {
            this.Option = option;
        }

        #region 获取支付网关
        /// <summary>
        /// 获取支付网关
        /// </summary>
        protected virtual Payment.Model.GatewayInfo GetGateway(string gatewayName)
        {
            Payment.Model.GatewayInfo info = new Payment.Model.GatewayInfo();

            //写入网关名称和附加参数
            GatewayDatas.Insert(0, gatewayName);

            #region 网关写入SaaS用户企业ID
            string tmpAutoTag = string.Empty;
            if (Components.MvcApplication.IsAutoConn)
            {
                tmpAutoTag = Common.CallContextHelper.GetAutoTag();
                if (string.IsNullOrWhiteSpace(tmpAutoTag))
                    Core.Globals.WriteText(new System.Text.StringBuilder("IsAutoConn SetAutoTag IS NULL"));
            }
            GatewayDatas.Insert(2, tmpAutoTag);
            #endregion

            //针对URL 特殊Base64方法
            info.Data = Common.DEncrypt.Base64.Encode4Url(string.Join("|", GatewayDatas));
            info.DataList = GatewayDatas;
            info.ReturnUrl = Common.Globals.FullPath(string.Format(Option.ReturnUrl, info.Data));
            info.NotifyUrl = Common.Globals.FullPath(string.Format(Option.NotifyUrl, info.Data));
            return info;
        }

        #endregion

        #region 获取收款人信息
        /// <summary>
        /// 获取收款人信息
        /// </summary>
        protected virtual PayeeInfo GetPayee(PaymentModeInfo paymode)
        {
            if (paymode == null)
            {
                return null;
            }
            PayeeInfo info = new PayeeInfo();
            info.EmailAddress = paymode.EmailAddress;
            info.Partner = paymode.Partner;
            info.Password = paymode.Password;
            info.PrimaryKey = paymode.SecretKey;
            info.SecondKey = paymode.SecondKey;
            info.SellerAccount = paymode.MerchantCode;
            return info;
        }
        #endregion

        #region 获取交易信息

        /// <summary>
        /// 获取交易信息
        /// </summary>
        /// <param name="rechargeRequest">充值信息</param>
        /// <param name="payCharge">支付手续费</param>
        /// <param name="user">用户</param>
        protected virtual TradeInfo GetTrade(T rechargeRequest, decimal payCharge, U user)
        {
            decimal totalMoney = rechargeRequest.RechargeBlance + payCharge;
            string orderId = rechargeRequest.RechargeId.ToString(CultureInfo.InvariantCulture);
            TradeInfo info = new TradeInfo();
            info.BuyerEmailAddress = user.Email;
            info.Date = rechargeRequest.TradeDate;
            info.OrderId = orderId;
            info.Showurl = Globals.HostPath(HttpContext.Current.Request.Url);
            info.Subject = HostName + "在线充值: " + orderId;
            info.Body = HostName + "在线充值: " + orderId + " 金额: " + totalMoney.ToString("F2");
            info.TotalMoney = totalMoney;
            return info;
        }
        #endregion

        #region 验证支付请求是否合法

        /// <summary>
        /// 验证支付请求是否合法
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>是否合法</returns>
        protected virtual bool VerifySendPayment(HttpContext context)
        {
            return true;
        }

        #endregion

        #region IHttpHandler 成员

        public virtual bool IsReusable
        {
            get { return false; }
        }

        public virtual void ProcessRequest(HttpContext context)
        {
            //Safe
            if (!VerifySendPayment(context)) return;

            //充值ID
            long rechargeId = Globals.SafeLong(context.Request.QueryString["RechargeId"], 0);
            if (rechargeId == 0) return;

            T rechargeRequest = Option.GetRechargeRequest(rechargeId);
            if (rechargeRequest == null || rechargeRequest.PaymentTypeId < 1) return;

            PaymentModeInfo paymentMode = PaymentModeManage.GetPaymentModeById(rechargeRequest.PaymentTypeId);
            if (paymentMode == null) return;

            U user = Option.GetCurrentUser(context);
            if (user == null) return;

            PayConfiguration config = PayConfiguration.GetConfig();
            if (config == null) return;

            string getwayName = paymentMode.Gateway.ToLower();
            GatewayProvider provider = config.Providers[getwayName] as GatewayProvider;
            if (provider == null) return;

            //计算支付手续费
            decimal payCharge = paymentMode.CalcPayCharge(rechargeRequest.RechargeBlance);
            //TODO: 根据多币种货币换算, 计算手续费
            //decimal payCharge = Sales.ScaleMoney(paymentMode.CalcPayCharge(balance));

            //支付网关
            GatewayInfo gatewayInfo = this.GetGateway(getwayName);

            //交易信息
            TradeInfo tradeInfo = this.GetTrade(rechargeRequest, payCharge, user);

            #region 测试模式
            //DONE: 测试模式埋点
            if (Globals.IsRechargeTestMode && !Globals.ExcludeGateway.Contains(getwayName.ToLower()))
            {
                System.Text.StringBuilder url = new System.Text.StringBuilder(gatewayInfo.ReturnUrl);
                url.AppendFormat("&out_trade_no={0}", tradeInfo.OrderId);
                url.AppendFormat("&total_fee={0}", tradeInfo.TotalMoney);
                url.AppendFormat("&sign={0}", Globals.GetMd5(System.Text.Encoding.UTF8, url.ToString()));
                HttpContext.Current.Response.Redirect(
                    gatewayInfo.ReturnUrl.Contains("?")
                        ? url.ToString()
                        : url.ToString().Replace("&out_trade_no", "?out_trade_no"), true);
                return;
            }
            #endregion

            PaymentRequest.Instance(
                provider.RequestType,
                this.GetPayee(paymentMode),
                gatewayInfo,
                tradeInfo
                ).SendRequest();
        }
        #endregion
    }
}