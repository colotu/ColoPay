using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using YSWL.DBUtility;

namespace ColoPay.SQLServerDAL.SysManage
{
    
    /// <summary>
    /// �û���־�Ĳ�����
    /// </summary>
    public class UserLog  
    {
                
        /// <summary>
        /// ������־
        /// </summary>
        /// <param name="model"></param>
        public void LogUserAdd(ColoPay.Model.SysManage.UserLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SA_UserLog(");
            strSql.Append("OPTime,Url,OPInfo,UserName,UserType,UserIP)");
            strSql.Append(" values (");
            strSql.Append("@OPTime,@Url,@OPInfo,@UserName,@UserType,@UserIP)");
            SqlParameter[] parameters = {
					new SqlParameter("@OPTime", SqlDbType.DateTime),
					new SqlParameter("@Url", SqlDbType.NVarChar,200),
					new SqlParameter("@OPInfo", SqlDbType.NVarChar),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@UserType", SqlDbType.Char,2),
                    new SqlParameter("@UserIP", SqlDbType.NVarChar,20)};
            parameters[0].Value = DateTime.Now;
            parameters[1].Value = model.Url;
            parameters[2].Value = model.OPInfo;
            parameters[3].Value = model.UserName;
            parameters[4].Value = model.UserType;
            parameters[5].Value = model.UserIP;

            DBHelper.DefaultDBHelper.ExecuteSql(strSql.ToString(), parameters);
        }
                    
        /// <summary>
        /// ���ݲ�ѯ������ȡ��־�б�
        /// </summary>
        /// <param name="strWhere">��ѯ����</param>
        /// <returns>���ص����ݼ�</returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [ID],[OPTime],[url],[OPInfo],[UserName],[UserType],[UserIp] ");
            strSql.Append(" FROM SA_UserLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere + " Order By [OPTime] Desc ");
            }
            else
            {
                strSql.Append(" Order By [OPTime] Desc ");
            }
            return DBHelper.DefaultDBHelper.Query(strSql.ToString());
        }


        /// <summary>
        /// ɾ��һ����־��¼
        /// </summary>
        /// <param name="iID">Ҫɾ������־���</param>
        public void LogUserDelete(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SA_UserLog ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
				};
            parameters[0].Value = ID;
            DBHelper.DefaultDBHelper.ExecuteSql(strSql.ToString(), parameters);
        }


        public void LogUserDelete(string IdList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SA_UserLog ");
            strSql.Append(" where ID in("+IdList+")");            
            DBHelper.DefaultDBHelper.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// ɾ��ĳһ����֮ǰ������
        /// </summary>
        /// <param name="dtDateBefore">����</param>
        public void LogUserDelete(DateTime dtDateBefore)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SA_UserLog ");
            strSql.Append(" where OPTime <= @OPTime");
            SqlParameter[] parameters = {
					new SqlParameter("@OPTime", SqlDbType.DateTime)
				};
            parameters[0].Value = dtDateBefore;
            DBHelper.DefaultDBHelper.ExecuteSql(strSql.ToString(), parameters);
        }



        /// <summary>
        /// ɾ��ĳһ����֮ǰ�������ô洢����
        /// </summary>
        /// <param name="dtDateBefore">����</param>
        public void LogDelete(DateTime dtDateBefore)
        {
            SqlParameter[] parameters = { new SqlParameter("@OPTime", SqlDbType.DateTime), };

            parameters[0].Value = dtDateBefore;
            DBHelper.DefaultDBHelper.RunProcedure("sp_LogUser_delete", parameters);

        }


        /// <summary>
        /// �õ�Ҫ��ѯ�����ݵ�����
        /// </summary>
        /// <param name="strTable">Ҫ��ѯ�ı�</param>
        /// <param name="strWhere">��ѯ������,���û��������1=1</param>
        /// <returns>���صļ�¼����</returns>
        public int GetCount( string strWhere)
        {
            string strCmd = "select count(*) from  SA_UserLog   where  " + strWhere;
            int iResult = Convert.ToInt32(DBHelper.DefaultDBHelper.GetSingle(strCmd));
            return iResult;
        }

        
    }
   


}
