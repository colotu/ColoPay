﻿/**  版本信息模板在安装目录下，可自行修改。
* Withdraw.cs
*
* 功 能： N/A
* 类 名： Withdraw
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
	/// 数据访问类:Withdraw
	/// </summary>
	public partial class Withdraw
	{
		public Withdraw()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("WithdrawId", "Pay_Withdraw");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int WithdrawId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Pay_Withdraw");
            strSql.Append(" where WithdrawId=@WithdrawId");
            SqlParameter[] parameters = {
                    new SqlParameter("@WithdrawId", SqlDbType.Int,4)
            };
            parameters[0].Value = WithdrawId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ColoPay.Model.Pay.Withdraw model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Pay_Withdraw(");
            strSql.Append("WithdrawCode,EnterpriseID,AgentId,Type,UserName,Amount,WithdrawBank,WithdrawInfo,WithdrawNum,Status,CreatedDate,CreatedUserId,AuditDate,AuditUserId,PayDate,PayUserId,Remark)");
            strSql.Append(" values (");
            strSql.Append("@WithdrawCode,@EnterpriseID,@AgentId,@Type,@UserName,@Amount,@WithdrawBank,@WithdrawInfo,@WithdrawNum,@Status,@CreatedDate,@CreatedUserId,@AuditDate,@AuditUserId,@PayDate,@PayUserId,@Remark)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@WithdrawCode", SqlDbType.NVarChar,100),
                    new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
                    new SqlParameter("@AgentId", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,200),
                    new SqlParameter("@Amount", SqlDbType.Money,8),
                    new SqlParameter("@WithdrawBank", SqlDbType.NVarChar,300),
                    new SqlParameter("@WithdrawInfo", SqlDbType.NVarChar,200),
                    new SqlParameter("@WithdrawNum", SqlDbType.NVarChar,200),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
                    new SqlParameter("@AuditDate", SqlDbType.DateTime),
                    new SqlParameter("@AuditUserId", SqlDbType.Int,4),
                    new SqlParameter("@PayDate", SqlDbType.DateTime),
                    new SqlParameter("@PayUserId", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,300)};
            parameters[0].Value = model.WithdrawCode;
            parameters[1].Value = model.EnterpriseID;
            parameters[2].Value = model.AgentId;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.UserName;
            parameters[5].Value = model.Amount;
            parameters[6].Value = model.WithdrawBank;
            parameters[7].Value = model.WithdrawInfo;
            parameters[8].Value = model.WithdrawNum;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.CreatedDate;
            parameters[11].Value = model.CreatedUserId;
            parameters[12].Value = model.AuditDate;
            parameters[13].Value = model.AuditUserId;
            parameters[14].Value = model.PayDate;
            parameters[15].Value = model.PayUserId;
            parameters[16].Value = model.Remark;

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
        public bool Update(ColoPay.Model.Pay.Withdraw model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Pay_Withdraw set ");
            strSql.Append("WithdrawCode=@WithdrawCode,");
            strSql.Append("EnterpriseID=@EnterpriseID,");
            strSql.Append("AgentId=@AgentId,");
            strSql.Append("Type=@Type,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("Amount=@Amount,");
            strSql.Append("WithdrawBank=@WithdrawBank,");
            strSql.Append("WithdrawInfo=@WithdrawInfo,");
            strSql.Append("WithdrawNum=@WithdrawNum,");
            strSql.Append("Status=@Status,");
            strSql.Append("CreatedDate=@CreatedDate,");
            strSql.Append("CreatedUserId=@CreatedUserId,");
            strSql.Append("AuditDate=@AuditDate,");
            strSql.Append("AuditUserId=@AuditUserId,");
            strSql.Append("PayDate=@PayDate,");
            strSql.Append("PayUserId=@PayUserId,");
            strSql.Append("Remark=@Remark");
            strSql.Append(" where WithdrawId=@WithdrawId");
            SqlParameter[] parameters = {
                    new SqlParameter("@WithdrawCode", SqlDbType.NVarChar,100),
                    new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
                    new SqlParameter("@AgentId", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,200),
                    new SqlParameter("@Amount", SqlDbType.Money,8),
                    new SqlParameter("@WithdrawBank", SqlDbType.NVarChar,300),
                    new SqlParameter("@WithdrawInfo", SqlDbType.NVarChar,200),
                    new SqlParameter("@WithdrawNum", SqlDbType.NVarChar,200),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
                    new SqlParameter("@AuditDate", SqlDbType.DateTime),
                    new SqlParameter("@AuditUserId", SqlDbType.Int,4),
                    new SqlParameter("@PayDate", SqlDbType.DateTime),
                    new SqlParameter("@PayUserId", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,300),
                    new SqlParameter("@WithdrawId", SqlDbType.Int,4)};
            parameters[0].Value = model.WithdrawCode;
            parameters[1].Value = model.EnterpriseID;
            parameters[2].Value = model.AgentId;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.UserName;
            parameters[5].Value = model.Amount;
            parameters[6].Value = model.WithdrawBank;
            parameters[7].Value = model.WithdrawInfo;
            parameters[8].Value = model.WithdrawNum;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.CreatedDate;
            parameters[11].Value = model.CreatedUserId;
            parameters[12].Value = model.AuditDate;
            parameters[13].Value = model.AuditUserId;
            parameters[14].Value = model.PayDate;
            parameters[15].Value = model.PayUserId;
            parameters[16].Value = model.Remark;
            parameters[17].Value = model.WithdrawId;

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
        public bool Delete(int WithdrawId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Pay_Withdraw ");
            strSql.Append(" where WithdrawId=@WithdrawId");
            SqlParameter[] parameters = {
                    new SqlParameter("@WithdrawId", SqlDbType.Int,4)
            };
            parameters[0].Value = WithdrawId;

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
        public bool DeleteList(string WithdrawIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Pay_Withdraw ");
            strSql.Append(" where WithdrawId in (" + WithdrawIdlist + ")  ");
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
        public ColoPay.Model.Pay.Withdraw GetModel(int WithdrawId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 WithdrawId,WithdrawCode,EnterpriseID,AgentId,Type,UserName,Amount,WithdrawBank,WithdrawInfo,WithdrawNum,Status,CreatedDate,CreatedUserId,AuditDate,AuditUserId,PayDate,PayUserId,Remark from Pay_Withdraw ");
            strSql.Append(" where WithdrawId=@WithdrawId");
            SqlParameter[] parameters = {
                    new SqlParameter("@WithdrawId", SqlDbType.Int,4)
            };
            parameters[0].Value = WithdrawId;

            ColoPay.Model.Pay.Withdraw model = new ColoPay.Model.Pay.Withdraw();
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
        public ColoPay.Model.Pay.Withdraw DataRowToModel(DataRow row)
        {
            ColoPay.Model.Pay.Withdraw model = new ColoPay.Model.Pay.Withdraw();
            if (row != null)
            {
                if (row["WithdrawId"] != null && row["WithdrawId"].ToString() != "")
                {
                    model.WithdrawId = int.Parse(row["WithdrawId"].ToString());
                }
                if (row["WithdrawCode"] != null)
                {
                    model.WithdrawCode = row["WithdrawCode"].ToString();
                }
                if (row["EnterpriseID"] != null && row["EnterpriseID"].ToString() != "")
                {
                    model.EnterpriseID = int.Parse(row["EnterpriseID"].ToString());
                }
                if (row["AgentId"] != null && row["AgentId"].ToString() != "")
                {
                    model.AgentId = int.Parse(row["AgentId"].ToString());
                }
                if (row["Type"] != null && row["Type"].ToString() != "")
                {
                    model.Type = int.Parse(row["Type"].ToString());
                }
                if (row["UserName"] != null)
                {
                    model.UserName = row["UserName"].ToString();
                }
                if (row["Amount"] != null && row["Amount"].ToString() != "")
                {
                    model.Amount = decimal.Parse(row["Amount"].ToString());
                }
                if (row["WithdrawBank"] != null)
                {
                    model.WithdrawBank = row["WithdrawBank"].ToString();
                }
                if (row["WithdrawInfo"] != null)
                {
                    model.WithdrawInfo = row["WithdrawInfo"].ToString();
                }
                if (row["WithdrawNum"] != null)
                {
                    model.WithdrawNum = row["WithdrawNum"].ToString();
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.CreatedDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row["CreatedUserId"] != null && row["CreatedUserId"].ToString() != "")
                {
                    model.CreatedUserId = int.Parse(row["CreatedUserId"].ToString());
                }
                if (row["AuditDate"] != null && row["AuditDate"].ToString() != "")
                {
                    model.AuditDate = DateTime.Parse(row["AuditDate"].ToString());
                }
                if (row["AuditUserId"] != null && row["AuditUserId"].ToString() != "")
                {
                    model.AuditUserId = int.Parse(row["AuditUserId"].ToString());
                }
                if (row["PayDate"] != null && row["PayDate"].ToString() != "")
                {
                    model.PayDate = DateTime.Parse(row["PayDate"].ToString());
                }
                if (row["PayUserId"] != null && row["PayUserId"].ToString() != "")
                {
                    model.PayUserId = int.Parse(row["PayUserId"].ToString());
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
            strSql.Append("select WithdrawId,WithdrawCode,EnterpriseID,AgentId,Type,UserName,Amount,WithdrawBank,WithdrawInfo,WithdrawNum,Status,CreatedDate,CreatedUserId,AuditDate,AuditUserId,PayDate,PayUserId,Remark ");
            strSql.Append(" FROM Pay_Withdraw ");
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
            strSql.Append(" WithdrawId,WithdrawCode,EnterpriseID,AgentId,Type,UserName,Amount,WithdrawBank,WithdrawInfo,WithdrawNum,Status,CreatedDate,CreatedUserId,AuditDate,AuditUserId,PayDate,PayUserId,Remark ");
            strSql.Append(" FROM Pay_Withdraw ");
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
            strSql.Append("select count(1) FROM Pay_Withdraw ");
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
                strSql.Append("order by T.WithdrawId desc");
            }
            strSql.Append(")AS Row, T.*  from Pay_Withdraw T ");
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
			parameters[0].Value = "Pay_Withdraw";
			parameters[1].Value = "WithdrawId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        public bool Audit(int withdrawId, int status, int userId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Pay_Withdraw set ");
            strSql.Append("Status=@Status,");
                strSql.Append("AuditDate=@Date,");
                strSql.Append("AuditUserId=@UserId ");
            strSql.Append(" where WithdrawId=@WithdrawId");
            SqlParameter[] parameters = { 
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@Date", SqlDbType.DateTime), 
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@WithdrawId", SqlDbType.Int,4)};
            parameters[0].Value = status;
            parameters[1].Value =DateTime.Now;
            parameters[2].Value = userId;
            parameters[3].Value = withdrawId;
            
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

        public bool Pay(ColoPay.Model.Pay.Withdraw withdrawModel, ColoPay.Model.Pay.BalanceDetail detailModel,int userId)
        {

            //事务处理
            List<CommandInfo> sqllist = new List<CommandInfo>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Pay_Withdraw set ");
            strSql.Append("Status=@Status,");
            strSql.Append("PayDate=@Date,");
            strSql.Append("PayUserId=@UserId ");
            strSql.Append(" where WithdrawId=@WithdrawId");
            SqlParameter[] parameters = {
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@Date", SqlDbType.DateTime),
                    new SqlParameter("@UserId", SqlDbType.Int,4),
                    new SqlParameter("@WithdrawId", SqlDbType.Int,4)};
            parameters[0].Value = withdrawModel.Status;
            parameters[1].Value = DateTime.Now;
            parameters[2].Value = userId;
            parameters[3].Value = withdrawModel.WithdrawId;
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

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
            parameters2[0].Value = detailModel.EnterpriseID;
            parameters2[1].Value = detailModel.PayType;
            parameters2[2].Value = detailModel.OriginalId;
            parameters2[3].Value = detailModel.OriginalCode;
            parameters2[4].Value = detailModel.PaymentFee;
            parameters2[5].Value = detailModel.OrderAmount; 
            parameters2[6].Value = detailModel.Amount;
            parameters2[7].Value = detailModel.CreatedTime;

            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            return DBHelper.DefaultDBHelper.ExecuteSqlTran(sqllist) > 0 ? true : false;
        }
        /// <summary>
        /// 余额在提交申请的时候就扣除，防止多次提交申请
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddWithdraw(ColoPay.Model.Pay.Withdraw model)
        {

            //事务处理
            List<CommandInfo> sqllist = new List<CommandInfo>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Pay_Withdraw(");
            strSql.Append("WithdrawCode,EnterpriseID,AgentId,Type,UserName,Amount,WithdrawBank,WithdrawInfo,WithdrawNum,Status,CreatedDate,CreatedUserId,AuditDate,AuditUserId,PayDate,PayUserId,Remark)");
            strSql.Append(" values (");
            strSql.Append("@WithdrawCode,@EnterpriseID,@AgentId,@Type,@UserName,@Amount,@WithdrawBank,@WithdrawInfo,@WithdrawNum,@Status,@CreatedDate,@CreatedUserId,@AuditDate,@AuditUserId,@PayDate,@PayUserId,@Remark)");
            SqlParameter[] parameters = {
                    new SqlParameter("@WithdrawCode", SqlDbType.NVarChar,100),
                    new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
                    new SqlParameter("@AgentId", SqlDbType.Int,4),
                    new SqlParameter("@Type", SqlDbType.Int,4),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,200),
                    new SqlParameter("@Amount", SqlDbType.Money,8),
                    new SqlParameter("@WithdrawBank", SqlDbType.NVarChar,300),
                    new SqlParameter("@WithdrawInfo", SqlDbType.NVarChar,200),
                    new SqlParameter("@WithdrawNum", SqlDbType.NVarChar,200),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@CreatedDate", SqlDbType.DateTime),
                    new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
                    new SqlParameter("@AuditDate", SqlDbType.DateTime),
                    new SqlParameter("@AuditUserId", SqlDbType.Int,4),
                    new SqlParameter("@PayDate", SqlDbType.DateTime),
                    new SqlParameter("@PayUserId", SqlDbType.Int,4),
                    new SqlParameter("@Remark", SqlDbType.NVarChar,300)};
            parameters[0].Value = model.WithdrawCode;
            parameters[1].Value = model.EnterpriseID;
            parameters[2].Value = model.AgentId;
            parameters[3].Value = model.Type;
            parameters[4].Value = model.UserName;
            parameters[5].Value = model.Amount;
            parameters[6].Value = model.WithdrawBank;
            parameters[7].Value = model.WithdrawInfo;
            parameters[8].Value = model.WithdrawNum;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.CreatedDate;
            parameters[11].Value = model.CreatedUserId;
            parameters[12].Value = model.AuditDate;
            parameters[13].Value = model.AuditUserId;
            parameters[14].Value = model.PayDate;
            parameters[15].Value = model.PayUserId;
            parameters[16].Value = model.Remark;

            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);
            if (model.EnterpriseID > 0 && model.Type == 0)
            {
                StringBuilder strSql1 = new StringBuilder();
                strSql1.Append("update Pay_Enterprise set  Balance=Balance-@Amount ");
                strSql1.Append(" where EnterpriseID=@EnterpriseID  ");
                SqlParameter[] parameters1 = {
                        new SqlParameter("@EnterpriseID", SqlDbType.Int,4),
                          new SqlParameter("@Amount", SqlDbType.Decimal)
                                         };
                parameters1[0].Value = model.EnterpriseID;
                parameters1[1].Value = model.Amount;
                cmd = new CommandInfo(strSql1.ToString(), parameters1);
                sqllist.Add(cmd);

            }
           else {
                StringBuilder strSql1 = new StringBuilder();
                strSql1.Append("update Pay_Agent set  Balance=Balance-@Amount ");
                strSql1.Append(" where AgentId=@AgentId  ");
                SqlParameter[] parameters1 = {
                        new SqlParameter("@AgentId", SqlDbType.Int,4),
                          new SqlParameter("@Amount", SqlDbType.Decimal)
                                         };
                parameters1[0].Value = model.AgentId;
                parameters1[1].Value = model.Amount;
                cmd = new CommandInfo(strSql1.ToString(), parameters1);
                sqllist.Add(cmd);
            }

            return DBHelper.DefaultDBHelper.ExecuteSqlTran(sqllist) > 0 ? true : false;
        }

        #endregion  ExtensionMethod
    }
}

