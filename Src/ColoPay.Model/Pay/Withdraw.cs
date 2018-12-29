/**  版本信息模板在安装目录下，可自行修改。
* Withdraw.cs
*
* 功 能： N/A
* 类 名： Withdraw
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
	/// Withdraw:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Withdraw
	{
		public Withdraw()
		{}
		#region Model
		private int _withdrawid;
		private int _enterpriseid;
		private decimal _amount;
		private string _withdrawbank;
		private string _withdrawinfo;
		private string _withdrawnum;
		private int _status;
		private DateTime _createddate;
		private int _createduserid;
		private DateTime? _auditdate;
		private int _audituserid;
		private DateTime? _paydate;
		private int _payuserid;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public int WithdrawId
		{
			set{ _withdrawid=value;}
			get{return _withdrawid;}
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
		/// 提现金额
		/// </summary>
		public decimal Amount
		{
			set{ _amount=value;}
			get{return _amount;}
		}
		/// <summary>
		/// 提现银行
		/// </summary>
		public string WithdrawBank
		{
			set{ _withdrawbank=value;}
			get{return _withdrawbank;}
		}
		/// <summary>
		/// 提现开户行信息
		/// </summary>
		public string WithdrawInfo
		{
			set{ _withdrawinfo=value;}
			get{return _withdrawinfo;}
		}
		/// <summary>
		/// 提现开户行账号
		/// </summary>
		public string WithdrawNum
		{
			set{ _withdrawnum=value;}
			get{return _withdrawnum;}
		}
		/// <summary>
		/// 状态 0：提交  1：审核 2：已打款
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 创建人
		/// </summary>
		public int CreatedUserId
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 审核时间
		/// </summary>
		public DateTime? AuditDate
		{
			set{ _auditdate=value;}
			get{return _auditdate;}
		}
		/// <summary>
		/// 审核人员
		/// </summary>
		public int AuditUserId
		{
			set{ _audituserid=value;}
			get{return _audituserid;}
		}
		/// <summary>
		/// 付款时间
		/// </summary>
		public DateTime? PayDate
		{
			set{ _paydate=value;}
			get{return _paydate;}
		}
		/// <summary>
		/// 付款人员
		/// </summary>
		public int PayUserId
		{
			set{ _payuserid=value;}
			get{return _payuserid;}
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

