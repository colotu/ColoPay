﻿/**  版本信息模板在安装目录下，可自行修改。
* AgentPayFee.cs
*
* 功 能： N/A
* 类 名： AgentPayFee
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
	/// AgentPayFee:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class AgentPayFee
	{
		public AgentPayFee()
		{}
		#region Model
		private int _agentid;
		private int _paymodeid;
		private decimal _feerate;
		/// <summary>
		/// 
		/// </summary>
		public int AgentID
		{
			set{ _agentid=value;}
			get{return _agentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int PayModeId
		{
			set{ _paymodeid=value;}
			get{return _paymodeid;}
		}
		/// <summary>
		/// 费率  单位为%
		/// </summary>
		public decimal FeeRate
		{
			set{ _feerate=value;}
			get{return _feerate;}
		}
		#endregion Model

	}
}

