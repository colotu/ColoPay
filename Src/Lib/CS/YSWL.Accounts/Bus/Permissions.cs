using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using YSWL.Accounts.IData;
using YSWL.Common;
using YSWL.DBUtility;
using YSWL.SAAS.BLL;

namespace YSWL.Accounts.Bus
{
    /// <summary>
    /// Ȩ�޹���
    /// </summary>
    [Serializable]
    public class Permissions
    {
        private IData.IPermission dalPermission = PubConstant.IsSQLServer
                                                       ? (IPermission)new Data.Permission()
                                                       : new MySqlData.Permission();


        private int _permissionID;
        /// <summary>
        /// �û����
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


        private string _description;
        /// <summary>
        /// �û����
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        private int _categoryID;
        /// <summary>
        /// �û����
        /// </summary>
        public int CategoryID
        {
            get
            {
                return _categoryID;
            }
            set
            {
                _categoryID = value;
            }
        }


        /// <summary>
        /// ���캯��
        /// </summary>
        public Permissions()
        {
        }



        /// <summary>
        /// ����һ��Ȩ��
        /// </summary>
        /// <param name="pcID">���ID</param>
        /// <param name="description">Ȩ������</param>
        /// <returns></returns>
        public int Create(int pcID, string description)
        {
            int pID = dalPermission.Create(pcID, description);
            return pID;
        }
        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="pcID">Ȩ��ID</param>
        /// <param name="description">Ȩ������</param>
        /// <returns></returns>
        public bool Update(int pcID, string description)
        {
            return dalPermission.Update(pcID, description);
        }

        public void UpdateCategory(string PermissionIDlist, int CategoryID)
        {
            dalPermission.UpdateCategory(PermissionIDlist, CategoryID);
        }

        /// <summary>
        /// ɾ��Ȩ��
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public bool Delete(int pID)
        {
            return dalPermission.Delete(pID);
        }

        /// <summary>
        /// �õ�Ȩ������
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public string GetPermissionName(int permissionId)
        {
            DataSet permissions = dalPermission.Retrieve(permissionId);
            if (permissions.Tables[0].Rows.Count == 0)
            {
                throw new Exception("�Ҳ���Ȩ�� ��" + permissionId + "��");
            }
            else
            {
                return permissions.Tables[0].Rows[0]["Description"].ToString();
            }
        }

        /// <summary>
        /// �õ�Ȩ����Ϣ
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public void GetPermissionDetails(int pID)
        {
            DataSet permissions = dalPermission.Retrieve(pID);
            if (permissions.Tables[0].Rows.Count > 0)
            {
                if (permissions.Tables[0].Rows[0]["PermissionID"] != null)
                {
                    _permissionID = Convert.ToInt32(permissions.Tables[0].Rows[0]["PermissionID"]);
                }
                _description = permissions.Tables[0].Rows[0]["Description"].ToString();
                if (permissions.Tables[0].Rows[0]["CategoryID"] != null)
                {
                    _categoryID = Convert.ToInt32(permissions.Tables[0].Rows[0]["CategoryID"]);
                }
            }
        }


        #region  XML  Ȩ�޴���

        private static DataCacheCore dataCache = new DataCacheCore(new CacheOption
        {
            CacheType = SAASInfo.GetSystemBoolValue("RedisCacheUse") ? CacheType.Redis : CacheType.IIS,
            ReadWriteHosts = SAASInfo.GetSystemValue("RedisCacheReadWriteHosts"),
            ReadOnlyHosts = SAASInfo.GetSystemValue("RedisCacheReadOnlyHosts"),
            CancelProductKey = true,
            CancelEnterpriseKey = true,
            DefaultDb = 1
        });
        /// <summary>
        /// ��ȡ���е�XML Ȩ��
        /// </summary>
        /// <returns></returns>
        public static List<Permissions> GetAllXMLPermission()
        {
            string path = HttpContext.Current.Server.MapPath("/Config/Permission.config");
            List<Permissions> permissionList = new List<Permissions>();
            if (!File.Exists(path))
            {
                return permissionList;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            //ȡָ���Ľ��ļ���
            XmlNodeList nodes = xmlDoc.SelectNodes("permissions/application");
            if (nodes != null)
            {
                Permissions model = null;
                foreach (XmlElement parentNode in nodes)
                {
                    foreach (XmlElement node in parentNode.ChildNodes)
                    {
                        model = new Permissions
                        {
                            PermissionID = Common.Globals.SafeInt(node.GetAttribute("PermissionID"), 0),
                            Description = node.GetAttribute("Description"),
                            CategoryID = Common.Globals.SafeInt(node.GetAttribute("CategoryID"), 0)
                        };
                        permissionList.Add(model);
                    }
                }
            }
            return permissionList;
        }
        /// <summary>
        /// ��ȡ���е�XML Ȩ�޻���
        /// </summary>
        /// <returns></returns>
        public static List<Permissions> GetAllXMLPermissionCache()
        {
            string CacheKey = "GetAllXMLPermissionCache";
            List<Permissions> allPermissions = dataCache.GetCache<List<Permissions>>(CacheKey);
            if (allPermissions == null)
            {
                allPermissions = GetAllXMLPermission();
                if (allPermissions != null)
                {
                    dataCache.SetCache(CacheKey, allPermissions, DateTime.MaxValue, TimeSpan.Zero);
                }
            }
            return allPermissions;
        }
        /// <summary>
        /// ��ȡĳ�û���Ȩ��
        /// </summary>
        /// <returns></returns>
        public static List<Permissions> GetPermissionByUser(int userId)
        {
            List<Permissions> ALLList = GetAllXMLPermissionCache();
            //��ȡ���е��û���ɫ����
            YSWL.Accounts.Bus.Role roleBll=new Role();
            List<UserRoles> ALLUserRoleList= roleBll.GetALLUserRole();
            //��ȡ���еĽ�ɫȨ�޹���
            List<RolePermissions> ALLRolePermList = roleBll.GetALLRolePerm();

            #region  ����Ӧ�ò˵�
            #endregion 

            #region  ����Ȩ���߼�

            List<UserRoles> userRoleList = ALLUserRoleList.Where(c => c.UserID == userId).ToList();

            #region  ����ϵͳ����ԱȨ�ޣ�ϵͳ����ԱĬ�ϼ������е�Ȩ��
            var systemRole= userRoleList.Find(c => c.RoleID == 1);
            if (systemRole != null) //�������ϵͳ����Ա��ɫ����������е�Ȩ��
            {
                return ALLList;
            }
            #endregion

            if (userRoleList == null || userRoleList.Count == 0)
            {
                return null;
            }
            List<RolePermissions> rolePermList = ALLRolePermList.Where(c => userRoleList.Select(k=>k.RoleID).Contains(c.RoleID)).ToList();

            if (rolePermList==null || rolePermList.Count==0)
            {
                return null;
            }
            List<Permissions> permissionList =
                ALLList.Where(c => rolePermList.Select(k => k.PermissionID).Contains(c.PermissionID)).ToList();
            #endregion

            return permissionList;  
        }

        #endregion
    }


}
