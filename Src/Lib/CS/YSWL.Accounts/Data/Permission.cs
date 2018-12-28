using System.Data;
using System.Data.SqlClient;
using System.Text;
using YSWL.Accounts.IData;
using YSWL.DBUtility;

namespace YSWL.Accounts.Data
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public class Permission : IPermission
    {

        #region
        /// <summary>
        /// 创建一个权限
        /// </summary>
        public int Create(int categoryID, string description)
        {
            int rowsAffected;
            SqlParameter[] parameters = 
                {
                    new SqlParameter("@CategoryID", SqlDbType.Int, 8),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 50)
                };
            parameters[0].Value = categoryID;
            parameters[1].Value = description;

            return DBHelper.DefaultDBHelper.RunProcedure("sp_Accounts_CreatePermission", parameters, out rowsAffected);
        }

        /// <summary>
        /// 更新权限信息
        /// </summary>
        public bool Update(int PermissionID, string description)
        {
            int rowsAffected;
            SqlParameter[] parameters = 
                {
                    new SqlParameter("@PermissionID", SqlDbType.Int, 8),
                    new SqlParameter("@Description", SqlDbType.NVarChar, 50)
                };
            parameters[0].Value = PermissionID;
            parameters[1].Value = description;

            DBHelper.DefaultDBHelper.RunProcedure("sp_Accounts_UpdatePermission", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }

        public void UpdateCategory(string PermissionIDlist, int CategoryID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Accounts_Permissions set ");
            strSql.AppendFormat(" CategoryID={0}", CategoryID);
            strSql.AppendFormat(" where PermissionID in({0})", PermissionIDlist);

            DBHelper.DefaultDBHelper.ExecuteSql(strSql.ToString());

        }


        /// <summary>
        /// 删除权限
        /// </summary>        
        public bool Delete(int id)
        {
            int rowsAffected;
            SqlParameter[] parameters =
                {
                    new SqlParameter("@PermissionID", SqlDbType.Int, 4)
                };
            parameters[0].Value = id;
            DBHelper.DefaultDBHelper.RunProcedure("sp_Accounts_DeletePermission", parameters, out rowsAffected);
            return (rowsAffected == 1);
        }

        /// <summary>
        /// 根据权限ID获取权限信息
        /// </summary>
        public DataSet Retrieve(int permissionId)
        {
            SqlParameter[] parameters = { new SqlParameter("@PermissionID", SqlDbType.Int, 4) };
            parameters[0].Value = permissionId;
            return DBHelper.DefaultDBHelper.RunProcedure("sp_Accounts_GetPermissionDetails", parameters, "Permissions");

        }

        /// <summary>
        /// 获取权限列表
        /// </summary>        
        public DataSet GetPermissionList()
        {
            SqlParameter[] parameters = { new SqlParameter("@RoleID", SqlDbType.NVarChar, 4) };
            using (DataSet permissions = DBHelper.DefaultDBHelper.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[] { }, "Categories"))
            {
                DBHelper.DefaultDBHelper.RunProcedure("sp_Accounts_GetPermissionList", parameters, permissions, "Permissions");
                DataRelation permissionCategories = new DataRelation("PermissionCategories",
                    permissions.Tables["Categories"].Columns["CategoryID"],
                    permissions.Tables["Permissions"].Columns["CategoryID"], true);
                permissions.Relations.Add(permissionCategories);
                DataColumn[] categoryKeys = new DataColumn[1];
                categoryKeys[0] = permissions.Tables["Categories"].Columns["CategoryID"];
                DataColumn[] permissionKeys = new DataColumn[1];
                permissionKeys[0] = permissions.Tables["Permissions"].Columns["PermissionID"];
                permissions.Tables["Categories"].PrimaryKey = categoryKeys;
                permissions.Tables["Permissions"].PrimaryKey = permissionKeys;
                return permissions;
            }
        }

        #endregion



        /// <summary>
        /// 获取指定角色的权限列表
        /// </summary>        
        public DataSet GetPermissionList(int roleId)
        {
            SqlParameter[] parameters = { new SqlParameter("@RoleID", SqlDbType.NVarChar, 4) };
            parameters[0].Value = roleId;
            using (DataSet permissions = DBHelper.DefaultDBHelper.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[] { }, "Categories"))
            {
                DBHelper.DefaultDBHelper.RunProcedure("sp_Accounts_GetPermissionList", parameters, permissions, "Permissions");
                DataRelation permissionCategories = new DataRelation("PermissionCategories",
                    permissions.Tables["Categories"].Columns["CategoryID"],
                    permissions.Tables["Permissions"].Columns["CategoryID"], true);
                permissions.Relations.Add(permissionCategories);
                DataColumn[] categoryKeys = new DataColumn[1];
                categoryKeys[0] = permissions.Tables["Categories"].Columns["CategoryID"];
                DataColumn[] permissionKeys = new DataColumn[1];
                permissionKeys[0] = permissions.Tables["Permissions"].Columns["PermissionID"];
                permissions.Tables["Categories"].PrimaryKey = categoryKeys;
                permissions.Tables["Permissions"].PrimaryKey = permissionKeys;
                return permissions;
            }
        }

        /// <summary>
        /// 获取指定角色没有的权限列表
        /// </summary>        
        public DataSet GetNoPermissionList(int roleId)
        {
            SqlParameter[] parameters = { new SqlParameter("@RoleID", SqlDbType.NVarChar, 4) };
            parameters[0].Value = roleId;
            using (DataSet permissions = DBHelper.DefaultDBHelper.RunProcedure("sp_Accounts_GetPermissionCategories", new IDataParameter[] { }, "Categories"))
            {
                DBHelper.DefaultDBHelper.RunProcedure("sp_Accounts_GetNoPermissionList", parameters, permissions, "Permissions");
                DataRelation permissionCategories = new DataRelation("PermissionCategories",
                    permissions.Tables["Categories"].Columns["CategoryID"],
                    permissions.Tables["Permissions"].Columns["CategoryID"], true);
                permissions.Relations.Add(permissionCategories);
                DataColumn[] categoryKeys = new DataColumn[1];
                categoryKeys[0] = permissions.Tables["Categories"].Columns["CategoryID"];
                DataColumn[] permissionKeys = new DataColumn[1];
                permissionKeys[0] = permissions.Tables["Permissions"].Columns["PermissionID"];
                permissions.Tables["Categories"].PrimaryKey = categoryKeys;
                permissions.Tables["Permissions"].PrimaryKey = permissionKeys;
                return permissions;
            }
        }




    }
}
