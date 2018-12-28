using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YSWL.Common.IIS
{
    public class IISManager
    {
        private  static readonly ServerManager serverManager = new ServerManager();

        #region 显示站点列表
        //public static void getListOfIIS()
        //{
        //    this.listBox1.Items.Clear();
        //    string StateStr = "";
        //    for (int i = 0; i < IISManager.Sites.Count; i++)
        //    {

        //        switch (IISManager.Sites[i].State)
        //        {
        //            case ObjectState.Started:
        //                {
        //                    StateStr = "正常"; break;
        //                }
        //            case ObjectState.Starting:
        //                {
        //                    StateStr = "正在启动"; break;
        //                }
        //            case ObjectState.Stopping:
        //                {
        //                    StateStr = "正在关闭"; break;
        //                }
        //            case ObjectState.Stopped:
        //                {
        //                    StateStr = "关闭"; break;
        //                }
        //        }
        //        this.listBox1.Items.Add(IISManager.Sites[i].Name + "【" + StateStr + "】");
        //    }
        //} 
        #endregion

        #region 创建站点、绑定域名
        //public static void Creat(string[] args)
        //{
        //    Site site = serverManager.Sites.Add("site name", "http", "*:80:" + siteUrl, sitePath);
        //    mySite.ServerAutoStart = true;
        //    serverManager.CommitChanges();
        //}
        #endregion

        #region 站点权限设置
        //        Configuration config = serverManager.GetApplicationHostConfiguration();
        //        ConfigurationSection anonymousAuthenticationSection = config.GetSection("system.webServer/security/authentication/anonymousAuthentication", sitename);
        //        anonymousAuthenticationSection["enabled"] = true;
        //anonymousAuthenticationSection["userName"] = @"IUSR_" + this.txt_No.Text.ToString();
        //anonymousAuthenticationSection["password"] = @"" + this.txt_password.Text.ToString();
        //serverManager.CommitChanges();
        #endregion

        #region 创建应用池
        //        ApplicationPool newPool = IISManager.ApplicationPools[appoolname];    
        //if (newPool == null)
        //{
        //    IISManager.ApplicationPools.Add(appoolname);
        //    newPool = IISManager.ApplicationPools[appoolname];
        //    newPool.Name =appoolname;
        //    newPool.ManagedPipelineMode = ManagedPipelineMode.Integrated;
        //    newPool.ManagedRuntimeVersion = "v2.0";
        //}
        #endregion

        #region 站点应用池绑定
        //site.Applications["/"].ApplicationPoolName  = appoolName //此appoolname就是新的的引用池
        #endregion

        #region  追加域名
        public static bool AddHost(string siteName, string domain, int port = 80, string ip = "*")
        {
            if (string.IsNullOrWhiteSpace(siteName) || string.IsNullOrWhiteSpace(domain)) return false;

            try
            {
                Site site = serverManager.Sites[siteName];
                string bindInfo = $"{ip}:{port}:{domain}";
                BindingCollection binds= site.Bindings;
                if (binds.AllowsAdd && binds.All(xx => xx.BindingInformation != bindInfo))
                {
                    binds.Add(bindInfo,"http");
                }
                serverManager.CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                FileManage.WriteText(new System.Text.StringBuilder(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " YSWL.Common.IIS.IISWebsite.AddHost:" + ex.Message));
            }
            return false;
        }
        #endregion
        
        #region  删除域名
        public static bool RemoveHost(string siteName, string domain, int port = 80, string ip = "*")
        {
            if (string.IsNullOrWhiteSpace(siteName) || string.IsNullOrWhiteSpace(domain)) return false;

            try
            {
                Site site = serverManager.Sites[siteName];
                string bindInfo = $"{ip}:{port}:{domain}";
                BindingCollection binds = site.Bindings;
                if (binds.AllowsRemove && binds.Any(xx => xx.BindingInformation != bindInfo))
                {
                    Binding binding = binds.First(xx => xx.BindingInformation == bindInfo);
                    binds.Remove(binding);
                }
                serverManager.CommitChanges();
                return true;
            }
            catch (Exception ex)
            {
                FileManage.WriteText(new System.Text.StringBuilder(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " YSWL.Common.IIS.IISWebsite.RemoveHost:" + ex.Message));
            }
            return false;
        }
        #endregion
    }
}
