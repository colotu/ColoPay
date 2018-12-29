/**  版本信息模板在安装目录下，可自行修改。
* PaymentTypes.cs
*
* 功 能： N/A
* 类 名： PaymentTypes
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
	/// PaymentTypes:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PaymentTypes
	{
		public PaymentTypes()
		{}
		#region Model
		private int _modeid;
		private string _merchantcode;
		private string _emailaddress;
		private string _secretkey;
		private string _secondkey;
		private string _password;
		private string _partner;
		private string _name;
		private string _description;
		private string _gateway;
		private int _displaysequence;
		private decimal _charge;
		private bool _ispercent;
		private bool _allowrecharge;
		private string _logo;
		private string _drivepath="|1|";
		/// <summary>
		/// 
		/// </summary>
		public int ModeId
		{
			set{ _modeid=value;}
			get{return _modeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MerchantCode
		{
			set{ _merchantcode=value;}
			get{return _merchantcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmailAddress
		{
			set{ _emailaddress=value;}
			get{return _emailaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SecretKey
		{
			set{ _secretkey=value;}
			get{return _secretkey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SecondKey
		{
			set{ _secondkey=value;}
			get{return _secondkey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Partner
		{
			set{ _partner=value;}
			get{return _partner;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Gateway
		{
			set{ _gateway=value;}
			get{return _gateway;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DisplaySequence
		{
			set{ _displaysequence=value;}
			get{return _displaysequence;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Charge
		{
			set{ _charge=value;}
			get{return _charge;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsPercent
		{
			set{ _ispercent=value;}
			get{return _ispercent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool AllowRecharge
		{
			set{ _allowrecharge=value;}
			get{return _allowrecharge;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Logo
		{
			set{ _logo=value;}
			get{return _logo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DrivePath
		{
			set{ _drivepath=value;}
			get{return _drivepath;}
		}
		#endregion Model

	}
}

