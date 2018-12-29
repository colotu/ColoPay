/**  版本信息模板在安装目录下，可自行修改。
* Order.cs
*
* 功 能： N/A
* 类 名： Order
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018/12/29 23:41:08   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace ColoPay.Model.Pay
{
	/// <summary>
	/// Order:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Order
	{
		public Order()
		{}
		#region Model
		private int _orderid;
		private string _ordercode;
		private string _enterorder;
		private int _enterpriseid;
		private decimal _amount;
		private decimal _paymentfee;
		private decimal _feerate;
		private decimal _orderamount;
		private int _paymodeid;
		private string _paymenttypename;
		private string _paymentgateway;
		private int _paymentstatus;
		private string _orderinfo;
		private string _appid;
		private string _appsecrit;
		private string _appurl;
		private string _appreturnurl;
		private DateTime _createdtime;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int OrderId
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 订单编号
		/// </summary>
		public string OrderCode
		{
			set{ _ordercode=value;}
			get{return _ordercode;}
		}
		/// <summary>
		/// 原商家订单
		/// </summary>
		public string EnterOrder
		{
			set{ _enterorder=value;}
			get{return _enterorder;}
		}
		/// <summary>
		/// 企业ID
		/// </summary>
		public int EnterpriseID
		{
			set{ _enterpriseid=value;}
			get{return _enterpriseid;}
		}
		/// <summary>
		/// 实际支付金额
		/// </summary>
		public decimal Amount
		{
			set{ _amount=value;}
			get{return _amount;}
		}
		/// <summary>
		/// 手续费
		/// </summary>
		public decimal PaymentFee
		{
			set{ _paymentfee=value;}
			get{return _paymentfee;}
		}
		/// <summary>
		/// 手续费比例   单位为%
		/// </summary>
		public decimal FeeRate
		{
			set{ _feerate=value;}
			get{return _feerate;}
		}
		/// <summary>
		/// 订单金额  订单金额=实际支付金额-支付手续费
		/// </summary>
		public decimal OrderAmount
		{
			set{ _orderamount=value;}
			get{return _orderamount;}
		}
		/// <summary>
		/// 支付类型
		/// </summary>
		public int PayModeId
		{
			set{ _paymodeid=value;}
			get{return _paymodeid;}
		}
		/// <summary>
		/// 支付方式名称
		/// </summary>
		public string PaymentTypeName
		{
			set{ _paymenttypename=value;}
			get{return _paymenttypename;}
		}
		/// <summary>
		/// 支付网关
		/// </summary>
		public string PaymentGateway
		{
			set{ _paymentgateway=value;}
			get{return _paymentgateway;}
		}
		/// <summary>
		/// 支付状态  0：未支付 1：支付中  2：完成支付
		/// </summary>
		public int PaymentStatus
		{
			set{ _paymentstatus=value;}
			get{return _paymentstatus;}
		}
		/// <summary>
		/// 订单支付信息
		/// </summary>
		public string OrderInfo
		{
			set{ _orderinfo=value;}
			get{return _orderinfo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AppId
		{
			set{ _appid=value;}
			get{return _appid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AppSecrit
		{
			set{ _appsecrit=value;}
			get{return _appsecrit;}
		}
		/// <summary>
		/// 应用地址
		/// </summary>
		public string AppUrl
		{
			set{ _appurl=value;}
			get{return _appurl;}
		}
		/// <summary>
		/// 应用回调地址
		/// </summary>
		public string AppReturnUrl
		{
			set{ _appreturnurl=value;}
			get{return _appreturnurl;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatedTime
		{
			set{ _createdtime=value;}
			get{return _createdtime;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

