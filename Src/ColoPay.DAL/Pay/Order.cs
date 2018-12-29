/**  版本信息模板在安装目录下，可自行修改。
* Order.cs
*
* 功 能： N/A
* 类 名： Order
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
	/// 数据访问类:Order
	/// </summary>
	public partial class Order
	{
		public Order()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("OrderId", "Pay_Order"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int OrderId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Pay_Order");
			strSql.Append(" where OrderId=@OrderId");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.Int,4)
			};
			parameters[0].Value = OrderId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ColoPay.Model.Pay.Order model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Pay_Order(");
			strSql.Append("OrderCode,EnterOrder,EnterpriseID,Amount,PaymentFee,FeeRate,OrderAmount,PayModeId,PaymentTypeName,PaymentGateway,PaymentStatus,OrderInfo,AppId,AppSecrit,AppUrl,AppReturnUrl,CreatedTime,Remark)");
			strSql.Append(" values (");
			strSql.Append("@OrderCode,@EnterOrder,@EnterpriseID,@Amount,@PaymentFee,@FeeRate,@OrderAmount,@PayModeId,@PaymentTypeName,@PaymentGateway,@PaymentStatus,@OrderInfo,@AppId,@AppSecrit,@AppUrl,@AppReturnUrl,@CreatedTime,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,300),
					new SqlParameter("@EnterOrder", SqlDbType.NVarChar,300),
					new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
					new SqlParameter("@Amount", SqlDbType.Money,8),
					new SqlParameter("@PaymentFee", SqlDbType.Money,8),
					new SqlParameter("@FeeRate", SqlDbType.Decimal,9),
					new SqlParameter("@OrderAmount", SqlDbType.Money,8),
					new SqlParameter("@PayModeId", SqlDbType.Int,4),
					new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentGateway", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentStatus", SqlDbType.Int,4),
					new SqlParameter("@OrderInfo", SqlDbType.NVarChar,300),
					new SqlParameter("@AppId", SqlDbType.NVarChar,200),
					new SqlParameter("@AppSecrit", SqlDbType.NVarChar,200),
					new SqlParameter("@AppUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@AppReturnUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000)};
			parameters[0].Value = model.OrderCode;
			parameters[1].Value = model.EnterOrder;
			parameters[2].Value = model.EnterpriseID;
			parameters[3].Value = model.Amount;
			parameters[4].Value = model.PaymentFee;
			parameters[5].Value = model.FeeRate;
			parameters[6].Value = model.OrderAmount;
			parameters[7].Value = model.PayModeId;
			parameters[8].Value = model.PaymentTypeName;
			parameters[9].Value = model.PaymentGateway;
			parameters[10].Value = model.PaymentStatus;
			parameters[11].Value = model.OrderInfo;
			parameters[12].Value = model.AppId;
			parameters[13].Value = model.AppSecrit;
			parameters[14].Value = model.AppUrl;
			parameters[15].Value = model.AppReturnUrl;
			parameters[16].Value = model.CreatedTime;
			parameters[17].Value = model.Remark;

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
		public bool Update(ColoPay.Model.Pay.Order model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Pay_Order set ");
			strSql.Append("OrderCode=@OrderCode,");
			strSql.Append("EnterOrder=@EnterOrder,");
			strSql.Append("EnterpriseID=@EnterpriseID,");
			strSql.Append("Amount=@Amount,");
			strSql.Append("PaymentFee=@PaymentFee,");
			strSql.Append("FeeRate=@FeeRate,");
			strSql.Append("OrderAmount=@OrderAmount,");
			strSql.Append("PayModeId=@PayModeId,");
			strSql.Append("PaymentTypeName=@PaymentTypeName,");
			strSql.Append("PaymentGateway=@PaymentGateway,");
			strSql.Append("PaymentStatus=@PaymentStatus,");
			strSql.Append("OrderInfo=@OrderInfo,");
			strSql.Append("AppId=@AppId,");
			strSql.Append("AppSecrit=@AppSecrit,");
			strSql.Append("AppUrl=@AppUrl,");
			strSql.Append("AppReturnUrl=@AppReturnUrl,");
			strSql.Append("CreatedTime=@CreatedTime,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where OrderId=@OrderId");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderCode", SqlDbType.NVarChar,300),
					new SqlParameter("@EnterOrder", SqlDbType.NVarChar,300),
					new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
					new SqlParameter("@Amount", SqlDbType.Money,8),
					new SqlParameter("@PaymentFee", SqlDbType.Money,8),
					new SqlParameter("@FeeRate", SqlDbType.Decimal,9),
					new SqlParameter("@OrderAmount", SqlDbType.Money,8),
					new SqlParameter("@PayModeId", SqlDbType.Int,4),
					new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentGateway", SqlDbType.NVarChar,50),
					new SqlParameter("@PaymentStatus", SqlDbType.Int,4),
					new SqlParameter("@OrderInfo", SqlDbType.NVarChar,300),
					new SqlParameter("@AppId", SqlDbType.NVarChar,200),
					new SqlParameter("@AppSecrit", SqlDbType.NVarChar,200),
					new SqlParameter("@AppUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@AppReturnUrl", SqlDbType.NVarChar,200),
					new SqlParameter("@CreatedTime", SqlDbType.DateTime),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@OrderId", SqlDbType.Int,4)};
			parameters[0].Value = model.OrderCode;
			parameters[1].Value = model.EnterOrder;
			parameters[2].Value = model.EnterpriseID;
			parameters[3].Value = model.Amount;
			parameters[4].Value = model.PaymentFee;
			parameters[5].Value = model.FeeRate;
			parameters[6].Value = model.OrderAmount;
			parameters[7].Value = model.PayModeId;
			parameters[8].Value = model.PaymentTypeName;
			parameters[9].Value = model.PaymentGateway;
			parameters[10].Value = model.PaymentStatus;
			parameters[11].Value = model.OrderInfo;
			parameters[12].Value = model.AppId;
			parameters[13].Value = model.AppSecrit;
			parameters[14].Value = model.AppUrl;
			parameters[15].Value = model.AppReturnUrl;
			parameters[16].Value = model.CreatedTime;
			parameters[17].Value = model.Remark;
			parameters[18].Value = model.OrderId;

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
		public bool Delete(int OrderId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Pay_Order ");
			strSql.Append(" where OrderId=@OrderId");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.Int,4)
			};
			parameters[0].Value = OrderId;

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
		public bool DeleteList(string OrderIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Pay_Order ");
			strSql.Append(" where OrderId in ("+OrderIdlist + ")  ");
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
		public ColoPay.Model.Pay.Order GetModel(int OrderId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 OrderId,OrderCode,EnterOrder,EnterpriseID,Amount,PaymentFee,FeeRate,OrderAmount,PayModeId,PaymentTypeName,PaymentGateway,PaymentStatus,OrderInfo,AppId,AppSecrit,AppUrl,AppReturnUrl,CreatedTime,Remark from Pay_Order ");
			strSql.Append(" where OrderId=@OrderId");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.Int,4)
			};
			parameters[0].Value = OrderId;

			ColoPay.Model.Pay.Order model=new ColoPay.Model.Pay.Order();
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
		public ColoPay.Model.Pay.Order DataRowToModel(DataRow row)
		{
			ColoPay.Model.Pay.Order model=new ColoPay.Model.Pay.Order();
			if (row != null)
			{
				if(row["OrderId"]!=null && row["OrderId"].ToString()!="")
				{
					model.OrderId=int.Parse(row["OrderId"].ToString());
				}
				if(row["OrderCode"]!=null)
				{
					model.OrderCode=row["OrderCode"].ToString();
				}
				if(row["EnterOrder"]!=null)
				{
					model.EnterOrder=row["EnterOrder"].ToString();
				}
				if(row["EnterpriseID"]!=null && row["EnterpriseID"].ToString()!="")
				{
					model.EnterpriseID=int.Parse(row["EnterpriseID"].ToString());
				}
				if(row["Amount"]!=null && row["Amount"].ToString()!="")
				{
					model.Amount=decimal.Parse(row["Amount"].ToString());
				}
				if(row["PaymentFee"]!=null && row["PaymentFee"].ToString()!="")
				{
					model.PaymentFee=decimal.Parse(row["PaymentFee"].ToString());
				}
				if(row["FeeRate"]!=null && row["FeeRate"].ToString()!="")
				{
					model.FeeRate=decimal.Parse(row["FeeRate"].ToString());
				}
				if(row["OrderAmount"]!=null && row["OrderAmount"].ToString()!="")
				{
					model.OrderAmount=decimal.Parse(row["OrderAmount"].ToString());
				}
				if(row["PayModeId"]!=null && row["PayModeId"].ToString()!="")
				{
					model.PayModeId=int.Parse(row["PayModeId"].ToString());
				}
				if(row["PaymentTypeName"]!=null)
				{
					model.PaymentTypeName=row["PaymentTypeName"].ToString();
				}
				if(row["PaymentGateway"]!=null)
				{
					model.PaymentGateway=row["PaymentGateway"].ToString();
				}
				if(row["PaymentStatus"]!=null && row["PaymentStatus"].ToString()!="")
				{
					model.PaymentStatus=int.Parse(row["PaymentStatus"].ToString());
				}
				if(row["OrderInfo"]!=null)
				{
					model.OrderInfo=row["OrderInfo"].ToString();
				}
				if(row["AppId"]!=null)
				{
					model.AppId=row["AppId"].ToString();
				}
				if(row["AppSecrit"]!=null)
				{
					model.AppSecrit=row["AppSecrit"].ToString();
				}
				if(row["AppUrl"]!=null)
				{
					model.AppUrl=row["AppUrl"].ToString();
				}
				if(row["AppReturnUrl"]!=null)
				{
					model.AppReturnUrl=row["AppReturnUrl"].ToString();
				}
				if(row["CreatedTime"]!=null && row["CreatedTime"].ToString()!="")
				{
					model.CreatedTime=DateTime.Parse(row["CreatedTime"].ToString());
				}
				if(row["Remark"]!=null)
				{
					model.Remark=row["Remark"].ToString();
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
			strSql.Append("select OrderId,OrderCode,EnterOrder,EnterpriseID,Amount,PaymentFee,FeeRate,OrderAmount,PayModeId,PaymentTypeName,PaymentGateway,PaymentStatus,OrderInfo,AppId,AppSecrit,AppUrl,AppReturnUrl,CreatedTime,Remark ");
			strSql.Append(" FROM Pay_Order ");
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
			strSql.Append(" OrderId,OrderCode,EnterOrder,EnterpriseID,Amount,PaymentFee,FeeRate,OrderAmount,PayModeId,PaymentTypeName,PaymentGateway,PaymentStatus,OrderInfo,AppId,AppSecrit,AppUrl,AppReturnUrl,CreatedTime,Remark ");
			strSql.Append(" FROM Pay_Order ");
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
			strSql.Append("select count(1) FROM Pay_Order ");
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
				strSql.Append("order by T.OrderId desc");
			}
			strSql.Append(")AS Row, T.*  from Pay_Order T ");
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
			parameters[0].Value = "Pay_Order";
			parameters[1].Value = "OrderId";
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

