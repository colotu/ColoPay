﻿/**
* MvcApplication.cs
*
* 功 能： Global
* 类 名： MvcApplication
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/8/27 12:00:33   Ben    初版
*
* Copyright (c) 2012 YSWL Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：云商未来（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YSWL.Common;
using YSWL.Components.Filters;
using YSWL.Web;

namespace YSWL.Components
{
    #region IApplicationOption接口
    /// <summary>
    /// IApplicationOption接口
    /// </summary>
    public interface IApplicationOption
    {
        /// <summary>
        /// 授权码
        /// </summary>
        string AuthorizeCode { get; }

        /// <summary>
        /// 网站版权
        /// </summary>
        string WebPowerBy { get; }

        /// <summary>
        /// 备案信息
        /// </summary>
        string WebRecord { get; }

        /// <summary>
        /// 统计脚本
        /// </summary>
        string PageFootJs { get; }

        /// <summary>
        /// 网站名称
        /// </summary>
        string SiteName { get; }

        /// <summary>
        /// 模版名称
        /// </summary>
        string ThemeName { get; }

        /// <summary>
        /// 获取指定区域的模版名称
        /// </summary>
        string GetAreaThemeName(AreaRoute areaRoute, ControllerContext context = null);
    }
    #endregion

    /// <summary>
    /// HttpApplication 基类
    /// </summary>
    public abstract class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// ApplicationOption接口 必须使用子类静态构造函数初始化
        /// </summary>
        protected static IApplicationOption ApplicationOption;

        private static string _defaultUploadFolder = string.Empty;

        /// <summary>
        /// 上传文件跟目录
        /// </summary>
        public static string UploadFolder
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_defaultUploadFolder))
                {
                    _defaultUploadFolder = ConfigHelper.GetConfigString("UploadFolder");

                    if (string.IsNullOrWhiteSpace(_defaultUploadFolder))
                        _defaultUploadFolder = "Upload";
                }

                if (IsAutoConn)
                {
                    return $"{_defaultUploadFolder}/{CallContextHelper.GetEnterpriseId()}";
                }
                return _defaultUploadFolder;
            }
        }

        /// <summary>
        /// 是否已安装
        /// </summary>
        public static bool IsInstall;

        /// <summary>
        /// 是否自动连接数据库
        /// </summary>
        public static bool IsAutoConn;
        /// <summary>
        /// 网站名称
        /// </summary>
        public static string SiteName
        {
            get { return ApplicationOption == null ? string.Empty : ApplicationOption.SiteName; }
        }
        /// <summary>
        /// 模版名称
        /// </summary>
        public static string ThemeName
        {
            get { return ApplicationOption == null ? string.Empty : ApplicationOption.ThemeName; }
        }

        #region 效验授权

        private const string KEY_ISAUTHORIZE = "YSWL_IsAuthorize";

        public static bool IsAuthorize
        {
            get
            {
                if (IsAutoConn)//如果是SAAS 自动跳过校验
                {
                    return true;
                }
                if (ApplicationOption == null) return false;
                if (HttpContext.Current == null) return false;
                if (HttpContext.Current.Session != null)
                {
                    bool? isAuthorize = Globals.SafeBool(HttpContext.Current.Session[KEY_ISAUTHORIZE], null);
                    if (!isAuthorize.HasValue)
                    {
                        //重新效验
                        HttpContext.Current.Session[KEY_ISAUTHORIZE] = isAuthorize =
                            YSWL.Authorize.AuthController.CheckAuthorize
                                (
                                    ApplicationOption.AuthorizeCode, ProductInfoFull);
                    }
                    return isAuthorize.Value;
                }
                else
                {
                    //NO SESSION FROM APPLICATION
                    bool? isAuthorize = Globals.SafeBool(HttpContext.Current.Application[KEY_ISAUTHORIZE], null);
                    if (!isAuthorize.HasValue)
                    {
                        //重新效验
                        HttpContext.Current.Application[KEY_ISAUTHORIZE] = isAuthorize =
                           YSWL.Authorize.AuthController.CheckAuthorize(
                                    ApplicationOption.AuthorizeCode, ProductInfoFull);
                    }
                    return isAuthorize.Value;
                }
            }
            //set { if (HttpContext.Current != null) HttpContext.Current.Application[KEY_ISAUTHORIZE] = value; }
        }
        #endregion

        public static string WebPowerBy
        {
            get { return ApplicationOption == null ? string.Empty : ApplicationOption.WebPowerBy; }
        }

        public static string WebRecord
        {
            get { return ApplicationOption == null ? string.Empty : ApplicationOption.WebRecord; }
        }

        public static string PageFootJs
        {
            get { return ApplicationOption == null ? string.Empty : ApplicationOption.PageFootJs; }
        }


        #region 子类实现

        protected abstract string MainArea { get; }


        protected abstract void ApplicationStart();

        #endregion

        public static string Version;
        public static string ProductInfo;
        public static string ProductInfoFull;

        //SET FOR SA_Config_System Key:MainArea 只能加载一次, 随运行池销毁(重置)
        public static AreaRoute MainAreaRoute;

        protected static readonly System.Collections.Generic.List<string> AreaList = new System.Collections.Generic.List<string>();
        private static string staticHost;

        #region 构造初始化

        public MvcApplication()
        {
            IsAutoConn = Common.ConfigHelper.GetConfigBool("AutoConnection");

            IsInstall = Common.ConfigHelper.GetConfigBool("Installer");
            if (!IsInstall) return; //未安装 终止

            if (MainArea == null)
                throw new ArgumentNullException("MvcApplication: MainArea Is NULL!");
            MainAreaRoute = Globals.SafeEnum<AreaRoute>(MainArea, AreaRoute.None, true);
            staticHost = YSWL.Common.ConfigHelper.GetConfigString("StaticWebUrl");

            if (string.IsNullOrWhiteSpace(staticHost))
                staticHost = "http://static.yuns56.cn";
            else
                staticHost = staticHost.TrimEnd('/');
        }

        #endregion

        #region 获取当前区域/路由信息
        /// <summary>
        /// 当前系统是否有指定区域
        /// </summary>
        /// <remarks>在运行池启动时加载目录</remarks>
        /// <param name="area">Area</param>
        public static bool HasArea(AreaRoute area)
        {
            if (area == AreaRoute.None) return false;

            if (HttpContext.Current == null) throw new ArgumentNullException();

            if (AreaList.Count < 1)
            {
                #region 加载区域目录
                string mapPath = HttpContext.Current.Server.MapPath("/Areas");
                if (!System.IO.Directory.Exists(mapPath)) return false;

                System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(mapPath);
                System.IO.DirectoryInfo[] subInfos = directoryInfo.GetDirectories();
                if (subInfos.Length < 1) return false;

                foreach (System.IO.DirectoryInfo item in subInfos)
                {
                    AreaList.Add(item.Name);
                }
                #endregion
            }

            return AreaList.Contains(area.ToString());
        }

        /// <summary>
        /// 静态站Host
        /// </summary>
        public static string StaticHost
        {
            get
            {
                return staticHost;
            }
        }

        /// <summary>
        /// 获取模版名称
        /// </summary>
        public static string GetCurrentThemeName(AreaRoute currentArea)
        {
            if (currentArea == MainAreaRoute)
            {
                return ThemeName;
            }
            else
            {
                return ApplicationOption == null ? "Default" : ApplicationOption.GetAreaThemeName(currentArea);
            }
        }

        /// <summary>
        /// 获取当前区域标识
        /// </summary>
        /// <param name="controllerContext">当前ControllerContext</param>
        /// <returns>AreaRoute</returns>
        public static AreaRoute GetCurrentAreaRoute(ControllerContext controllerContext)
        {
            if (controllerContext == null) return AreaRoute.None;

            return GetCurrentAreaRoute(controllerContext.RouteData.DataTokens["area"]);
        }
        /// <summary>
        /// 获取当前区域标识
        /// </summary>
        /// <param name="currentArea">当前路由</param>
        /// <returns>AreaRoute</returns>
        public static AreaRoute GetCurrentAreaRoute(object currentArea)
        {
            if (currentArea == null) return AreaRoute.None;

            return Globals.SafeEnum<AreaRoute>(currentArea.ToString(), AreaRoute.None, true);
        }

        #region 获取当前路由的访问路径(前缀)

        /// <summary>
        /// 获取当前路由的访问路径(前缀)
        /// </summary>
        /// <param name="controllerContext">当前ControllerContext</param>
        /// <returns>访问前缀</returns>
        public static string GetCurrentRoutePath(ControllerContext controllerContext)
        {
            return GetCurrentRoutePath(GetCurrentAreaRoute(controllerContext));
        }

        /// <summary>
        /// 获取当前路由的访问路径(前缀)
        /// </summary>
        /// <param name="currentArea">当前路由</param>
        /// <returns>访问前缀</returns>
        public static string GetCurrentRoutePath(object currentArea)
        {
            return GetCurrentRoutePath(GetCurrentAreaRoute(currentArea));
        }

        /// <summary>
        /// 获取当前路由的访问路径(前缀)
        /// </summary>
        /// <param name="currentArea">当前路由</param>
        /// <returns>访问前缀</returns>
        public static string GetCurrentRoutePath(AreaRoute currentArea)
        {
            return (currentArea != MvcApplication.MainAreaRoute
                        ? string.Format("/{0}/", currentArea)
                        : "/");
        }
        #endregion

        #region 获取当前View的访问路径(前缀)
        /// <summary>
        /// 获取当前View的访问路径(前缀)
        /// </summary>
        /// <param name="currentArea">当前路由</param>
        /// <returns>View路径前缀</returns>
        public static string GetCurrentViewPath(object currentArea)
        {
            return GetCurrentViewPath(GetCurrentAreaRoute(currentArea));
        }
        /// <summary>
        /// 获取当前View的访问路径(前缀)
        /// </summary>
        /// <param name="currentArea">当前路由</param>
        /// <returns>View路径前缀</returns>
        public static string GetCurrentViewPath(AreaRoute currentArea)
        {
            return (currentArea != AreaRoute.None
                        ? string.Format("~/Areas/{0}/Themes/{1}/Views/", currentArea, GetCurrentThemeName(currentArea))
                        : string.Format("~/Views/"));
        }
        #endregion

        #endregion

        #region 路由配置

        /// <summary>
        /// 注册全局过滤器
        /// </summary>
        public virtual void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //注册区域路由过滤器
            filters.Add(new AreaRouteFilter());
        }

        /// <summary>
        /// 注册忽略路由
        /// </summary>
        /// <remarks>最高优先级</remarks>
        public virtual void RegisterIgnoreRoutes(RouteCollection routes)
        {
            RouteTable.Routes.RouteExistingFiles = false;
            //忽略浏览器图标Check 防止异常过多致IIS运行池重启
            routes.IgnoreRoute("favicon.ico");

            //忽略WebForm的设置
            routes.IgnoreRoute("Admin/{*pathInfo}");
            routes.IgnoreRoute("Admin/{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("API/{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("Upload/{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("{resource}.aspx/{*pathInfo}");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Ajax_Handle/{resource}.ashx/{*pathInfo}");
            routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");
        }

        #region 注册主路由
        /// <summary>
        /// 注册主路由
        /// </summary>
        /// <remarks>最低优先级</remarks>
        public virtual void RegisterRoutes(RouteCollection routes)
        {
            switch (MainAreaRoute)
            {
                //默认路由
                case AreaRoute.None:
                    routes.MapRoute(
                        name: "Default", // 路由名称
                        url: "{controller}/{action}/{id}", // 带有参数的 URL
                        defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 参数默认值
                        , namespaces: new string[] { "YSWL.Web.Controllers" }
                        ).DataTokens.Add("area", "None");
                    break;
                //区域主路由
                default:
                    string area = MainAreaRoute.ToString();

                    routes.MapRoute(
                        name: "Default",
                        url: "{controller}/{action}/{viewname}/{id}",
                        defaults: new
                        {
                            area = area,
                            controller = "Home",
                            action = "Index",
                            viewname = UrlParameter.Optional,
                            id = UrlParameter.Optional
                        },
                        constraints: new
                        {
                            viewname = @"^[A-Za-z_]+${0,50}", //大小写字母/下划线
                            id = @"[\d]{0,11}" //*表示数字长度11  //new { id = @"\d*" } 长度不限
                        }
                        , namespaces: new[] { string.Format("YSWL.Web.Areas.{0}.Controllers", area) }
                        ).DataTokens.Add("area", area);

                    routes.MapRoute(
                        name: "Default_Base",
                        url: "{controller}/{action}/{id}",
                        defaults: new { area = area, controller = "Home", action = "Index", id = UrlParameter.Optional }
                        , namespaces: new[] { string.Format("YSWL.Web.Areas.{0}.Controllers", area) }
                        ).DataTokens.Add("area", area);
                    break;
            }
        }
        #endregion

        #endregion

        #region Application_Start

        protected virtual void Application_Start()
        {

            #region 调用一次DBFactory

            YSWL.DBUtility.DBFactory.InitDBFactory();
            #endregion 
            #region 注册路由


            //注册忽略路由
            RegisterIgnoreRoutes(RouteTable.Routes);
            //注册区域路由
            AreaRegistration.RegisterAllAreas();
            //注册全局过滤器
            RegisterGlobalFilters(GlobalFilters.Filters);
            //注册主路由
            RegisterRoutes(RouteTable.Routes);
            //设置备用路由命名空间
            ControllerBuilder.Current.DefaultNamespaces.Clear();
            ControllerBuilder.Current.DefaultNamespaces.Add(
                string.Format("YSWL.Web.Areas.{0}.Controllers",
                MainAreaRoute));
            ControllerBuilder.Current.DefaultNamespaces.Add("YSWL.Web.Controllers");

            #endregion

            #region 子类操作

            ApplicationStart();

            #endregion

            #region 解析产品信息
            ProductInfoFull = "YS56" + (string.IsNullOrWhiteSpace(ProductInfo) ? "FK" : " " + ProductInfo);
            ProductInfo = ProductInfoFull.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[0x0];
            #endregion

            #region OnlineUsers

            //try
            //{
            //    DataTable userTable = new DataTable();
            //    userTable.Columns.Add("SessionID");
            //    userTable.Columns.Add("UserIP");
            //    userTable.Columns.Add("Browser");
            //    userTable.Columns.Add("OSName");
            //    //userTable.Columns.Add("Area");

            //    userTable.AcceptChanges();
            //    Application.Lock();
            //    Application["OnlineUsers"] = userTable;
            //    Application.UnLock();
            //}
            //catch
            //{ }

            #endregion

        }

        #region FirstRequestInitialization
        //protected class FirstRequestInitialization
        //{
        //    private static bool s_InitializedAlready = false;
        //    private static Object s_lock = new Object();
        //    // Initialize only on the first request
        //    public static void Initialize(HttpContext context)
        //    {
        //        if (s_InitializedAlready)
        //        {
        //            return;
        //        }
        //        lock (s_lock)
        //        {
        //            if (s_InitializedAlready)
        //            {
        //                return;
        //            }
        //            // Perform first-request initialization here ...
        //            s_InitializedAlready = true;

        //            #region Application_Start初始化代码

        //            // ..

        //            #endregion
        //        }
        //    }
        //}
        #endregion

        #endregion

        #region Session_Start

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["Style"] = 1;
            //Session["language"] = "zh-CN";
            if (!IsInstall)
            {
                //Response.Write("<script language='javascript'>window.top.location='/Installer/Default.aspx'</script>");
                if (Request.UrlReferrer == null)
                {
                    Server.Transfer("/Installer/Default.aspx");
                }
                return; //未安装 终止
            }
            //勿删，否则，表格样式会变形。

            #region 默认蓝

            Application["1xtop1_bgimage"] = "images/login1/top-1.gif"; //顶框背景图片
            Application["1xtop2_bgimage"] = "images/login1/top-2.gif"; //顶框背景图片
            Application["1xtop3_bgimage"] = "images/login1/top-3.gif"; //顶框背景图片
            Application["1xtop4_bgimage"] = "images/login1/top-4.gif"; //顶框背景图片
            Application["1xtop5_bgimage"] = "images/login1/top-5.gif"; //顶框背景图片
            Application["1xtopbj_bgimage"] = "images/login1/top-bj.gif"; //顶框背景图片



            Application["1xtopbar_bgimage"] = "images/login1/topbar_01.jpg"; //顶框工具条背景图片
            Application["1xfirstpage_bgimage"] = "images/login1/dbsx_01.gif"; //首页背景图片
            Application["1xforumcolor"] = "#f0f4fb";
            Application["1xleft_width"] = "204"; //左框架宽度


            Application["1xtree_bgcolor"] = "#e3eeff"; //左框架树背景色
            Application["1xleft1_bgimage"] = "images/login1/left-1.gif";
            Application["1xleft2_bgimage"] = "images/login1/left-2.gif";
            Application["1xleft3_bgimage"] = "images/login1/left-3.gif";
            Application["1xleftbj_bgimage"] = "images/login1/left-bj.gif";


            Application["1xspliter_color"] = "#6B7DDE"; //分隔块色


            Application["1xdesktop_bj"] = ""; //images/login1/right-bj.gif
            Application["1xdesktop_bgimage"] = "images/login1/desktop_01.gif"; //right.gif

            Application["1xtable_bgcolor"] = "#F5F9FF"; //最外层表格背景
            Application["1xtable_bordercolorlight"] = "#CCC"; //中层表格亮边框
            Application["1xtable_bordercolordark"] = "#CCC"; //中层表格暗边框
            Application["1xtable_titlebgcolor"] = "#E3EFFF"; //中层表格标题栏


            Application["1xform_requestcolor"] = "#E78A29"; //表单中必填字段*颜色

            Application["1xfirstpage_topimage"] = "images/login1/top_01.gif";
            Application["1xfirstpage_bottomimage"] = "images/login1/bottom_01.gif";
            Application["1xfirstpage_middleimage"] = "images/login1/bg_01.gif";

            Application["1xabout_bgimage"] = "images/login1/about_01.gif"; //关于我们背景图片

            #endregion

            #region 绿色


            Application["2xtop1_bgimage"] = "images/login1/top-1-2.gif"; //顶框背景图片
            Application["2xtop2_bgimage"] = "images/login1/top-2-2.gif"; //顶框背景图片
            Application["2xtop3_bgimage"] = "images/login1/top-3-2.gif"; //顶框背景图片
            Application["2xtop4_bgimage"] = "images/login1/top-4-2.gif"; //顶框背景图片
            Application["2xtop5_bgimage"] = "images/login1/top-5-2.gif"; //顶框背景图片
            Application["2xtopbj_bgimage"] = "images/login1/top-bj-2.gif"; //顶框背景图片

            Application["2xtopbar_bgimage"] = "images/login1/topbar_01.jpg"; //顶框工具条背景图片
            Application["2xfirstpage_bgimage"] = "images/login1/dbsx_01.gif"; //首页背景图片
            Application["2xforumcolor"] = "#f0f4fb";
            Application["2xleft_width"] = "204"; //左框架宽度


            Application["2xtree_bgcolor"] = "#e3ffe9"; //左框架树背景色
            Application["2xleft1_bgimage"] = "images/login1/left-1-2.gif";
            Application["2xleft2_bgimage"] = "images/login1/left-2-2.gif";
            Application["2xleft3_bgimage"] = "images/login1/left-3-2.gif";
            Application["2xleftbj_bgimage"] = "images/login1/left-bj-2.gif";

            Application["2xspliter_color"] = "#51C94F"; //分隔块色


            Application["2xdesktop_bj"] = ""; //images/login1/right-bj-2.gif
            Application["2xdesktop_bgimage"] = "images/login1/desktop_02.gif"; //right-2.gif


            Application["2xtable_bgcolor"] = "#F5FFF5"; //最外层表格背景
            Application["2xtable_bordercolorlight"] = "#7DBD7B"; //中层表格亮边框
            Application["2xtable_bordercolordark"] = "#D3E0D3"; //中层表格暗边框
            Application["2xtable_titlebgcolor"] = "#E4FFE3"; //中层表格标题栏


            Application["2xform_requestcolor"] = "#E78A29"; //表单中必填字段*颜色

            Application["2xfirstpage_topimage"] = "images/login1/top_01.gif";
            Application["2xfirstpage_bottomimage"] = "images/login1/bottom_01.gif";
            Application["2xfirstpage_middleimage"] = "images/login1/bg_01.gif";



            #endregion

            #region 红色

            Application["3xtop1_bgimage"] = "images/login1/top-1-1.gif"; //顶框背景图片
            Application["3xtop2_bgimage"] = "images/login1/top-2-1.gif"; //顶框背景图片
            Application["3xtop3_bgimage"] = "images/login1/top-3-1.gif"; //顶框背景图片
            Application["3xtop4_bgimage"] = "images/login1/top-4-1.gif"; //顶框背景图片
            Application["3xtop5_bgimage"] = "images/login1/top-5-1.gif"; //顶框背景图片
            Application["3xtopbj_bgimage"] = "images/login1/top-bj-1.gif"; //顶框背景图片

            Application["3xtopbar_bgimage"] = "images/login1/topbar_01.jpg"; //顶框工具条背景图片
            Application["3xfirstpage_bgimage"] = "images/login1/dbsx_01.gif"; //首页背景图片
            Application["3xforumcolor"] = "#f0f4fb";
            Application["3xleft_width"] = "204"; //左框架宽度


            Application["3xtree_bgcolor"] = "#ffe3e5"; //左框架树背景色			
            Application["3xleft1_bgimage"] = "images/login1/left-1-1.gif";
            Application["3xleft2_bgimage"] = "images/login1/left-2-1.gif";
            Application["3xleft3_bgimage"] = "images/login1/left-3-1.gif";
            Application["3xleftbj_bgimage"] = "images/login1/left-bj-1.gif";

            Application["3xspliter_color"] = "#C94F4F"; //分隔块色


            Application["3xdesktop_bj"] = ""; //images/login1/right-bj-1.gif
            Application["3xdesktop_bgimage"] = "images/login1/desktop_03.gif"; //right-1.gif


            Application["3xtable_bgcolor"] = "#FFF5F5"; //最外层表格背景
            Application["3xtable_bordercolorlight"] = "#BD7B7B"; //中层表格亮边框
            Application["3xtable_bordercolordark"] = "#E1D3D3"; //中层表格暗边框
            Application["3xtable_titlebgcolor"] = "#FFE3E3"; //中层表格标题栏


            Application["3xform_requestcolor"] = "#E78A29"; //表单中必填字段*颜色

            Application["3xfirstpage_topimage"] = "images/login1/top_01.gif";
            Application["3xfirstpage_bottomimage"] = "images/login1/bottom_01.gif";
            Application["3xfirstpage_middleimage"] = "images/login1/bg_01.gif";



            #endregion

            #region 深绿色


            Application["4xtop1_bgimage"] = "images/login1/top-1-3.gif"; //顶框背景图片
            Application["4xtop2_bgimage"] = "images/login1/top-2-3.gif"; //顶框背景图片
            Application["4xtop3_bgimage"] = "images/login1/top-3-3.gif"; //顶框背景图片
            Application["4xtop4_bgimage"] = "images/login1/top-4-3.gif"; //顶框背景图片
            Application["4xtop5_bgimage"] = "images/login1/top-5-3.gif"; //顶框背景图片
            Application["4xtopbj_bgimage"] = "images/login1/top-bj-3.gif"; //顶框背景图片

            Application["4xtopbar_bgimage"] = "images/login1/topbar_01.jpg"; //顶框工具条背景图片
            Application["4xfirstpage_bgimage"] = "images/login1/dbsx_01.gif"; //首页背景图片
            Application["4xforumcolor"] = "#f0f4fb";
            Application["4xleft_width"] = "204"; //左框架宽度


            Application["4xtree_bgcolor"] = "#e3ffe9"; //左框架树背景色			
            Application["4xleft1_bgimage"] = "images/login1/left-1-3.gif";
            Application["4xleft2_bgimage"] = "images/login1/left-2-3.gif";
            Application["4xleft3_bgimage"] = "images/login1/left-3-3.gif";
            Application["4xleftbj_bgimage"] = "images/login1/left-bj-3.gif";

            Application["4xspliter_color"] = "#51C94F"; //分隔块色


            Application["4xdesktop_bj"] = ""; //images/login1/right-bj-3.gif
            Application["4xdesktop_bgimage"] = "images/login1/desktop_02.gif"; //right-3.gif


            Application["4xtable_bgcolor"] = "#F5FFF5"; //最外层表格背景
            Application["4xtable_bordercolorlight"] = "#7DBD7B"; //中层表格亮边框
            Application["4xtable_bordercolordark"] = "#D3E0D3"; //中层表格暗边框
            Application["4xtable_titlebgcolor"] = "#E4FFE3"; //中层表格标题栏


            Application["4xform_requestcolor"] = "#E78A29"; //表单中必填字段*颜色

            Application["4xfirstpage_topimage"] = "images/login1/top_01.gif";
            Application["4xfirstpage_bottomimage"] = "images/login1/bottom_01.gif";
            Application["4xfirstpage_middleimage"] = "images/login1/bg_01.gif";



            #endregion

            #region OnlineUsers
            //try
            //{
            //    string seesionid = Session.SessionID;
            //    string UserIP = Request.UserHostAddress;
            //    HttpBrowserCapabilities bc = Request.Browser;
            //    string OSName = "Win2000";
            //    switch (bc.Platform)
            //    {
            //        case "Win98":
            //            OSName = "Windows98";
            //            break;
            //        case "WinNT 5.1":
            //        case "WinXP":
            //            OSName = "Windows XP";
            //            break;
            //        case "WinNT 5.0":
            //            OSName = "Win2000";
            //            break;
            //        case "WinNT":
            //            OSName = "Win7";
            //            break;
            //        default:
            //            OSName = bc.Platform;
            //            break;
            //    }
            //    string Browser = bc.Type;

            //    DataTable userTable = (DataTable)Application["OnlineUsers"];
            //    if (userTable == null)
            //        return;
            //    DataRow[] curRow = userTable.Select("SessionID='" + seesionid + "'");
            //    if (curRow.Length == 0)
            //    {
            //        DataRow newRow = userTable.NewRow();
            //        newRow["SessionID"] = seesionid;
            //        newRow["UserIP"] = UserIP;
            //        newRow["Browser"] = Browser;
            //        newRow["OSName"] = OSName;
            //        userTable.Rows.Add(newRow);

            //        userTable.AcceptChanges();

            //        Application.Lock();
            //        Application["OnlineUsers"] = userTable;
            //        Application.UnLock();
            //    }

            //    #region
            //    //if (Application["onlineCount"] == null)
            //    //{
            //    //    Application["onlineCount"] = 0;
            //    //}
            //    //int onlineCount = (int)Application["onlineCount"];
            //    //onlineCount++;
            //    //Application["onlineCount"] = onlineCount;
            //    #endregion
            //}
            //catch
            //{ }

            #endregion

            #region 设置企业标识

            //int enterpriseId = 0;
            //#region 个性域名检测
            ////SaaS域名
            //if (enterpriseId == 0 && MvcApplication.ProductInfo.Contains("SAAS"))
            //{
            //    string host = string.Empty;
            //    host = Request.Url.Host.ToLower();
            //    //启用个性域名访问
            //    int entId = DBUtility.SAASInfo.GetSAASEnterpriseIdByDomain(host);
            //    //FileManage.WriteText(new System.Text.StringBuilder(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + host + " entId:" + entId));
            //    if (entId < 0)
            //    {
            //        //域名未登记, 返回404
            //        Response.StatusCode = 404;
            //        FileManage.WriteText(new System.Text.StringBuilder(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " filterContext.HttpContext.Request.Url.Host:" + Request.Url.Host));
            //        //                        FileManage.WriteText(new System.Text.StringBuilder("filterContext.HttpContext.Request.ServerVariables['HTTP_HOST']:" + filterContext.HttpContext.Request.ServerVariables["HTTP_HOST"]));
            //        Response.End();
            //        return;
            //    }
            //    if (entId > 0)
            //    {
            //        Common.CallContextHelper.SetAutoTag(entId);
            //        Session["YSWL_Auto_EnterpriseID"] = entId;
            //    }
            //}
            ////Shop域名
            //if (enterpriseId == 0 && MvcApplication.ProductInfo.Contains("Mall"))
            //{
            //    string host = string.Empty;
            //    host = Request.Url.Host.ToLower();
            //    //启用个性域名访问
            //    int entId = DBUtility.SAASInfo.GetSAASEntIdByMallDomain(host);
            //    if (entId < 0)
            //    {
            //        //域名未登记, 返回404
            //        Response.StatusCode = 404;
            //        FileManage.WriteText(new System.Text.StringBuilder(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " filterContext.HttpContext.Request.Url.Host:" + Request.Url.Host));
            //        //                        FileManage.WriteText(new System.Text.StringBuilder("filterContext.HttpContext.Request.ServerVariables['HTTP_HOST']:" + filterContext.HttpContext.Request.ServerVariables["HTTP_HOST"]));
            //        Response.End();
            //        return;
            //    }
            //    if (entId > 0)
            //    {
            //        Common.CallContextHelper.SetAutoTag(entId);
            //        Session["YSWL_Auto_EnterpriseID"] = entId;
            //    }
            //}
            //#endregion
            #endregion
        }

        #endregion

        #region Application_BeginRequest

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            // Attempt to peform first request initialization
            //HttpApplication app = (HttpApplication)sender;
            //HttpContext context = app.Context;
            //FirstRequestInitialization.Initialize(context);
            try
            {
                if (Request.Cookies["language"] != null)
                {
                    string lang = Request.Cookies["language"].Value.ToString();
                    System.Threading.Thread.CurrentThread.CurrentCulture =
                        System.Globalization.CultureInfo.CreateSpecificCulture(lang);
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion

        #region OtherApplicationEvent

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            #region

            Exception ex = Server.GetLastError();

            //string LogLastDays = ConfigurationManager.AppSettings.Get("LogLastDays");
            string errmsg = "";
            string StackTrace = "";
            if (ex.InnerException != null)
            {
                errmsg = ex.InnerException.Message;
                StackTrace = ex.InnerException.StackTrace;
            }
            else
            {
                errmsg = ex.Message;
                StackTrace = ex.StackTrace;
            }

            //if (LogInFile)
            //{
            //string filename = Server.MapPath("~/log/ErrorMessage.txt");
            //string strTime = DateTime.Now.ToString();
            //StreamWriter sw = new StreamWriter(filename, true);
            //sw.WriteLine(strTime + "：" + errmsg);
            //sw.WriteLine(StackTrace);
            //sw.WriteLine("");
            //sw.Close();
            Log.LogHelper.AddErrorLog("Application_Error:" + errmsg, " 详细错误：" + StackTrace);
            // }
            //if (LogInDB)
            //{
            //    YSWL.BLL.SysManage.ErrorLog.Add(new YSWL.Model.SysManage.ErrorLog("", errmsg, StackTrace));

            //    //Assistant.DelOverdueLog(LogLastDays);
            //}
            //Server.Transfer("ErrorMsg.aspx");
            Server.ClearError();

            #endregion
        }

        protected void Session_End(object sender, EventArgs e)
        {
            try
            {
                string seesionid = Session.SessionID;
                DataTable userTable = (DataTable)Application["OnlineUsers"];
                if (userTable == null)
                    return;
                foreach (DataRow row in userTable.Select("SessionID='" + seesionid + "'"))
                {
                    userTable.Rows.Remove(row);
                }
                userTable.AcceptChanges();
                Application.Lock();
                Application["OnlineUsers"] = userTable;
                Application.UnLock();
            }
            catch
            {
            }

        }

        #region Application_End

        protected void Application_End(object sender, EventArgs e)
        {
            YSWL.DBUtility.DBFactory.Disposable();
        }

        //protected void RecordEndReason()
        //{
        //    HttpRuntime runtime = (HttpRuntime)typeof(System.Web.HttpRuntime).InvokeMember("_theRuntime",
        //        BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField,
        //        null,
        //        null,
        //        null);

        //    if (runtime == null)
        //        return;

        //    string shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage",
        //        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField,
        //        null,
        //        runtime,
        //        null);

        //    string shutDownStack = (string)runtime.GetType().InvokeMember(
        //        "_shutDownStack",
        //        BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField,
        //        null,
        //        runtime,
        //        null);

        //    YSWL.BLL.SysManage.ErrorLog.Add(new YSWL.Model.SysManage.ErrorLog("", String.Format("ShutDownMessage:[{0}]", shutDownMessage), shutDownStack));
        //}

        #endregion

        #endregion

    }
}

namespace YSWL.Web
{
    /// <summary>
    /// 路由区域枚举
    /// </summary>
    public enum AreaRoute
    {
        /// <summary>
        /// 默认主区域(根目录的Controller和View)
        /// </summary>
        None,
        /// <summary>
        /// CMS区域
        /// </summary>
        CMS,
        /// <summary>
        /// Shop区域
        /// </summary>
        Shop,
        /// <summary>
        /// SNS区域
        /// </summary>
        SNS,
        /// <summary>
        /// 移动平台社区区域
        /// </summary>
        MSNS,
        /// <summary>
        /// 用户中心区域 (预留)
        /// </summary>
        UserCenter,
        /// <summary>
        /// 淘宝客区域
        /// </summary>
        Tao,
        /// <summary>
        /// 共用模块区域
        /// </summary>
        COM,
        /// <summary>
        /// 移动平台商城区域
        /// </summary>
        MShop,
        /// <summary>
        /// 移动订货商城区域
        /// </summary>
        MBShop,
        /// <summary>
        /// 移动平台官网区域
        /// </summary>
        MPage,
        /// <summary>
        /// 移动商家区域
        /// </summary>
        MobileSP,
        /// <summary>
        /// 移动代理商区域
        /// </summary>
        MobileAG,
        /// <summary>
        /// ERP系统
        /// </summary>
        ERP,
        /// <summary>
        /// 商家后台区域
        /// </summary>
        Supplier,
        /// <summary>
        /// 代理商后台区域
        /// </summary>
        Agent,
        /// <summary>
        /// 商家微官网区域
        /// </summary>
        MPageSP,
        /// <summary>
        /// 业务员区域
        /// </summary>
        Sales,
        /// <summary>
        /// 加盟商区域
        /// </summary>
        Ship,
        /// <summary>
        /// 管理员区域
        /// </summary>
        Admin,
        /// <summary>
        /// SAAS区域
        /// </summary>
        SAAS,
        /// <summary>
        /// MSAAS区域
        /// </summary>
        MSAAS,
        /// <summary>
        /// 管理员手机区域
        /// </summary>
        MAdmin,
        /// <summary>
        ///订货B端区域
        /// </summary>
        MB,
        /// <summary>
        ///PC端订货B端区域
        /// </summary>
        BShop
    }
}