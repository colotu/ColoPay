using System.Data;
using System.Data.SqlClient;
using System.Text;
using YSWL.Accounts.IData;
using YSWL.DBUtility;

namespace YSWL.Accounts.Data
{
    /// <summary>
    ///Ȩ�����
    /// </summary>
    public class PermissionCategory : IPermissionCategory
    {
        public PermissionCategory()
        { }

        /// <summary>
        /// ����Ȩ�����
        /// </summary>        
        public int Create(string description)
        {
            int rowsAffected;
            SqlParameter[] parameters = 
                {
                    new SqlParameter("@Description", SqlDbType.NVarChar, 50)
                };
            parameters[0].Value = description;

            return DBHelper.DefaultDBHelper.RunProcedure("sp_Accounts_CreatePermissionCategory", parameters, out rowsAffected);
        }

        /// <summary>
        /// ��������Ƿ����Ȩ�޼�¼
        /// </summary>
        public bool ExistsPerm(int CategoryID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Accounts_Permissions");
            strSql.Append(" where CategoryID=@CategoryID");
            SqlParameter[] parameters = {
                    new SqlParameter("@CategoryID", SqlDbType.Int,4)
            };
            parameters[0].Value = CategoryID;

            return DBHelper.DefaultDBHelper.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// ɾ��Ȩ�����
        /// </summary>        
        public bool Delete(int CategoryID)
        {
            int rowsAffected;
            SqlParameter[] parameters =
                {
                    new SqlParameter("@CategoryID", SqlDbType.Int, 4)
                };
            parameters[0].Value = CategoryID;
            DBHelper.DefaultDBHelper.RunProcedure("sp_Accounts_DeletePermissionCategory", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }

        /// <summary>
        /// ��ȡȨ�������Ϣ
        /// </summary>        
        public DataRow Retrieve(int categoryId)
        {
            SqlParameter[] parameters = 
                {
                    new SqlParameter("@CategoryID", SqlDbType.Int, 4)
                };
            parameters[0].Value = categoryId;

            using (DataSet categories = DBHelper.DefaultDBHelper.RunProcedure("sp_Accounts_GetPermissionCategoryDetails", parameters, "Categories"))
            {
                return categories.Tables[0].Rows[0];
            }
        }

        /// <summary>
        /// ��ȡָ������µ�Ȩ���б�
        /// </summary>        
        public DataSet GetPermissionsInCategory(int categoryId)
        {
            SqlParameter[] parameters =
                {
                    new SqlParameter("@CategoryID", SqlDbType.Int, 4)
                };
            parameters[0].Value = categoryId;
            using (DataSet permissions = DBHelper.DefaultDBHelper.RunProcedure("sp_Accounts_GetPermissionsInCategory",
                       parameters, "Categories"))
            {
                return permissions;
            }
        }

        /// <summary>
        /// ��ȡȨ�������б�
        /// </summary>        
        public DataSet GetCategoryList()
        {
            using (DataSet categories = DBHelper.DefaultDBHelper.RunProcedure("sp_Accounts_GetPermissionCategories",
                       new IDataParameter[] { },
                       "Categories"))
            {
                return categories;
            }
        }
    }
}
