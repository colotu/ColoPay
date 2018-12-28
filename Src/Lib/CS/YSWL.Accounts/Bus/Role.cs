using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization.Configuration;
using YSWL.Accounts.IData;

namespace YSWL.Accounts.Bus
{
    /// <summary>
    /// ��ɫ����
    /// </summary>
    [Serializable]
    public class Role
    {
        #region ����
        private int roleId;
        private string description;
        private DataSet permissions;
        private DataSet nopermissions;
        private DataSet users;
        /// <summary>
        /// ��ɫ���
        /// </summary>
        public int RoleID
        {
            get
            {
                return roleId;
            }
            set
            {
                roleId = value;
            }
        }
        /// <summary>
        /// ��ɫ����
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        /// <summary>
        /// �ý�ɫӵ�е�Ȩ���б�
        /// </summary>
        public DataSet Permissions
        {
            get
            {
                return permissions;
            }
        }
        /// <summary>
        /// �ý�ɫû�е�Ȩ���б�
        /// </summary>
        public DataSet NoPermissions
        {
            get
            {
                return nopermissions;
            }
        }
        /// <summary>
        /// �ý�ɫ�µ������û�
        /// </summary>
        public DataSet Users
        {
            get
            {
                return users;
            }
        }
        #endregion

        private IData.IRole dataRole;

        #region ����

        /// <summary>
        /// ���캯��
        /// </summary>
        public Role()
        {
            dataRole = PubConstant.IsSQLServer ? (IRole)new Data.Role() : new MySqlData.Role();
        }


        /// <summary>
        /// ���ݽ�ɫID�����ɫ����Ϣ
        /// </summary>
        public Role(int currentRoleId)
        {
            dataRole = PubConstant.IsSQLServer ? (IRole)new Data.Role() : new MySqlData.Role();
            DataRow roleRow;
            roleRow = dataRole.Retrieve(currentRoleId);
            roleId = currentRoleId;
            if (roleRow["Description"] != null)
            {
                description = (string)roleRow["Description"];
            }
            IData.IPermission dataPermission = PubConstant.IsSQLServer ? (IPermission)new Data.Permission() : new MySqlData.Permission();
            permissions = dataPermission.GetPermissionList(currentRoleId);
            nopermissions = dataPermission.GetNoPermissionList(currentRoleId);

            IData.IUser user = PubConstant.IsSQLServer ? (IUser)new Data.User() : new MySqlData.User();
            users = user.GetUsersByRole(currentRoleId);
        }

        /// <summary>
        /// �Ƿ���ڸý�ɫ
        /// </summary>
        public bool RoleExists(string Description)
        {
            return dataRole.RoleExists(Description);
        }
        /// <summary>
        /// ���ӽ�ɫ
        /// </summary>
        public int Create()
        {
            roleId = dataRole.Create(description);
            return roleId;
        }
        /// <summary>
        /// ���½�ɫ
        /// </summary>
        public bool Update()
        {
            return dataRole.Update(roleId, description);
        }
        /// <summary>
        /// ɾ����ɫ
        /// </summary>
        public bool Delete()
        {
            return dataRole.Delete(roleId);
        }
        /// <summary>
        /// Ϊ��ɫ����Ȩ��
        /// </summary>
        public void AddPermission(int permissionId)
        {
            dataRole.AddPermission(roleId, permissionId);
        }
        /// <summary>
        /// �ӽ�ɫ�Ƴ�Ȩ��
        /// </summary>
        public void RemovePermission(int permissionId)
        {
            dataRole.RemovePermission(roleId, permissionId);
        }
        /// <summary>
        /// ��ս�ɫ��Ȩ��
        /// </summary>
        public void ClearPermissions()
        {
            dataRole.ClearPermissions(roleId);
        }
        /// <summary>
        /// ��ȡ���н�ɫ���б�
        /// </summary>
        public DataSet GetRoleList()
        {
            return dataRole.GetRoleList();
        }
        #endregion

        #region  �û���ɫȨ�޹�������

        public List<UserRoles> GetALLUserRole()
        {
            DataSet ds = dataRole.GetALLUserRole();
            List<UserRoles> modelList = new List<UserRoles>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                UserRoles model=null;
                for (int n = 0; n < rowsCount; n++)
                {
                    DataRow row = ds.Tables[0].Rows[n];
                    model=new UserRoles();
                    if (row != null)
                    {
                        if (row["RoleID"] != null && row["RoleID"].ToString() != "")
                        {
                            model.RoleID =Common.Globals.SafeInt(row["RoleID"],0);
                        }
                        if (row["UserID"] != null)
                        {
                            model.UserID = Common.Globals.SafeInt(row["UserID"],0);
                        }
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        public List<RolePermissions> GetALLRolePerm()
        {
            DataSet ds = dataRole.GetALLRolePerm();
            List<RolePermissions> modelList = new List<RolePermissions>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                RolePermissions model = null;
                for (int n = 0; n < rowsCount; n++)
                {
                    DataRow row = ds.Tables[0].Rows[n];
                    model = new RolePermissions();
                    if (row != null)
                    {
                        if (row["RoleID"] != null && row["RoleID"].ToString() != "")
                        {
                            model.RoleID = Common.Globals.SafeInt(row["RoleID"], 0);
                        }
                        if (row["PermissionID"] != null)
                        {
                            model.PermissionID = Common.Globals.SafeInt(row["PermissionID"], 0);
                        }
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }


        #endregion
    }


    #region ��ɫȨ��
    public class RolePermissions
    {
        private int _roleID;
        /// <summary>
        /// �û���ɫ
        /// </summary>
        public int RoleID
        {
            get
            {
                return _roleID;
            }
            set
            {
                _roleID = value;
            }
        }

        private int _permissionID;
        /// <summary>
        /// �û�Ȩ��
        /// </summary>
        public int PermissionID
        {
            get
            {
                return _permissionID;
            }
            set
            {
                _permissionID = value;
            }
        }

    }
    #endregion


    #region  �û���ɫ
    public class UserRoles
    {
        private int _roleID;
        /// <summary>
        /// �û���ɫ
        /// </summary>
        public int RoleID
        {
            get
            {
                return _roleID;
            }
            set
            {
                _roleID = value;
            }
        }

        private int _userID;
        /// <summary>
        /// �û�ID
        /// </summary>
        public int UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }
    }
    #endregion 
}
