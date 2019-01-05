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
using System.Data;
using System.Text;
using System.Data.SqlClient;
using YSWL.DBUtility;//Please add references
namespace ColoPay.DAL.Pay
{
	/// <summary>
	/// 数据访问类:BalanceDetail
	/// </summary>
	public partial class BalanceDetail
	{
		public BalanceDetail()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("DetailId", "Pay_BalanceDetail"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int DetailId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Pay_BalanceDetail");
			strSql.Append(" where DetailId=@DetailId");
			SqlParameter[] parameters = {
					new SqlParameter("@DetailId", SqlDbType.Int,4)
			};
			parameters[0].Value = DetailId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ColoPay.Model.Pay.BalanceDetail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Pay_BalanceDetail(");
			strSql.Append("EnterpriseID,AgentId,Type,PayType,OriginalId,OriginalCode,PaymentFee,OrderAmount,Amount,CreatedTime)");
			strSql.Append(" values (");
			strSql.Append("@EnterpriseID,@AgentId,@Type,@PayType,@OriginalId,@OriginalCode,@PaymentFee,@OrderAmount,@Amount,@CreatedTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
					new SqlParameter("@AgentId", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@PayType", SqlDbType.Int,4),
					new SqlParameter("@OriginalId", SqlDbType.Int,4),
					new SqlParameter("@OriginalCode", SqlDbType.NVarChar,100),
					new SqlParameter("@PaymentFee", SqlDbType.Money,8),
					new SqlParameter("@OrderAmount", SqlDbType.Money,8),
					new SqlParameter("@Amount", SqlDbType.Money,8),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime)};
			parameters[0].Value = model.EnterpriseID;
			parameters[1].Value = model.AgentId;
			parameters[2].Value = model.Type;
			parameters[3].Value = model.PayType;
			parameters[4].Value = model.OriginalId;
			parameters[5].Value = model.OriginalCode;
			parameters[6].Value = model.PaymentFee;
			parameters[7].Value = model.OrderAmount;
			parameters[8].Value = model.Amount;
			parameters[9].Value = model.CreatedTime;

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
		public bool Update(ColoPay.Model.Pay.BalanceDetail model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Pay_BalanceDetail set ");
			strSql.Append("EnterpriseID=@EnterpriseID,");
			strSql.Append("AgentId=@AgentId,");
			strSql.Append("Type=@Type,");
			strSql.Append("PayType=@PayType,");
			strSql.Append("OriginalId=@OriginalId,");
			strSql.Append("OriginalCode=@OriginalCode,");
			strSql.Append("PaymentFee=@PaymentFee,");
			strSql.Append("OrderAmount=@OrderAmount,");
			strSql.Append("Amount=@Amount,");
			strSql.Append("CreatedTime=@CreatedTime");
			strSql.Append(" where DetailId=@DetailId");
			SqlParameter[] parameters = {
					new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
					new SqlParameter("@AgentId", SqlDbType.Int,4),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@PayType", SqlDbType.Int,4),
					new SqlParameter("@OriginalId", SqlDbType.Int,4),
					new SqlParameter("@OriginalCode", SqlDbType.NVarChar,100),
					new SqlParameter("@PaymentFee", SqlDbType.Money,8),
					new SqlParameter("@OrderAmount", SqlDbType.Money,8),
					new SqlParameter("@Amount", SqlDbType.Money,8),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime),
					new SqlParameter("@DetailId", SqlDbType.Int,4)};
			parameters[0].Value = model.EnterpriseID;
			parameters[1].Value = model.AgentId;
			parameters[2].Value = model.Type;
			parameters[3].Value = model.PayType;
			parameters[4].Value = model.OriginalId;
			parameters[5].Value = model.OriginalCode;
			parameters[6].Value = model.PaymentFee;
			parameters[7].Value = model.OrderAmount;
			parameters[8].Value = model.Amount;
			parameters[9].Value = model.CreatedTime;
			parameters[10].Value = model.DetailId;

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
		public bool Delete(int DetailId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Pay_BalanceDetail ");
			strSql.Append(" where DetailId=@DetailId");
			SqlParameter[] parameters = {
					new SqlParameter("@DetailId", SqlDbType.Int,4)
			};
			parameters[0].Value = DetailId;

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
		public bool DeleteList(string DetailIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Pay_BalanceDetail ");
			strSql.Append(" where DetailId in ("+DetailIdlist + ")  ");
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
		public ColoPay.Model.Pay.BalanceDetail GetModel(int DetailId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 DetailId,EnterpriseID,AgentId,Type,PayType,OriginalId,OriginalCode,PaymentFee,OrderAmount,Amount,CreatedTime from Pay_BalanceDetail ");
			strSql.Append(" where DetailId=@DetailId");
			SqlParameter[] parameters = {
					new SqlParameter("@DetailId", SqlDbType.Int,4)
			};
			parameters[0].Value = DetailId;

			ColoPay.Model.Pay.BalanceDetail model=new ColoPay.Model.Pay.BalanceDetail();
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
		public ColoPay.Model.Pay.BalanceDetail DataRowToModel(DataRow row)
		{
			ColoPay.Model.Pay.BalanceDetail model=new ColoPay.Model.Pay.BalanceDetail();
			if (row != null)
			{
				if(row["DetailId"]!=null && row["DetailId"].ToString()!="")
				{
					model.DetailId=int.Parse(row["DetailId"].ToString());
				}
				if(row["EnterpriseID"]!=null && row["EnterpriseID"].ToString()!="")
				{
					model.EnterpriseID=int.Parse(row["EnterpriseID"].ToString());
				}
				if(row["AgentId"]!=null && row["AgentId"].ToString()!="")
				{
					model.AgentId=int.Parse(row["AgentId"].ToString());
				}
				if(row["Type"]!=null && row["Type"].ToString()!="")
				{
					model.Type=int.Parse(row["Type"].ToString());
				}
				if(row["PayType"]!=null && row["PayType"].ToString()!="")
				{
					model.PayType=int.Parse(row["PayType"].ToString());
				}
				if(row["OriginalId"]!=null && row["OriginalId"].ToString()!="")
				{
					model.OriginalId=int.Parse(row["OriginalId"].ToString());
				}
				if(row["OriginalCode"]!=null)
				{
					model.OriginalCode=row["OriginalCode"].ToString();
				}
				if(row["PaymentFee"]!=null && row["PaymentFee"].ToString()!="")
				{
					model.PaymentFee=decimal.Parse(row["PaymentFee"].ToString());
				}
				if(row["OrderAmount"]!=null && row["OrderAmount"].ToString()!="")
				{
					model.OrderAmount=decimal.Parse(row["OrderAmount"].ToString());
				}
				if(row["Amount"]!=null && row["Amount"].ToString()!="")
				{
					model.Amount=decimal.Parse(row["Amount"].ToString());
				}
				if(row["CreatedTime"]!=null && row["CreatedTime"].ToString()!="")
				{
					model.CreatedTime=DateTime.Parse(row["CreatedTime"].ToString());
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
			strSql.Append("select DetailId,EnterpriseID,AgentId,Type,PayType,OriginalId,OriginalCode,PaymentFee,OrderAmount,Amount,CreatedTime ");
			strSql.Append(" FROM Pay_BalanceDetail ");
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
			strSql.Append(" DetailId,EnterpriseID,AgentId,Type,PayType,OriginalId,OriginalCode,PaymentFee,OrderAmount,Amount,CreatedTime ");
			strSql.Append(" FROM Pay_BalanceDetail ");
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
			strSql.Append("select count(1) FROM Pay_BalanceDetail ");
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
				strSql.Append("order by T.DetailId desc");
			}
			strSql.Append(")AS Row, T.*  from Pay_BalanceDetail T ");
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
			parameters[0].Value = "Pay_BalanceDetail";
			parameters[1].Value = "DetailId";
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

