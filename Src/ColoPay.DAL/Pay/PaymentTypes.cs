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
using System.Data;
using System.Text;
using System.Data.SqlClient;
using YSWL.DBUtility;//Please add references
namespace ColoPay.DAL.Pay
{
	/// <summary>
	/// 数据访问类:PaymentTypes
	/// </summary>
	public partial class PaymentTypes
	{
		public PaymentTypes()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ModeId", "Pay_PaymentTypes"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ModeId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Pay_PaymentTypes");
			strSql.Append(" where ModeId=@ModeId");
			SqlParameter[] parameters = {
					new SqlParameter("@ModeId", SqlDbType.Int,4)
			};
			parameters[0].Value = ModeId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ColoPay.Model.Pay.PaymentTypes model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Pay_PaymentTypes(");
			strSql.Append("MerchantCode,EmailAddress,SecretKey,SecondKey,Password,Partner,Name,Description,Gateway,DisplaySequence,Charge,IsPercent,AllowRecharge,Logo,DrivePath)");
			strSql.Append(" values (");
			strSql.Append("@MerchantCode,@EmailAddress,@SecretKey,@SecondKey,@Password,@Partner,@Name,@Description,@Gateway,@DisplaySequence,@Charge,@IsPercent,@AllowRecharge,@Logo,@DrivePath)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@MerchantCode", SqlDbType.NVarChar,300),
					new SqlParameter("@EmailAddress", SqlDbType.NVarChar,255),
					new SqlParameter("@SecretKey", SqlDbType.NVarChar,4000),
					new SqlParameter("@SecondKey", SqlDbType.NVarChar,4000),
					new SqlParameter("@Password", SqlDbType.NVarChar,4000),
					new SqlParameter("@Partner", SqlDbType.NVarChar,300),
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.NText),
					new SqlParameter("@Gateway", SqlDbType.NVarChar,200),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Charge", SqlDbType.Money,8),
					new SqlParameter("@IsPercent", SqlDbType.Bit,1),
					new SqlParameter("@AllowRecharge", SqlDbType.Bit,1),
					new SqlParameter("@Logo", SqlDbType.NVarChar,255),
					new SqlParameter("@DrivePath", SqlDbType.NVarChar,255)};
			parameters[0].Value = model.MerchantCode;
			parameters[1].Value = model.EmailAddress;
			parameters[2].Value = model.SecretKey;
			parameters[3].Value = model.SecondKey;
			parameters[4].Value = model.Password;
			parameters[5].Value = model.Partner;
			parameters[6].Value = model.Name;
			parameters[7].Value = model.Description;
			parameters[8].Value = model.Gateway;
			parameters[9].Value = model.DisplaySequence;
			parameters[10].Value = model.Charge;
			parameters[11].Value = model.IsPercent;
			parameters[12].Value = model.AllowRecharge;
			parameters[13].Value = model.Logo;
			parameters[14].Value = model.DrivePath;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ColoPay.Model.Pay.PaymentTypes model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Pay_PaymentTypes set ");
			strSql.Append("MerchantCode=@MerchantCode,");
			strSql.Append("EmailAddress=@EmailAddress,");
			strSql.Append("SecretKey=@SecretKey,");
			strSql.Append("SecondKey=@SecondKey,");
			strSql.Append("Password=@Password,");
			strSql.Append("Partner=@Partner,");
			strSql.Append("Name=@Name,");
			strSql.Append("Description=@Description,");
			strSql.Append("Gateway=@Gateway,");
			strSql.Append("DisplaySequence=@DisplaySequence,");
			strSql.Append("Charge=@Charge,");
			strSql.Append("IsPercent=@IsPercent,");
			strSql.Append("AllowRecharge=@AllowRecharge,");
			strSql.Append("Logo=@Logo,");
			strSql.Append("DrivePath=@DrivePath");
			strSql.Append(" where ModeId=@ModeId");
			SqlParameter[] parameters = {
					new SqlParameter("@MerchantCode", SqlDbType.NVarChar,300),
					new SqlParameter("@EmailAddress", SqlDbType.NVarChar,255),
					new SqlParameter("@SecretKey", SqlDbType.NVarChar,4000),
					new SqlParameter("@SecondKey", SqlDbType.NVarChar,4000),
					new SqlParameter("@Password", SqlDbType.NVarChar,4000),
					new SqlParameter("@Partner", SqlDbType.NVarChar,300),
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@Description", SqlDbType.NText),
					new SqlParameter("@Gateway", SqlDbType.NVarChar,200),
					new SqlParameter("@DisplaySequence", SqlDbType.Int,4),
					new SqlParameter("@Charge", SqlDbType.Money,8),
					new SqlParameter("@IsPercent", SqlDbType.Bit,1),
					new SqlParameter("@AllowRecharge", SqlDbType.Bit,1),
					new SqlParameter("@Logo", SqlDbType.NVarChar,255),
					new SqlParameter("@DrivePath", SqlDbType.NVarChar,255),
					new SqlParameter("@ModeId", SqlDbType.Int,4)};
			parameters[0].Value = model.MerchantCode;
			parameters[1].Value = model.EmailAddress;
			parameters[2].Value = model.SecretKey;
			parameters[3].Value = model.SecondKey;
			parameters[4].Value = model.Password;
			parameters[5].Value = model.Partner;
			parameters[6].Value = model.Name;
			parameters[7].Value = model.Description;
			parameters[8].Value = model.Gateway;
			parameters[9].Value = model.DisplaySequence;
			parameters[10].Value = model.Charge;
			parameters[11].Value = model.IsPercent;
			parameters[12].Value = model.AllowRecharge;
			parameters[13].Value = model.Logo;
			parameters[14].Value = model.DrivePath;
			parameters[15].Value = model.ModeId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ModeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Pay_PaymentTypes ");
			strSql.Append(" where ModeId=@ModeId");
			SqlParameter[] parameters = {
					new SqlParameter("@ModeId", SqlDbType.Int,4)
			};
			parameters[0].Value = ModeId;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string ModeIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Pay_PaymentTypes ");
			strSql.Append(" where ModeId in ("+ModeIdlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ColoPay.Model.Pay.PaymentTypes GetModel(int ModeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ModeId,MerchantCode,EmailAddress,SecretKey,SecondKey,Password,Partner,Name,Description,Gateway,DisplaySequence,Charge,IsPercent,AllowRecharge,Logo,DrivePath from Pay_PaymentTypes ");
			strSql.Append(" where ModeId=@ModeId");
			SqlParameter[] parameters = {
					new SqlParameter("@ModeId", SqlDbType.Int,4)
			};
			parameters[0].Value = ModeId;

			ColoPay.Model.Pay.PaymentTypes model=new ColoPay.Model.Pay.PaymentTypes();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ColoPay.Model.Pay.PaymentTypes DataRowToModel(DataRow row)
		{
			ColoPay.Model.Pay.PaymentTypes model=new ColoPay.Model.Pay.PaymentTypes();
			if (row != null)
			{
				if(row["ModeId"]!=null && row["ModeId"].ToString()!="")
				{
					model.ModeId=int.Parse(row["ModeId"].ToString());
				}
				if(row["MerchantCode"]!=null)
				{
					model.MerchantCode=row["MerchantCode"].ToString();
				}
				if(row["EmailAddress"]!=null)
				{
					model.EmailAddress=row["EmailAddress"].ToString();
				}
				if(row["SecretKey"]!=null)
				{
					model.SecretKey=row["SecretKey"].ToString();
				}
				if(row["SecondKey"]!=null)
				{
					model.SecondKey=row["SecondKey"].ToString();
				}
				if(row["Password"]!=null)
				{
					model.Password=row["Password"].ToString();
				}
				if(row["Partner"]!=null)
				{
					model.Partner=row["Partner"].ToString();
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["Description"]!=null)
				{
					model.Description=row["Description"].ToString();
				}
				if(row["Gateway"]!=null)
				{
					model.Gateway=row["Gateway"].ToString();
				}
				if(row["DisplaySequence"]!=null && row["DisplaySequence"].ToString()!="")
				{
					model.DisplaySequence=int.Parse(row["DisplaySequence"].ToString());
				}
				if(row["Charge"]!=null && row["Charge"].ToString()!="")
				{
					model.Charge=decimal.Parse(row["Charge"].ToString());
				}
				if(row["IsPercent"]!=null && row["IsPercent"].ToString()!="")
				{
					if((row["IsPercent"].ToString()=="1")||(row["IsPercent"].ToString().ToLower()=="true"))
					{
						model.IsPercent=true;
					}
					else
					{
						model.IsPercent=false;
					}
				}
				if(row["AllowRecharge"]!=null && row["AllowRecharge"].ToString()!="")
				{
					if((row["AllowRecharge"].ToString()=="1")||(row["AllowRecharge"].ToString().ToLower()=="true"))
					{
						model.AllowRecharge=true;
					}
					else
					{
						model.AllowRecharge=false;
					}
				}
				if(row["Logo"]!=null)
				{
					model.Logo=row["Logo"].ToString();
				}
				if(row["DrivePath"]!=null)
				{
					model.DrivePath=row["DrivePath"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ModeId,MerchantCode,EmailAddress,SecretKey,SecondKey,Password,Partner,Name,Description,Gateway,DisplaySequence,Charge,IsPercent,AllowRecharge,Logo,DrivePath ");
			strSql.Append(" FROM Pay_PaymentTypes ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ModeId,MerchantCode,EmailAddress,SecretKey,SecondKey,Password,Partner,Name,Description,Gateway,DisplaySequence,Charge,IsPercent,AllowRecharge,Logo,DrivePath ");
			strSql.Append(" FROM Pay_PaymentTypes ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Pay_PaymentTypes ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ModeId desc");
			}
			strSql.Append(")AS Row, T.*  from Pay_PaymentTypes T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "Pay_PaymentTypes";
			parameters[1].Value = "ModeId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

