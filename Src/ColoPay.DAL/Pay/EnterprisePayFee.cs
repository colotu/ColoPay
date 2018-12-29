/**  版本信息模板在安装目录下，可自行修改。
* EnterprisePayFee.cs
*
* 功 能： N/A
* 类 名： EnterprisePayFee
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
	/// 数据访问类:EnterprisePayFee
	/// </summary>
	public partial class EnterprisePayFee
	{
		public EnterprisePayFee()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("EnterpriseID", "Pay_EnterprisePayFee"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int EnterpriseID,int PayModeId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Pay_EnterprisePayFee");
			strSql.Append(" where EnterpriseID=@EnterpriseID and PayModeId=@PayModeId ");
			SqlParameter[] parameters = {
					new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
					new SqlParameter("@PayModeId", SqlDbType.Int,4)			};
			parameters[0].Value = EnterpriseID;
			parameters[1].Value = PayModeId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ColoPay.Model.Pay.EnterprisePayFee model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Pay_EnterprisePayFee(");
			strSql.Append("EnterpriseID,PayModeId,FeeRate)");
			strSql.Append(" values (");
			strSql.Append("@EnterpriseID,@PayModeId,@FeeRate)");
			SqlParameter[] parameters = {
					new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
					new SqlParameter("@PayModeId", SqlDbType.Int,4),
					new SqlParameter("@FeeRate", SqlDbType.Decimal,9)};
			parameters[0].Value = model.EnterpriseID;
			parameters[1].Value = model.PayModeId;
			parameters[2].Value = model.FeeRate;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(ColoPay.Model.Pay.EnterprisePayFee model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Pay_EnterprisePayFee set ");
			strSql.Append("FeeRate=@FeeRate");
			strSql.Append(" where EnterpriseID=@EnterpriseID and PayModeId=@PayModeId ");
			SqlParameter[] parameters = {
					new SqlParameter("@FeeRate", SqlDbType.Decimal,9),
					new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
					new SqlParameter("@PayModeId", SqlDbType.Int,4)};
			parameters[0].Value = model.FeeRate;
			parameters[1].Value = model.EnterpriseID;
			parameters[2].Value = model.PayModeId;

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
		public bool Delete(int EnterpriseID,int PayModeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Pay_EnterprisePayFee ");
			strSql.Append(" where EnterpriseID=@EnterpriseID and PayModeId=@PayModeId ");
			SqlParameter[] parameters = {
					new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
					new SqlParameter("@PayModeId", SqlDbType.Int,4)			};
			parameters[0].Value = EnterpriseID;
			parameters[1].Value = PayModeId;

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
		/// 得到一个对象实体
		/// </summary>
		public ColoPay.Model.Pay.EnterprisePayFee GetModel(int EnterpriseID,int PayModeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 EnterpriseID,PayModeId,FeeRate from Pay_EnterprisePayFee ");
			strSql.Append(" where EnterpriseID=@EnterpriseID and PayModeId=@PayModeId ");
			SqlParameter[] parameters = {
					new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
					new SqlParameter("@PayModeId", SqlDbType.Int,4)			};
			parameters[0].Value = EnterpriseID;
			parameters[1].Value = PayModeId;

			ColoPay.Model.Pay.EnterprisePayFee model=new ColoPay.Model.Pay.EnterprisePayFee();
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
		public ColoPay.Model.Pay.EnterprisePayFee DataRowToModel(DataRow row)
		{
			ColoPay.Model.Pay.EnterprisePayFee model=new ColoPay.Model.Pay.EnterprisePayFee();
			if (row != null)
			{
				if(row["EnterpriseID"]!=null && row["EnterpriseID"].ToString()!="")
				{
					model.EnterpriseID=int.Parse(row["EnterpriseID"].ToString());
				}
				if(row["PayModeId"]!=null && row["PayModeId"].ToString()!="")
				{
					model.PayModeId=int.Parse(row["PayModeId"].ToString());
				}
				if(row["FeeRate"]!=null && row["FeeRate"].ToString()!="")
				{
					model.FeeRate=decimal.Parse(row["FeeRate"].ToString());
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
			strSql.Append("select EnterpriseID,PayModeId,FeeRate ");
			strSql.Append(" FROM Pay_EnterprisePayFee ");
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
			strSql.Append(" EnterpriseID,PayModeId,FeeRate ");
			strSql.Append(" FROM Pay_EnterprisePayFee ");
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
			strSql.Append("select count(1) FROM Pay_EnterprisePayFee ");
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
				strSql.Append("order by T.PayModeId desc");
			}
			strSql.Append(")AS Row, T.*  from Pay_EnterprisePayFee T ");
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
			parameters[0].Value = "Pay_EnterprisePayFee";
			parameters[1].Value = "PayModeId";
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

