/**  版本信息模板在安装目录下，可自行修改。
* Agent.cs
*
* 功 能： N/A
* 类 名： Agent
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2019/1/3 21:18:53   N/A    初版
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
	/// Agent:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Agent
	{
		public Agent()
		{}
		#region Model
		private int _agentid;
		private string _name;
		private string _username;
		private int _parentid;
		private int _status;
		private decimal _balance=0M;
		private DateTime _createddate= DateTime.Now;
		private int _createduserid;
		private string _businesslicense;
		private string _telphone;
		private string _cellphone;
		private string _accountbank;
		private string _accountinfo;
		private string _accountnum;
		private string _withdrawbank;
		private string _withdrawinfo;
		private string _withdrawnum;
		private string _contactmail;
		private string _address;
		private string _registerip;
		private string _remark;
		/// <summary>
		/// 代理商ID
		/// </summary>
		public int AgentId
		{
			set{ _agentid=value;}
			get{return _agentid;}
		}
		/// <summary>
		/// 代理商名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 登录用户
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 层级
		/// </summary>
		public int ParentId
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 状态 0：冻结  1：通过
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 余额
		/// </summary>
		public decimal Balance
		{
			set{ _balance=value;}
			get{return _balance;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CreatedUserId
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 证件编号
		/// </summary>
		public string BusinessLicense
		{
			set{ _businesslicense=value;}
			get{return _businesslicense;}
		}
		/// <summary>
		/// 企业电话
		/// </summary>
		public string TelPhone
		{
			set{ _telphone=value;}
			get{return _telphone;}
		}
		/// <summary>
		/// 手机号码
		/// </summary>
		public string CellPhone
		{
			set{ _cellphone=value;}
			get{return _cellphone;}
		}
		/// <summary>
		/// 银行名称
		/// </summary>
		public string AccountBank
		{
			set{ _accountbank=value;}
			get{return _accountbank;}
		}
		/// <summary>
		/// 开户行信息
		/// </summary>
		public string AccountInfo
		{
			set{ _accountinfo=value;}
			get{return _accountinfo;}
		}
		/// <summary>
		/// 银行账号
		/// </summary>
		public string AccountNum
		{
			set{ _accountnum=value;}
			get{return _accountnum;}
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
		/// 联系邮箱
		/// </summary>
		public string ContactMail
		{
			set{ _contactmail=value;}
			get{return _contactmail;}
		}
		/// <summary>
		/// 企业地址
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 注册IP
		/// </summary>
		public string RegisterIp
		{
			set{ _registerip=value;}
			get{return _registerip;}
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

