/**  版本信息模板在安装目录下，可自行修改。
* BalanceDetail.cs
*
* 功 能： N/A
* 类 名： BalanceDetail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018/12/30 21:54:34   N/A    初版
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
	/// BalanceDetail:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BalanceDetail
	{
		public BalanceDetail()
		{}
		#region Model
		private int _detailid;
		private int _enterpriseid;
		private int _agentid=0;
		private int _type=0;
		private int _paytype;
		private int _originalid;
		private string _originalcode;
		private decimal _paymentfee;
		private decimal _orderamount;
		private decimal _amount;
		private DateTime _createdtime;
		/// <summary>
		/// 
		/// </summary>
		public int DetailId
		{
			set{ _detailid=value;}
			get{return _detailid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int EnterpriseID
		{
			set{ _enterpriseid=value;}
			get{return _enterpriseid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int AgentId
		{
			set{ _agentid=value;}
			get{return _agentid;}
		}
		/// <summary>
		/// 明细类型  0：商家用户 1：代理商
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 支付方式  0：支付  1：提现
		/// </summary>
		public int PayType
		{
			set{ _paytype=value;}
			get{return _paytype;}
		}
		/// <summary>
		/// 原始ID，对应OrderID  或者 WithdrawId
		/// </summary>
		public int OriginalId
		{
			set{ _originalid=value;}
			get{return _originalid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OriginalCode
		{
			set{ _originalcode=value;}
			get{return _originalcode;}
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
		/// 订单金额  订单金额=实际支付金额-支付手续费
		/// </summary>
		public decimal OrderAmount
		{
			set{ _orderamount=value;}
			get{return _orderamount;}
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
		/// 
		/// </summary>
		public DateTime CreatedTime
		{
			set{ _createdtime=value;}
			get{return _createdtime;}
		}
		#endregion Model

	}
}

