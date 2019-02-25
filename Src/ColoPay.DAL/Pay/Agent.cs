/**  版本信息模板在安装目录下，可自行修改。
* Agent.cs
*
* 功 能： N/A
* 类 名： Agent
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
	/// 数据访问类:Agent
	/// </summary>
	public partial class Agent
	{
		public Agent()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("AgentId", "Pay_Agent"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int AgentId)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Pay_Agent");
			strSql.Append(" where AgentId=@AgentId");
			SqlParameter[] parameters = {
					new SqlParameter("@AgentId", SqlDbType.Int,4)
			};
			parameters[0].Value = AgentId;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ColoPay.Model.Pay.Agent model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Pay_Agent(");
			strSql.Append("Name,UserName,ParentId,Status,Balance,CreatedDate,CreatedUserId,BusinessLicense,TelPhone,CellPhone,AccountBank,AccountInfo,AccountNum,WithdrawBank,WithdrawInfo,WithdrawNum,ContactMail,Address,RegisterIp,Remark)");
			strSql.Append(" values (");
			strSql.Append("@Name,@UserName,@ParentId,@Status,@Balance,@CreatedDate,@CreatedUserId,@BusinessLicense,@TelPhone,@CellPhone,@AccountBank,@AccountInfo,@AccountNum,@WithdrawBank,@WithdrawInfo,@WithdrawNum,@ContactMail,@Address,@RegisterIp,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@Balance", SqlDbType.Money,8),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
					new SqlParameter("@BusinessLicense", SqlDbType.NVarChar,200),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@CellPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@AccountBank", SqlDbType.NVarChar,300),
					new SqlParameter("@AccountInfo", SqlDbType.NVarChar,200),
					new SqlParameter("@AccountNum", SqlDbType.NVarChar,200),
					new SqlParameter("@WithdrawBank", SqlDbType.NVarChar,300),
					new SqlParameter("@WithdrawInfo", SqlDbType.NVarChar,200),
					new SqlParameter("@WithdrawNum", SqlDbType.NVarChar,200),
					new SqlParameter("@ContactMail", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,50),
					new SqlParameter("@RegisterIp", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.ParentId;
			parameters[3].Value = model.Status;
			parameters[4].Value = model.Balance;
			parameters[5].Value = model.CreatedDate;
			parameters[6].Value = model.CreatedUserId;
			parameters[7].Value = model.BusinessLicense;
			parameters[8].Value = model.TelPhone;
			parameters[9].Value = model.CellPhone;
			parameters[10].Value = model.AccountBank;
			parameters[11].Value = model.AccountInfo;
			parameters[12].Value = model.AccountNum;
			parameters[13].Value = model.WithdrawBank;
			parameters[14].Value = model.WithdrawInfo;
			parameters[15].Value = model.WithdrawNum;
			parameters[16].Value = model.ContactMail;
			parameters[17].Value = model.Address;
			parameters[18].Value = model.RegisterIp;
			parameters[19].Value = model.Remark;

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
		public bool Update(ColoPay.Model.Pay.Agent model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Pay_Agent set ");
			strSql.Append("Name=@Name,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("ParentId=@ParentId,");
			strSql.Append("Status=@Status,");
			strSql.Append("Balance=@Balance,");
			strSql.Append("CreatedDate=@CreatedDate,");
			strSql.Append("CreatedUserId=@CreatedUserId,");
			strSql.Append("BusinessLicense=@BusinessLicense,");
			strSql.Append("TelPhone=@TelPhone,");
			strSql.Append("CellPhone=@CellPhone,");
			strSql.Append("AccountBank=@AccountBank,");
			strSql.Append("AccountInfo=@AccountInfo,");
			strSql.Append("AccountNum=@AccountNum,");
			strSql.Append("WithdrawBank=@WithdrawBank,");
			strSql.Append("WithdrawInfo=@WithdrawInfo,");
			strSql.Append("WithdrawNum=@WithdrawNum,");
			strSql.Append("ContactMail=@ContactMail,");
			strSql.Append("Address=@Address,");
			strSql.Append("RegisterIp=@RegisterIp,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where AgentId=@AgentId");
			SqlParameter[] parameters = {
					new SqlParameter("@Name", SqlDbType.NVarChar,100),
					new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@ParentId", SqlDbType.Int,4),
					new SqlParameter("@Status", SqlDbType.SmallInt,2),
					new SqlParameter("@Balance", SqlDbType.Money,8),
					new SqlParameter("@CreatedDate", SqlDbType.DateTime),
					new SqlParameter("@CreatedUserId", SqlDbType.Int,4),
					new SqlParameter("@BusinessLicense", SqlDbType.NVarChar,200),
					new SqlParameter("@TelPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@CellPhone", SqlDbType.NVarChar,50),
					new SqlParameter("@AccountBank", SqlDbType.NVarChar,300),
					new SqlParameter("@AccountInfo", SqlDbType.NVarChar,200),
					new SqlParameter("@AccountNum", SqlDbType.NVarChar,200),
					new SqlParameter("@WithdrawBank", SqlDbType.NVarChar,300),
					new SqlParameter("@WithdrawInfo", SqlDbType.NVarChar,200),
					new SqlParameter("@WithdrawNum", SqlDbType.NVarChar,200),
					new SqlParameter("@ContactMail", SqlDbType.NVarChar,50),
					new SqlParameter("@Address", SqlDbType.NVarChar,50),
					new SqlParameter("@RegisterIp", SqlDbType.NVarChar,50),
					new SqlParameter("@Remark", SqlDbType.NVarChar,1000),
					new SqlParameter("@AgentId", SqlDbType.Int,4)};
			parameters[0].Value = model.Name;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.ParentId;
			parameters[3].Value = model.Status;
			parameters[4].Value = model.Balance;
			parameters[5].Value = model.CreatedDate;
			parameters[6].Value = model.CreatedUserId;
			parameters[7].Value = model.BusinessLicense;
			parameters[8].Value = model.TelPhone;
			parameters[9].Value = model.CellPhone;
			parameters[10].Value = model.AccountBank;
			parameters[11].Value = model.AccountInfo;
			parameters[12].Value = model.AccountNum;
			parameters[13].Value = model.WithdrawBank;
			parameters[14].Value = model.WithdrawInfo;
			parameters[15].Value = model.WithdrawNum;
			parameters[16].Value = model.ContactMail;
			parameters[17].Value = model.Address;
			parameters[18].Value = model.RegisterIp;
			parameters[19].Value = model.Remark;
			parameters[20].Value = model.AgentId;

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
		public bool Delete(int AgentId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Pay_Agent ");
			strSql.Append(" where AgentId=@AgentId");
			SqlParameter[] parameters = {
					new SqlParameter("@AgentId", SqlDbType.Int,4)
			};
			parameters[0].Value = AgentId;

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
		public bool DeleteList(string AgentIdlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Pay_Agent ");
			strSql.Append(" where AgentId in ("+AgentIdlist + ")  ");
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
		public ColoPay.Model.Pay.Agent GetModel(int AgentId)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 AgentId,Name,UserName,ParentId,Status,Balance,CreatedDate,CreatedUserId,BusinessLicense,TelPhone,CellPhone,AccountBank,AccountInfo,AccountNum,WithdrawBank,WithdrawInfo,WithdrawNum,ContactMail,Address,RegisterIp,Remark from Pay_Agent ");
			strSql.Append(" where AgentId=@AgentId");
			SqlParameter[] parameters = {
					new SqlParameter("@AgentId", SqlDbType.Int,4)
			};
			parameters[0].Value = AgentId;

			ColoPay.Model.Pay.Agent model=new ColoPay.Model.Pay.Agent();
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
		public ColoPay.Model.Pay.Agent DataRowToModel(DataRow row)
		{
			ColoPay.Model.Pay.Agent model=new ColoPay.Model.Pay.Agent();
			if (row != null)
			{
				if(row["AgentId"]!=null && row["AgentId"].ToString()!="")
				{
					model.AgentId=int.Parse(row["AgentId"].ToString());
				}
				if(row["Name"]!=null)
				{
					model.Name=row["Name"].ToString();
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["ParentId"]!=null && row["ParentId"].ToString()!="")
				{
					model.ParentId=int.Parse(row["ParentId"].ToString());
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
				}
				if(row["Balance"]!=null && row["Balance"].ToString()!="")
				{
					model.Balance=decimal.Parse(row["Balance"].ToString());
				}
				if(row["CreatedDate"]!=null && row["CreatedDate"].ToString()!="")
				{
					model.CreatedDate=DateTime.Parse(row["CreatedDate"].ToString());
				}
				if(row["CreatedUserId"]!=null && row["CreatedUserId"].ToString()!="")
				{
					model.CreatedUserId=int.Parse(row["CreatedUserId"].ToString());
				}
				if(row["BusinessLicense"]!=null)
				{
					model.BusinessLicense=row["BusinessLicense"].ToString();
				}
				if(row["TelPhone"]!=null)
				{
					model.TelPhone=row["TelPhone"].ToString();
				}
				if(row["CellPhone"]!=null)
				{
					model.CellPhone=row["CellPhone"].ToString();
				}
				if(row["AccountBank"]!=null)
				{
					model.AccountBank=row["AccountBank"].ToString();
				}
				if(row["AccountInfo"]!=null)
				{
					model.AccountInfo=row["AccountInfo"].ToString();
				}
				if(row["AccountNum"]!=null)
				{
					model.AccountNum=row["AccountNum"].ToString();
				}
				if(row["WithdrawBank"]!=null)
				{
					model.WithdrawBank=row["WithdrawBank"].ToString();
				}
				if(row["WithdrawInfo"]!=null)
				{
					model.WithdrawInfo=row["WithdrawInfo"].ToString();
				}
				if(row["WithdrawNum"]!=null)
				{
					model.WithdrawNum=row["WithdrawNum"].ToString();
				}
				if(row["ContactMail"]!=null)
				{
					model.ContactMail=row["ContactMail"].ToString();
				}
				if(row["Address"]!=null)
				{
					model.Address=row["Address"].ToString();
				}
				if(row["RegisterIp"]!=null)
				{
					model.RegisterIp=row["RegisterIp"].ToString();
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
			strSql.Append("select AgentId,Name,UserName,ParentId,Status,Balance,CreatedDate,CreatedUserId,BusinessLicense,TelPhone,CellPhone,AccountBank,AccountInfo,AccountNum,WithdrawBank,WithdrawInfo,WithdrawNum,ContactMail,Address,RegisterIp,Remark ");
			strSql.Append(" FROM Pay_Agent ");
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
			strSql.Append(" AgentId,Name,UserName,ParentId,Status,Balance,CreatedDate,CreatedUserId,BusinessLicense,TelPhone,CellPhone,AccountBank,AccountInfo,AccountNum,WithdrawBank,WithdrawInfo,WithdrawNum,ContactMail,Address,RegisterIp,Remark ");
			strSql.Append(" FROM Pay_Agent ");
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
			strSql.Append("select count(1) FROM Pay_Agent ");
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
				strSql.Append("order by T.AgentId desc");
			}
			strSql.Append(")AS Row, T.*  from Pay_Agent T ");
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
			parameters[0].Value = "Pay_Agent";
			parameters[1].Value = "AgentId";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        public int GetAgentId(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select Top 1 AgentId  FROM Pay_Agent  where  UserName='{0}'", userName);

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
        #endregion  ExtensionMethod

        #region  周 20190101增加  用户名和企业名是否重复
        /// <summary>
        /// 是否用户名是否存在
        /// </summary>
        public bool ExistsUsername(string strUsername)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Pay_Agent");
            strSql.Append(" where UserName=@strUsername");
            SqlParameter[] parameters = {
                    new SqlParameter("@strUsername", SqlDbType.VarChar,50)
            };
            parameters[0].Value = strUsername;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 修改用户名是否存在
        /// </summary>
        public bool ExistsUsername(string Agentid, string strUsername)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Pay_Agent");
            strSql.Append(" where AgentId<>@AgentId and  UserName=@strUsername");
            SqlParameter[] parameters = {
                    new SqlParameter("@AgentId", SqlDbType.Int,4),
                    new SqlParameter("@strUsername", SqlDbType.VarChar,50)
            };
            parameters[0].Value = Agentid;
            parameters[1].Value = strUsername;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 输入用户名，获得姓名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetNameByusername(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select Top 1 Name  FROM Pay_Agent  where  UserName='{0}'", userName);

            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return "";
            }
            else
            {
                return obj.ToString();
            }
        }


        public int GetAgentID(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select Top 1 AgentId  FROM Pay_Agent  where  UserName='{0}'", userName);

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

        #endregion
    }
}

