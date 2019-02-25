/**  版本信息模板在安装目录下，可自行修改。
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
using System.Data;
using System.Text;
using System.Data.SqlClient;
using YSWL.DBUtility;//Please add references
namespace ColoPay.DAL.Pay
{
	/// <summary>
	/// 数据访问类:AgentPayFee
	/// </summary>
	public partial class AgentPayFee
	{
		public AgentPayFee()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("AgentID", "Pay_AgentPayFee"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int AgentID,int PayModeId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Pay_AgentPayFee");
			strSql.Append(" where AgentID=@AgentID and PayModeId=@PayModeId ");
			SqlParameter[] parameters = {
					new SqlParameter("@AgentID", SqlDbType.Int,4),
					new SqlParameter("@PayModeId", SqlDbType.Int,4)			};
			parameters[0].Value = AgentID;
			parameters[1].Value = PayModeId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(ColoPay.Model.Pay.AgentPayFee model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Pay_AgentPayFee(");
			strSql.Append("AgentID,PayModeId,FeeRate)");
			strSql.Append(" values (");
			strSql.Append("@AgentID,@PayModeId,@FeeRate)");
			SqlParameter[] parameters = {
					new SqlParameter("@AgentID", SqlDbType.Int,4),
					new SqlParameter("@PayModeId", SqlDbType.Int,4),
					new SqlParameter("@FeeRate", SqlDbType.Decimal,9)};
			parameters[0].Value = model.AgentID;
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
		public bool Update(ColoPay.Model.Pay.AgentPayFee model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Pay_AgentPayFee set ");
			strSql.Append("FeeRate=@FeeRate");
			strSql.Append(" where AgentID=@AgentID and PayModeId=@PayModeId ");
			SqlParameter[] parameters = {
					new SqlParameter("@FeeRate", SqlDbType.Decimal,9),
					new SqlParameter("@AgentID", SqlDbType.Int,4),
					new SqlParameter("@PayModeId", SqlDbType.Int,4)};
			parameters[0].Value = model.FeeRate;
			parameters[1].Value = model.AgentID;
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
		public bool Delete(int AgentID,int PayModeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Pay_AgentPayFee ");
			strSql.Append(" where AgentID=@AgentID and PayModeId=@PayModeId ");
			SqlParameter[] parameters = {
					new SqlParameter("@AgentID", SqlDbType.Int,4),
					new SqlParameter("@PayModeId", SqlDbType.Int,4)			};
			parameters[0].Value = AgentID;
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
		public ColoPay.Model.Pay.AgentPayFee GetModel(int AgentID,int PayModeId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 AgentID,PayModeId,FeeRate from Pay_AgentPayFee ");
			strSql.Append(" where AgentID=@AgentID and PayModeId=@PayModeId ");
			SqlParameter[] parameters = {
					new SqlParameter("@AgentID", SqlDbType.Int,4),
					new SqlParameter("@PayModeId", SqlDbType.Int,4)			};
			parameters[0].Value = AgentID;
			parameters[1].Value = PayModeId;

			ColoPay.Model.Pay.AgentPayFee model=new ColoPay.Model.Pay.AgentPayFee();
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
		public ColoPay.Model.Pay.AgentPayFee DataRowToModel(DataRow row)
		{
			ColoPay.Model.Pay.AgentPayFee model=new ColoPay.Model.Pay.AgentPayFee();
			if (row != null)
			{
				if(row["AgentID"]!=null && row["AgentID"].ToString()!="")
				{
					model.AgentID=int.Parse(row["AgentID"].ToString());
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
			strSql.Append("select AgentID,PayModeId,FeeRate ");
			strSql.Append(" FROM Pay_AgentPayFee ");
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
			strSql.Append(" AgentID,PayModeId,FeeRate ");
			strSql.Append(" FROM Pay_AgentPayFee ");
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
			strSql.Append("select count(1) FROM Pay_AgentPayFee ");
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
			strSql.Append(")AS Row, T.*  from Pay_AgentPayFee T ");
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
			parameters[0].Value = "Pay_AgentPayFee";
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

