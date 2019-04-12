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
using System.Collections.Generic;

namespace ColoPay.DAL.Pay
{
    /// <summary>
    /// 数据访问类:Order
    /// </summary>
    public partial class Order
    {
        public Order()
        { }
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Pay_Order");
            strSql.Append(" where OrderId=@OrderId");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderId", SqlDbType.Int,4)
            };
            parameters[0].Value = OrderId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ColoPay.Model.Pay.Order model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Pay_Order(");
            strSql.Append("OrderCode,EnterOrder,EnterpriseID,Agentd,Amount,PaymentFee,FeeRate,OrderAmount,PayModeId,PaymentTypeName,PaymentGateway,PaymentStatus,OrderStatus,OrderInfo,AppId,AppSecrit,AppUrl,AppReturnUrl,AppNotifyUrl,CreatedTime,Remark)");
            strSql.Append(" values (");
            strSql.Append("@OrderCode,@EnterOrder,@EnterpriseID,@Agentd,@Amount,@PaymentFee,@FeeRate,@OrderAmount,@PayModeId,@PaymentTypeName,@PaymentGateway,@PaymentStatus,@OrderStatus,@OrderInfo,@AppId,@AppSecrit,@AppUrl,@AppReturnUrl,@AppNotifyUrl,@CreatedTime,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderCode", SqlDbType.NVarChar,300),
                    new SqlParameter("@EnterOrder", SqlDbType.NVarChar,300),
                    new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
                    new SqlParameter("@Agentd", SqlDbType.Int,4),
                    new SqlParameter("@Amount", SqlDbType.Money,8),
                    new SqlParameter("@PaymentFee", SqlDbType.Money,8),
                    new SqlParameter("@FeeRate", SqlDbType.Decimal,9),
                    new SqlParameter("@OrderAmount", SqlDbType.Money,8),
                    new SqlParameter("@PayModeId", SqlDbType.Int,4),
                    new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar,50),
                    new SqlParameter("@PaymentGateway", SqlDbType.NVarChar,50),
                    new SqlParameter("@PaymentStatus", SqlDbType.Int,4),
                    new SqlParameter("@OrderStatus", SqlDbType.Int,4),
                    new SqlParameter("@OrderInfo", SqlDbType.NVarChar,300),
                    new SqlParameter("@AppId", SqlDbType.NVarChar,200),
                    new SqlParameter("@AppSecrit", SqlDbType.NVarChar,200),
                    new SqlParameter("@AppUrl", SqlDbType.NVarChar,200),
                    new SqlParameter("@AppReturnUrl", SqlDbType.NVarChar,200),
                    new SqlParameter("@AppNotifyUrl", SqlDbType.NVarChar,200),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,1000)};
            parameters[0].Value = model.OrderCode;
            parameters[1].Value = model.EnterOrder;
            parameters[2].Value = model.EnterpriseID;
            parameters[3].Value = model.Agentd;
            parameters[4].Value = model.Amount;
            parameters[5].Value = model.PaymentFee;
            parameters[6].Value = model.FeeRate;
            parameters[7].Value = model.OrderAmount;
            parameters[8].Value = model.PayModeId;
            parameters[9].Value = model.PaymentTypeName;
            parameters[10].Value = model.PaymentGateway;
            parameters[11].Value = model.PaymentStatus;
            parameters[12].Value = model.OrderStatus;
            parameters[13].Value = model.OrderInfo;
            parameters[14].Value = model.AppId;
            parameters[15].Value = model.AppSecrit;
            parameters[16].Value = model.AppUrl;
            parameters[17].Value = model.AppReturnUrl;
            parameters[18].Value = model.AppNotifyUrl;
            parameters[19].Value = model.CreatedTime;
            parameters[20].Value = model.Remark;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Pay_Order set ");
            strSql.Append("OrderCode=@OrderCode,");
            strSql.Append("EnterOrder=@EnterOrder,");
            strSql.Append("EnterpriseID=@EnterpriseID,");
            strSql.Append("Agentd=@Agentd,");
            strSql.Append("Amount=@Amount,");
            strSql.Append("PaymentFee=@PaymentFee,");
            strSql.Append("FeeRate=@FeeRate,");
            strSql.Append("OrderAmount=@OrderAmount,");
            strSql.Append("PayModeId=@PayModeId,");
            strSql.Append("PaymentTypeName=@PaymentTypeName,");
            strSql.Append("PaymentGateway=@PaymentGateway,");
            strSql.Append("PaymentStatus=@PaymentStatus,");
            strSql.Append("OrderStatus=@OrderStatus,");
            strSql.Append("OrderInfo=@OrderInfo,");
            strSql.Append("AppId=@AppId,");
            strSql.Append("AppSecrit=@AppSecrit,");
            strSql.Append("AppUrl=@AppUrl,");
            strSql.Append("AppReturnUrl=@AppReturnUrl,");
            strSql.Append("AppNotifyUrl=@AppNotifyUrl,");
            strSql.Append("CreatedTime=@CreatedTime,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where OrderId=@OrderId");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderCode", SqlDbType.NVarChar,300),
                    new SqlParameter("@EnterOrder", SqlDbType.NVarChar,300),
                    new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
                    new SqlParameter("@Agentd", SqlDbType.Int,4),
                    new SqlParameter("@Amount", SqlDbType.Money,8),
                    new SqlParameter("@PaymentFee", SqlDbType.Money,8),
                    new SqlParameter("@FeeRate", SqlDbType.Decimal,9),
                    new SqlParameter("@OrderAmount", SqlDbType.Money,8),
                    new SqlParameter("@PayModeId", SqlDbType.Int,4),
                    new SqlParameter("@PaymentTypeName", SqlDbType.NVarChar,50),
                    new SqlParameter("@PaymentGateway", SqlDbType.NVarChar,50),
                    new SqlParameter("@PaymentStatus", SqlDbType.Int,4),
                    new SqlParameter("@OrderStatus", SqlDbType.Int,4),
                    new SqlParameter("@OrderInfo", SqlDbType.NVarChar,300),
                    new SqlParameter("@AppId", SqlDbType.NVarChar,200),
                    new SqlParameter("@AppSecrit", SqlDbType.NVarChar,200),
                    new SqlParameter("@AppUrl", SqlDbType.NVarChar,200),
                    new SqlParameter("@AppReturnUrl", SqlDbType.NVarChar,200),
                    new SqlParameter("@AppNotifyUrl", SqlDbType.NVarChar,200),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
                    new SqlParameter("@OrderId", SqlDbType.Int,4)};
            parameters[0].Value = model.OrderCode;
            parameters[1].Value = model.EnterOrder;
            parameters[2].Value = model.EnterpriseID;
            parameters[3].Value = model.Agentd;
            parameters[4].Value = model.Amount;
            parameters[5].Value = model.PaymentFee;
            parameters[6].Value = model.FeeRate;
            parameters[7].Value = model.OrderAmount;
            parameters[8].Value = model.PayModeId;
            parameters[9].Value = model.PaymentTypeName;
            parameters[10].Value = model.PaymentGateway;
            parameters[11].Value = model.PaymentStatus;
            parameters[12].Value = model.OrderStatus;
            parameters[13].Value = model.OrderInfo;
            parameters[14].Value = model.AppId;
            parameters[15].Value = model.AppSecrit;
            parameters[16].Value = model.AppUrl;
            parameters[17].Value = model.AppReturnUrl;
            parameters[18].Value = model.AppNotifyUrl;
            parameters[19].Value = model.CreatedTime;
            parameters[20].Value = model.Remark;
            parameters[21].Value = model.OrderId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Pay_Order ");
            strSql.Append(" where OrderId=@OrderId");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderId", SqlDbType.Int,4)
            };
            parameters[0].Value = OrderId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public bool DeleteList(string OrderIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Pay_Order ");
            strSql.Append(" where OrderId in (" + OrderIdlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 OrderId,OrderCode,EnterOrder,EnterpriseID,Agentd,Amount,PaymentFee,FeeRate,OrderAmount,PayModeId,PaymentTypeName,PaymentGateway,PaymentStatus,OrderStatus,OrderInfo,AppId,AppSecrit,AppUrl,AppReturnUrl,AppNotifyUrl,CreatedTime,Remark from Pay_Order ");
            strSql.Append(" where OrderId=@OrderId");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderId", SqlDbType.Int,4)
            };
            parameters[0].Value = OrderId;

            ColoPay.Model.Pay.Order model = new ColoPay.Model.Pay.Order();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
            ColoPay.Model.Pay.Order model = new ColoPay.Model.Pay.Order();
            if (row != null)
            {
                if (row["OrderId"] != null && row["OrderId"].ToString() != "")
                {
                    model.OrderId = int.Parse(row["OrderId"].ToString());
                }
                if (row["OrderCode"] != null)
                {
                    model.OrderCode = row["OrderCode"].ToString();
                }
                if (row["EnterOrder"] != null)
                {
                    model.EnterOrder = row["EnterOrder"].ToString();
                }
                if (row["EnterpriseID"] != null && row["EnterpriseID"].ToString() != "")
                {
                    model.EnterpriseID = int.Parse(row["EnterpriseID"].ToString());
                }
                if (row["Agentd"] != null && row["Agentd"].ToString() != "")
                {
                    model.Agentd = int.Parse(row["Agentd"].ToString());
                }
                if (row["Amount"] != null && row["Amount"].ToString() != "")
                {
                    model.Amount = decimal.Parse(row["Amount"].ToString());
                }
                if (row["PaymentFee"] != null && row["PaymentFee"].ToString() != "")
                {
                    model.PaymentFee = decimal.Parse(row["PaymentFee"].ToString());
                }
                if (row["FeeRate"] != null && row["FeeRate"].ToString() != "")
                {
                    model.FeeRate = decimal.Parse(row["FeeRate"].ToString());
                }
                if (row["OrderAmount"] != null && row["OrderAmount"].ToString() != "")
                {
                    model.OrderAmount = decimal.Parse(row["OrderAmount"].ToString());
                }
                if (row["PayModeId"] != null && row["PayModeId"].ToString() != "")
                {
                    model.PayModeId = int.Parse(row["PayModeId"].ToString());
                }
                if (row["PaymentTypeName"] != null)
                {
                    model.PaymentTypeName = row["PaymentTypeName"].ToString();
                }
                if (row["PaymentGateway"] != null)
                {
                    model.PaymentGateway = row["PaymentGateway"].ToString();
                }
                if (row["PaymentStatus"] != null && row["PaymentStatus"].ToString() != "")
                {
                    model.PaymentStatus = int.Parse(row["PaymentStatus"].ToString());
                }
                if (row["OrderStatus"] != null && row["OrderStatus"].ToString() != "")
                {
                    model.OrderStatus = int.Parse(row["OrderStatus"].ToString());
                }
                if (row["OrderInfo"] != null)
                {
                    model.OrderInfo = row["OrderInfo"].ToString();
                }
                if (row["AppId"] != null)
                {
                    model.AppId = row["AppId"].ToString();
                }
                if (row["AppSecrit"] != null)
                {
                    model.AppSecrit = row["AppSecrit"].ToString();
                }
                if (row["AppUrl"] != null)
                {
                    model.AppUrl = row["AppUrl"].ToString();
                }
                if (row["AppReturnUrl"] != null)
                {
                    model.AppReturnUrl = row["AppReturnUrl"].ToString();
                }
                if (row["AppNotifyUrl"] != null)
                {
                    model.AppNotifyUrl = row["AppNotifyUrl"].ToString();
                }
                if (row["CreatedTime"] != null && row["CreatedTime"].ToString() != "")
                {
                    model.CreatedTime = DateTime.Parse(row["CreatedTime"].ToString());
                }
                if (row["Remark"] != null)
                {
                    model.Remark = row["Remark"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select OrderId,OrderCode,EnterOrder,EnterpriseID,Agentd,Amount,PaymentFee,FeeRate,OrderAmount,PayModeId,PaymentTypeName,PaymentGateway,PaymentStatus,OrderStatus,OrderInfo,AppId,AppSecrit,AppUrl,AppReturnUrl,AppNotifyUrl,CreatedTime,Remark ");
            strSql.Append(" FROM Pay_Order ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" OrderId,OrderCode,EnterOrder,EnterpriseID,Agentd,Amount,PaymentFee,FeeRate,OrderAmount,PayModeId,PaymentTypeName,PaymentGateway,PaymentStatus,OrderStatus,OrderInfo,AppId,AppSecrit,AppUrl,AppReturnUrl,AppNotifyUrl,CreatedTime,Remark ");
            strSql.Append(" FROM Pay_Order ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Pay_Order ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
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
        public ColoPay.Model.Pay.Order GetModel(string code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 OrderId,OrderCode,EnterOrder,EnterpriseID,Agentd,Amount,PaymentFee,FeeRate,OrderAmount,PayModeId,PaymentTypeName,PaymentGateway,PaymentStatus,OrderStatus,OrderInfo,AppId,AppSecrit,AppUrl,AppReturnUrl,AppNotifyUrl,CreatedTime,Remark from Pay_Order ");
            strSql.Append(" where OrderCode=@OrderCode");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderCode", SqlDbType.NVarChar,200)
            };
            parameters[0].Value = code;

            ColoPay.Model.Pay.Order model = new ColoPay.Model.Pay.Order();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        public bool CompleteOrder(ColoPay.Model.Pay.Order orderInfo)
        {
            //事务处理
            List<CommandInfo> sqllist = new List<CommandInfo>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Pay_Order set ");
            strSql.Append(" PaymentStatus=2,OrderStatus=1 where OrderCode=@OrderCode");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderCode", SqlDbType.NVarChar,200)
                    };
            parameters[0].Value = orderInfo.OrderCode;

            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            //更新商家余额
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("update Pay_Enterprise set  Balance=Balance+@OrderAmount ");
            strSql1.Append(" where EnterpriseID=@EnterpriseID  ");
            SqlParameter[] parameters1 = {
                        new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
                          new SqlParameter("@OrderAmount", SqlDbType.Decimal)
                                         };
            parameters1[0].Value = orderInfo.EnterpriseID;
            parameters1[1].Value = orderInfo.OrderAmount;
            cmd = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd);
            //增加商家资金明细

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("insert into Pay_BalanceDetail(");
            strSql2.Append("EnterpriseID,PayType,OriginalId,OriginalCode,PaymentFee,OrderAmount,Amount,CreatedTime)");
            strSql2.Append(" values (");
            strSql2.Append("@EnterpriseID,@PayType,@OriginalId,@OriginalCode,@PaymentFee,@OrderAmount,@Amount,@CreatedTime)");
            SqlParameter[] parameters2 = {
                    new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
                    new SqlParameter("@PayType", SqlDbType.Int,4),
                    new SqlParameter("@OriginalId", SqlDbType.Int,4),
                    new SqlParameter("@OriginalCode", SqlDbType.NVarChar,100),
                    new SqlParameter("@PaymentFee", SqlDbType.Money,8),
                    new SqlParameter("@OrderAmount", SqlDbType.Money,8),
                    new SqlParameter("@Amount", SqlDbType.Money,8),
                    new SqlParameter("@CreatedTime", SqlDbType.DateTime)};
            parameters2[0].Value = orderInfo.EnterpriseID;
            parameters2[1].Value = 0;
            parameters2[2].Value = orderInfo.OrderId;
            parameters2[3].Value = orderInfo.OrderCode;
            parameters2[4].Value = orderInfo.PaymentFee;
            parameters2[5].Value = orderInfo.OrderAmount;
            parameters2[6].Value = orderInfo.Amount;
            parameters2[7].Value = DateTime.Now;

            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);


            return DBHelper.DefaultDBHelper.ExecuteSqlTran(sqllist) > 0 ? true : false;
        }


        public ColoPay.Model.Pay.Order GetModelEx(string enterOrder, int enterpriseID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 OrderId,OrderCode,EnterOrder,EnterpriseID,Agentd,Amount,PaymentFee,FeeRate,OrderAmount,PayModeId,PaymentTypeName,PaymentGateway,PaymentStatus,OrderStatus,OrderInfo,AppId,AppSecrit,AppUrl,AppReturnUrl,AppNotifyUrl,CreatedTime,Remark from Pay_Order ");
            strSql.Append(" where EnterOrder=@EnterOrder and EnterpriseID=@EnterpriseID");
            SqlParameter[] parameters = {
                    new SqlParameter("@EnterOrder", SqlDbType.NVarChar,200),
                      new SqlParameter("@EnterpriseID", SqlDbType.Int,4)
            };
            parameters[0].Value = enterOrder;
            parameters[1].Value = enterpriseID;

            ColoPay.Model.Pay.Order model = new ColoPay.Model.Pay.Order();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public bool HasNotify(int orderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Pay_Order set ");
            strSql.Append("OrderStatus=2 ");
            strSql.Append(" where OrderId=@OrderId");
            SqlParameter[] parameters = {
                    new SqlParameter("@OrderId", SqlDbType.Int,4)};
            parameters[0].Value = orderId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public int GetOrderCount(string startTime, string endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Pay_Order  ");
            strSql.Append(" where PaymentStatus=2  ");
            if (!String.IsNullOrWhiteSpace(startTime))
            {
                strSql.Append(String.Format(" and CreatedTime>='{0}' ", startTime));
            }

            if (!String.IsNullOrWhiteSpace(endTime))
            {
                strSql.Append(String.Format(" and CreatedTime<='{0}' ", endTime));
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



        public decimal GetOrderAmount(string startTime, string endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Sum(Amount) FROM Pay_Order  ");
            strSql.Append(" where PaymentStatus=2  ");
            if (!String.IsNullOrWhiteSpace(startTime))
            {
                strSql.Append(String.Format(" and CreatedTime>='{0}' ", startTime));
            }

            if (!String.IsNullOrWhiteSpace(endTime))
            {
                strSql.Append(String.Format(" and CreatedTime<='{0}' ", endTime));
            }

            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }


        public decimal GetOrderFee(string startTime, string endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Sum(PaymentFee) FROM Pay_Order  ");
            strSql.Append(" where PaymentStatus=2  ");
            if (!String.IsNullOrWhiteSpace(startTime))
            {
                strSql.Append(String.Format(" and CreatedTime>='{0}' ", startTime));
            }

            if (!String.IsNullOrWhiteSpace(endTime))
            {
                strSql.Append(String.Format(" and CreatedTime<='{0}' ", endTime));
            }

            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }


        public DataSet OrderStat(string startTime, string endTime)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@startTime",SqlDbType.VarChar,20),
                new SqlParameter("@endTime",SqlDbType.VarChar,20)
            };
            parameters[0].Value = startTime;
            parameters[1].Value = endTime;

            string strSql = @"select CONVERT(varchar(12),CreatedTime,23) Date, count(1) as Count,sum(Amount) as Total,sum(PaymentFee) as TotalFee  from dbo.Pay_Order 
where PaymentStatus=2 and  CreatedTime between @startTime and @endTime group by CONVERT(varchar(12), CreatedTime, 23)";
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }


        public DataSet GetOrderTop(int top)
        {
            string strSql =String.Format(@"select top {0} * from 
(select  EnterOrder,EnterpriseID,SUM(Amount) TotalAmount,SUM(PaymentFee) TotalFee from Pay_Order where PaymentStatus=2  group by EnterOrder,EnterpriseID) t
 order by TotalAmount Desc", top) ;
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion  ExtensionMethod
    }
}

