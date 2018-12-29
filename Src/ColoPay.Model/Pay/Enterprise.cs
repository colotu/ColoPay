
using System;
namespace ColoPay.Model.Pay
{
	/// <summary>
	/// Enterprise:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Enterprise
	{
		public Enterprise()
		{}
		#region Model
		private int _enterpriseid;
		private string _username;
		private string _name;
		private string _simplename;
		private int _status;
		private string _enterprisenum;
		private string _businesslicense;
		private string _telphone;
		private string _cellphone;
		private string _accountbank;
		private string _accountinfo;
		private string _accountnum;
		private string _withdrawbank;
		private string _withdrawinfo;
		private string _withdrawnum;
		private decimal _balance;
		private string _appid;
		private string _appsecrit;
		private string _appurl;
		private string _appreturnurl;
		private string _contactmail;
		private string _address;
		private int _enterank;
		private DateTime _createddate;
		private int _createduserid;
		private string _registerip;
		private string _remark;
		/// <summary>
		/// 企业ID
		/// </summary>
		public int EnterpriseID
		{
			set{ _enterpriseid=value;}
			get{return _enterpriseid;}
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
		/// 企业名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 企业简称
		/// </summary>
		public string SimpleName
		{
			set{ _simplename=value;}
			get{return _simplename;}
		}
		/// <summary>
		/// 状态  0：冻结  1：正常
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 商户号
		/// </summary>
		public string EnterpriseNum
		{
			set{ _enterprisenum=value;}
			get{return _enterprisenum;}
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
		/// 商家余额
		/// </summary>
		public decimal Balance
		{
			set{ _balance=value;}
			get{return _balance;}
		}
		/// <summary>
		/// 商家AppId
		/// </summary>
		public string AppId
		{
			set{ _appid=value;}
			get{return _appid;}
		}
		/// <summary>
		/// 密钥
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
		/// 
		/// </summary>
		public int EnteRank
		{
			set{ _enterank=value;}
			get{return _enterank;}
		}
		/// <summary>
		/// 商户等级 0：普通商户
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
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

