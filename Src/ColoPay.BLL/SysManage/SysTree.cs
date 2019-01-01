using System;
using System.Data;
using ColoPay.Model.SysManage;
using YSWL.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace ColoPay.BLL.SysManage
{
    /// <summary>
    /// ϵͳ�˵�����
    /// </summary>
    public class SysTree
    {
        private readonly ColoPay.DAL.SysManage.SysTree dal = new DAL.SysManage.SysTree();


        public int GetPermissionCatalogID(int permissionID)
        {
            return dal.GetPermissionCatalogID(permissionID);
        }
        public SysTree()
        {
        }

        public int AddTreeNode(SysNode node)
        {
            return dal.AddTreeNode(node);
        }
        public void UpdateNode(SysNode node)
        {
            dal.UpdateNode(node);
        }
        public void DelTreeNode(int nodeid)
        {
            dal.DelTreeNode(nodeid);
        }
        public void DelTreeNodes(string nodeidlist)
        {
            dal.DelTreeNodes(nodeidlist);
        }
        public void MoveNodes(string nodeidlist, int ParentID)
        {
            dal.MoveNodes(nodeidlist, ParentID);
        }

        public DataSet GetTreeList(string strWhere)
        {
            return dal.GetTreeList(strWhere);
        }

        /// <summary>
        /// ��ȡȫ���˵�����
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllTree()
        {
            return dal.GetTreeList("");
        }

        /// <summary>
        /// ���ݲ˵����ͻ�ȡ��Ӧ�˵�����
        /// </summary>
        /// <param name="treeType">�˵����� 0:admin��̨ 1:��ҵ��̨  2:�����̺�̨ 3:�û���̨</param>
        /// <returns></returns>
        public DataSet GetAllTreeByType(int treeType)
        {
            return dal.GetTreeList("TreeType=" + treeType);
        }

        /// <summary>
        /// ���ݲ˵����ͻ�ȡ���õĲ˵�����
        /// </summary>
        /// <param name="treeType">�˵����� 0:admin��̨ 1:��ҵ��̨  2:�����̺�̨ 3:�û���̨</param>
        /// <returns></returns>
        public DataSet GetAllEnabledTreeByType(int treeType)
        {
            return GetAllEnabledTreeByType(treeType, true);
        }

        /// <summary>
        /// ���ݲ˵����ͻ�ȡ��Ӧ�˵�����
        /// </summary>
        /// <param name="treeType">�˵����� 0:admin��̨ 1:��ҵ��̨  2:�����̺�̨ 3:�û���̨</param>
        /// <param name="Enabled">�Ƿ�����</param>
        /// <returns></returns>
        public DataSet GetAllEnabledTreeByType(int treeType, bool Enabled)
        {
            return dal.GetTreeList("TreeType=" + treeType + " AND Enabled = " + (Enabled ? "1" : "0"));
        }

        /// <summary>
        /// ���ݲ˵����ͻ�ȡ��Ӧ�˵�����
        /// </summary>
        /// <param name="parentID">��ID</param>
        /// <param name="treeType">�˵����� 0:admin��̨ 1:��ҵ��̨  2:�����̺�̨ 3:�û���̨</param>
        /// <param name="Enabled">�Ƿ�����</param>
        /// <returns></returns>
        public DataSet GetEnabledTreeByParentId(int parentID, int treeType, bool Enabled)
        {
            return dal.GetTreeList("ParentID=" + parentID + " AND TreeType=" + treeType + " AND Enabled = " + (Enabled ? "1" : "0"));
        }

        /// <summary>
        /// Get an object list��From the cache
        /// <param name="treeType">�˵����� 0:admin��̨ 1:��ҵ��̨  2:�����̺�̨ 3:�û���̨</param>
        /// </summary>
        public DataSet GetAllEnabledTreeByType4Cache(int treeType)
        {
            string CacheKey = "GetAllEnabledTreeByType4Cache" + treeType;
            object objModel = YSWL.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetAllEnabledTreeByType(treeType);
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        YSWL.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (DataSet)objModel;
        }


        public DataSet GetTreeSonList(int NodeID, int treeType, List<int> UserPermissions)
        {
            string strWhere = " Enabled=1 and TreeType=" + treeType;
            if (NodeID > -1)
            {
                strWhere += " and parentid=" + NodeID;
            }
            if (UserPermissions.Count > 0)
            {
                strWhere += " and (PermissionID=-1 or PermissionID in (" + StringPlus.GetArrayStr(UserPermissions) + "))";
            }
            return dal.GetTreeList(strWhere);
        }

        public SysNode GetNode(int NodeID)
        {
            return dal.GetNode(NodeID);
        }

        /// <summary>
        /// Get an object entity��From the cache
        /// </summary>
        public SysNode GetModelByCache(int NodeID)
        {

            string CacheKey = "SysManageModel-" + NodeID;
            object objModel = YSWL.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetNode(NodeID);
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        YSWL.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (SysNode)objModel;
        }


        /// <summary>
        /// �޸�����״̬
        /// </summary>
        /// <param name="nodeid"></param>
        public void UpdateEnabled(int nodeid)
        {
           dal.UpdateEnabled(nodeid);
        }


        public List<ColoPay.Model.SysManage.SysNode> GetTreeListByType(int treeType,bool Enabled)
        {
            ////�������ü��ض�Ӧ�Ĳ˵�
            //bool IsXMLTree = ColoPay.BLL.SysManage.ConfigSystem.GetBoolValueByCache("SAAS_Menu_IsXML");
            //if (IsXMLTree)
            //{
            //    return GetAllTreeListXmlCache(treeType);
            //}

            DataSet ds = GetAllEnabledTreeByType(treeType, Enabled);
            List<ColoPay.Model.SysManage.SysNode> NodeList= DataTableToList(ds.Tables[0]);
            foreach (var sysNode in NodeList)
            {
                int count = NodeList.Where(c => c.ParentID == sysNode.NodeID).Count();
                if (count == 0)
                    sysNode.hasChildren = false;
            }
            return NodeList;
        }


        public List<ColoPay.Model.SysManage.SysNode> GetTreeListByTypeCache(int treeType, bool Enabled, bool IsAutoConn)
        {
            //�������ü��ض�Ӧ�Ĳ˵�
            bool IsXMLTree = ColoPay.BLL.SysManage.ConfigSystem.GetBoolValueByCache("Shop_Menu_IsXML");
            if (IsXMLTree)
            {
                List<ColoPay.Model.SysManage.SysNode> allSysNodes = GetAllTreeListXmlCache(treeType);

                return allSysNodes;
            }

            string CacheKey = "GetTreeListByTypeCache-" + treeType + Enabled;
            object objModel = YSWL.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetTreeListByType(treeType, Enabled);
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        YSWL.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<ColoPay.Model.SysManage.SysNode>)objModel;
        }

        private static bool IsAppRelation(List<long> applist, string appStr)
        {
            if (String.IsNullOrWhiteSpace(appStr))
            {
                return true;
            }
            var appArr = appStr.Split(',');
            foreach (var item in applist)
            {
                if (appArr.Contains(item.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ��������б�
        /// </summary>
        public List<ColoPay.Model.SysManage.SysNode> DataTableToList(DataTable dt)
		{
            List<ColoPay.Model.SysManage.SysNode> modelList = new List<ColoPay.Model.SysManage.SysNode>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                ColoPay.Model.SysManage.SysNode model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

        #region ��־����
        public void AddLog(string time, string loginfo, string Particular)
        {
            dal.AddLog(time, loginfo, Particular);
        }
        public void DelOverdueLog(int days)
        {
            dal.DelOverdueLog(days);
        }
        public void DeleteLog(string Idlist)
        {
            string str = "";
            if (Idlist.Trim() != "")
            {
                str = " ID in (" + Idlist + ")";
            }
            dal.DeleteLog(str);
        }
        public void DeleteLog(string timestart, string timeend)
        {
            string str = " datetime>'" + timestart + "' and datetime<'" + timeend + "'";
            dal.DeleteLog(str);
        }
        public DataSet GetLogs(string strWhere)
        {
            return dal.GetLogs(strWhere);
        }
        public DataRow GetLog(string ID)
        {
            return dal.GetLog(ID);
        }

        #endregion

        #region  �˵����� XML
        /// <summary>
        /// ��ȡ���еĲ˵�
        /// </summary>
        /// <returns></returns>
        public static List<SysNode> GetAllTreeXml()
        {
            List<SysNode> menuList = new List<SysNode>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath("/Config/Menu.config"));
            //ȡָ���Ľ��ļ���
            XmlNodeList nodes = xmlDoc.SelectNodes("menus/menu");
            if (nodes != null)
            {
                SysNode model = null;
                foreach (var item in nodes)
                {
                    XmlElement node = (XmlElement)item;
                    model = new SysNode();
                    model.NodeID = YSWL.Common.Globals.SafeInt(node.GetAttribute("NodeID"), 0);
                    model.TreeText = node.GetAttribute("TreeText");
                    model.OrderID = YSWL.Common.Globals.SafeInt(node.GetAttribute("OrderID"), 0);
                    model.ParentID = YSWL.Common.Globals.SafeInt(node.GetAttribute("ParentID"), 0);
                    model.PermissionID = YSWL.Common.Globals.SafeInt(node.GetAttribute("PermissionID"), 0);
                    model.ImageUrl = node.GetAttribute("ImageUrl");
                    model.TreeType = YSWL.Common.Globals.SafeInt(node.GetAttribute("TreeType"), 0);
                    model.Url = node.GetAttribute("Url");
                    model.Enabled = YSWL.Common.Globals.SafeBool(node.GetAttribute("Enabled"), false);
                    model.AppStr = node.GetAttribute("ApplicationID");
                    menuList.Add(model);
                }
            }
            return menuList;
        }
        /// <summary>
        /// ��ȡ���õ�XML�˵�
        /// </summary>
        /// <returns></returns>
        public List<ColoPay.Model.SysManage.SysNode> GetAllTreeListXmlCache(int treeType)
        {
            string CacheKey = "GetAllTreeListXmlCache_"+ treeType;
            object objModel = YSWL.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    List<SysNode> allList = GetAllTreeXml();
                    objModel = allList.Where(c => c.Enabled&&c.TreeType== treeType).ToList();
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        YSWL.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<ColoPay.Model.SysManage.SysNode>)objModel;
        }
        /// <summary>
        /// ��Ӳ˵�
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddMenu(SysNode model)
        {
            try
            {
                string xmlFile = HttpContext.Current.Server.MapPath("/Config/Menu.config");
                XDocument xmlDoc = XDocument.Load(xmlFile);
                XElement newElement = new XElement("menu",
                    new XAttribute("NodeID", model.NodeID),
                    new XAttribute("TreeText", model.TreeText),
                    new XAttribute("OrderID", model.OrderID.HasValue ? model.OrderID.Value : 0),
                    new XAttribute("ParentID", model.ParentID),
                    new XAttribute("PermissionID", model.NodeID),
                    new XAttribute("ImageUrl", model.ImageUrl),
                    new XAttribute("TreeType", model.TreeType),
                    new XAttribute("Url", model.Url),
                    new XAttribute("Enabled", model.Enabled)
                    );
                XElement root = xmlDoc.Element("menus");
                if (root != null)
                {
                    //��ӵĽڵ��Ƿ���ڣ�������ھ����Ƴ�Ȼ������ӡ�
                    XElement menu = root.Elements().FirstOrDefault(c => YSWL.Common.Globals.SafeInt(c.Attribute("NodeID").Value, 0) == model.NodeID);
                    if (menu != null)
                    {
                        menu.Remove();
                    }
                    root.Add(newElement);
                }
                xmlDoc.Save(xmlFile);
                return true;
            }
            catch (Exception ex)
            {
                YSWL.Log.LogHelper.AddErrorLog("���XML�˵�ʧ�ܣ�" + ex.Message, ex.StackTrace);
                throw;
            }

        }
        /// <summary>
        /// ͬ�����е����ݿ�˵���XML�˵� ����ʼ��һ���Ե��ã�
        /// </summary>
        /// <returns></returns>
        public static bool SyncMenu()
        {
           // ColoPay.BLL.SysManage.SysTree treeBll = new SysTree();
            //int treeType = 4; //YSWL.Common.ConfigHelper.GetConfigInt("TreeType");
            List<SysNode> allList = GetAllTreeXml();//treeBll.GetTreeListByType(treeType, true);
            foreach (var item in allList)
            {
                AddMenu(item);
            }
            return true;
        }
        #endregion 
    }
}
