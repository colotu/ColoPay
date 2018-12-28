﻿#define isFirstProgram
/**
* ControllerBase.cs
*
* 功 能： 页面层(表示层)基类,所有前台页面继承，无权限验证
* 类 名： ControllerBase
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/7/23 14:36:01   Ben    初版
*
* Copyright (c) 2012 YSWL Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：云商未来（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YSWL.Accounts.Bus;
using YSWL.Common;
using YSWL.Components;
using YSWL.SAAS.BLL;

namespace YSWL.Web.Controllers
{
    /// <summary>
    /// 页面层(表示层)基类,所有前台页面继承，无权限验证
    /// </summary>
    public abstract class ControllerBaseAbs : Controller
    {
        protected static IPageBaseOption ControllerBaseOption;
        public ControllerBaseAbs(IPageBaseOption option)
        {
            ControllerBaseOption = option;
            DefaultLogin = ControllerBaseOption.DefaultLogin;
        }

        protected readonly string DefaultLogin;

        protected static Hashtable ActHashtab;

        protected AreaRoute CurrentArea = AreaRoute.None;
        protected string CurrentThemeName = "Default";
        protected string CurrentThemeViewPath = "Default";

        protected bool IncludeProduct = true;
        protected int UserAlbumDetailType = -1;

        #region 用户信息

        protected AccountsPrincipal userPrincipal;
        /// <summary>
        ///  权限角色验证对象
        /// </summary>
        public AccountsPrincipal UserPrincipal
        {
            get
            {
                return userPrincipal;
            }
        }
        protected YSWL.Accounts.Bus.User currentUser;
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public YSWL.Accounts.Bus.User CurrentUser
        {
            get
            {
                return currentUser;
            }
        }
        #endregion
        //子类通过 new 重写该值
        protected int Act_DeleteList = 1; //批量删除按钮

        protected int Act_ShowInvalid = 2; //查看失效数据
        protected int Act_CloseList = 3; //批量关闭按钮
        protected int Act_OpenList = 8; //批量打开按钮
        protected int Act_ApproveList = 4; //批量审核按钮
        protected int Act_SetInvalid = 5; //批量设为无效
        protected int Act_SetValid = 6; //批量设为有效
        protected int Act_AddData = 15;//添加数据

        protected static readonly string P_DATA = YSWL.Common.DEncrypt.Hex16.Decode("0050006F0077006500720065006400200062007900204E915546672A6765");

        #region 初始化
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region 安装检测
            if (!MvcApplication.IsInstall)
            {
                filterContext.Result = Redirect("/Installer/Default.aspx");
                return; //未安装 终止
            }
            #endregion


            string tagStr = Request.Params["tag"];
            #region 设置企业标识

            long enterpriseId = 0;
            if (MvcApplication.IsAutoConn)
            {
                #region 读取APP加密企业ID

                if (enterpriseId == 0)
                {
                    //手机登录时候做处理
                    string userAgent = Request.UserAgent;
                    string enterPriseValue = Request.Headers["YSWL_SAAS_EnterpriseID"];
                    string userValue = Request.Headers["YSWL_SAAS_UserName"];
                    if (!string.IsNullOrEmpty(userAgent) && userAgent.Contains("ys56")
                        && !string.IsNullOrEmpty(enterPriseValue) && !string.IsNullOrEmpty(userValue))
                    {
                        enterpriseId = Common.DEncrypt.DEncrypt.ConvertToNumber(enterPriseValue);
                    }
                }
                #endregion

                //优先处理传值过来的
                if (!string.IsNullOrWhiteSpace(tagStr))
                {
                    enterpriseId = Common.DEncrypt.DEncrypt.ConvertToNumber(tagStr);
                }

                if (enterpriseId == 0)
                {
                    Session.Timeout = 60;
                    enterpriseId = Globals.SafeLong(Session["YSWL_Auto_EnterpriseID"], 0);//保存在session里面
                }

                //Shop域名
                if (enterpriseId == 0 && MvcApplication.ProductInfo.Contains("Mall"))
                {
                    string host = string.Empty;
                    if (filterContext.HttpContext.Request.Url != null)
                    {
                        host = filterContext.HttpContext.Request.Url.Host.ToLower();
                    }
                    //启用个性域名访问
                    int entId = SAASInfo.GetSAASEntIdByMallDomain(host);
                    if (entId > 0) enterpriseId = entId;
                }

                if (enterpriseId == 0)
                {
                    //获取cookie中的企业标识
                    string tag = Common.Cookies.getKeyCookie("YSWL_SAAS_EnterpriseID");
                    if (!String.IsNullOrWhiteSpace(tag) && Common.DEncrypt.DEncrypt.ConvertToNumber(tag) > 0)
                    {
                        enterpriseId = Common.DEncrypt.DEncrypt.ConvertToNumber(tag);
                    }
                }

                if (enterpriseId < 1 && MvcApplication.ProductInfo.Contains("Mall"))
                {
                    //域名未登记, 返回404
                    filterContext.Result = new HttpNotFoundResult();
                    FileManage.WriteText(new System.Text.StringBuilder(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " filterContext.HttpContext.Request.Url.Host:" + filterContext.HttpContext.Request.Url.Host));
                    //                        FileManage.WriteText(new System.Text.StringBuilder("filterContext.HttpContext.Request.ServerVariables['HTTP_HOST']:" + filterContext.HttpContext.Request.ServerVariables["HTTP_HOST"]));
                    return;
                }

                if (enterpriseId > 0)
                {
                    Common.CallContextHelper.SetAutoTag(enterpriseId);
                    Session["YSWL_Auto_EnterpriseID"] = enterpriseId;
                }
                else
                {
                    string returnUrl = Common.ConfigHelper.GetConfigString("SAASLoginUrl");
                    filterContext.Result = Redirect(returnUrl);
                    return; //没有企业信息 跳转到SAAS登陆页面
                }
            }

            #region 个性域名检测
            //SaaS域名
            if (enterpriseId == 0 && MvcApplication.ProductInfo.Contains("SAAS"))
            {
                string host = string.Empty;
                if (filterContext.HttpContext.Request.Url != null)
                {
                    host = filterContext.HttpContext.Request.Url.Host.ToLower();
                }
                //启用个性域名访问
                int entId = SAASInfo.GetSAASEnterpriseIdByDomain(host);
                //FileManage.WriteText(new System.Text.StringBuilder(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + host + " entId:" + entId));
                if (entId < 0)
                {
                    //域名未登记, 返回404
                    filterContext.Result = new HttpNotFoundResult();
                    FileManage.WriteText(new System.Text.StringBuilder(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + " filterContext.HttpContext.Request.Url.Host:" + filterContext.HttpContext.Request.Url.Host));
                    //                        FileManage.WriteText(new System.Text.StringBuilder("filterContext.HttpContext.Request.ServerVariables['HTTP_HOST']:" + filterContext.HttpContext.Request.ServerVariables["HTTP_HOST"]));
                    return;
                }
                if (entId > 0)
                {
                    Common.CallContextHelper.SetAutoTag(entId);
                    Session["YSWL_Auto_EnterpriseID"] = entId;
                }
            }
            #endregion
            #endregion

            #region 动态获取当前区域的基础路径
            ViewBag.CurrentArea = CurrentArea = MvcApplication.GetCurrentAreaRoute(
                filterContext.RouteData.DataTokens["area"]);
            ViewBag.BasePath = MvcApplication.GetCurrentRoutePath(CurrentArea);
            #endregion


            #region 静态站域名
            ViewBag.CurrentArea = CurrentArea = MvcApplication.GetCurrentAreaRoute(
                filterContext.RouteData.DataTokens["area"]);
            ViewBag.BasePath = MvcApplication.GetCurrentRoutePath(CurrentArea);

            ViewBag.StaticHost = MvcApplication.StaticHost;
            #endregion

            #region 获取网站公共设置数据
            CurrentThemeName = MvcApplication.GetCurrentThemeName(CurrentArea);
            //DONE: 更正为动态区域, 模版使用主区域Check是否存在 BEN Modify 2013-05-17
            CurrentThemeName = System.IO.Directory.Exists(
                filterContext.HttpContext.Server.MapPath(
                    "/Areas/" + MvcApplication.MainAreaRoute + "/Themes/" + CurrentThemeName))
                            ? CurrentThemeName
                            : "Default";

            CurrentThemeViewPath = MvcApplication.GetCurrentViewPath(CurrentArea);

            //TODO: 应更正为一个值去处理 TO: 涂 BEN ADD 2013-05-17
            if (CurrentThemeName == "TufenXiang")
            {
                IncludeProduct = false;
                UserAlbumDetailType = 0;
            }
            ViewBag.SiteName = MvcApplication.SiteName;
            #endregion

            ViewBag.CurrentUserId = -1;
#if isFirstProgram
            //加载已登录用户对象和Style数据, 由子类实现
            if (!InitializeComponent(filterContext)) return;
#else
            if(InitializeComponent(requestContext.HttpContext))
            { base.Initialize(requestContext); }
#endif
            //SingleLogin slogin = new SingleLogin();
            //if (slogin.ValidateForceLogin())
            //{
            //    requestContext.HttpContext.Response.Write("<script defer>window.alert('" + Resources.Site.TooltipForceLogin + "');parent.location='" + DefaultLogin + "';</script>");
            //}

            Actions bllAction = new Actions();
            ActHashtab = bllAction.GetHashListByCache();
            if (ActHashtab != null && UserPrincipal != null)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)))
                {
                    ViewBag.DeleteAuthority = true;
                }
            }
            base.OnActionExecuting(filterContext);
        }

#if isFirstProgram
        /// <summary>
        /// 加载已登录用户对象和Style数据, 由子类实现 采用虚方法 子类可选
        /// </summary>
        protected virtual bool InitializeComponent(ActionExecutingContext filterContext)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                try
                {
                    userPrincipal = new AccountsPrincipal(HttpContext.User.Identity.Name);
                }
                catch (System.Security.Principal.IdentityNotMappedException)
                {
                    //用户在DB中不存在 退出
                    System.Web.Security.FormsAuthentication.SignOut();
                    Session.Remove(Globals.SESSIONKEY_USER);
                    Session.Clear();
                    Session.Abandon();
                    return false;
                }
                if (Session[Globals.SESSIONKEY_USER] == null)
                {
                    if (MvcApplication.IsAutoConn)
                    {
                        //用户在DB中不存在 退出
                        System.Web.Security.FormsAuthentication.SignOut();
                        Session.Clear();
                        Session.Abandon();
                        return false;
                    }
                    else
                    {
                        currentUser = new YSWL.Accounts.Bus.User(userPrincipal);
                        Session[Globals.SESSIONKEY_USER] = currentUser;
                        Session["Style"] = currentUser.Style;
                    }
                }
                else
                {
                    currentUser = (YSWL.Accounts.Bus.User)Session[Globals.SESSIONKEY_USER];
                    Session["Style"] = currentUser.Style;
                    ViewBag.UserType = currentUser.UserType;
                }
                ViewBag.CurrentUserId = currentUser.UserID;
            }
            return true;
        }

#else
         /// <summary>
        /// 加载已登录用户对象和Style数据, 由子类实现 采用虚方法 子类可选
        /// </summary>
        public virtual bool InitializeComponent(HttpContextBase context)
        {
            return true;
        }
#endif


        #endregion


        ///// <summary>
        ///// 重定向到当前区域的Action
        ///// </summary>
        ///// <param name="actionName">Action</param>
        ///// <param name="controllerName">Controller</param>
        //protected new RedirectToRouteResult RedirectToAction(string actionName, string controllerName)
        //{
        //    return base.RedirectToAction(actionName, controllerName, new { Area = MvcApplication.GetCurrentRoutePath(ControllerContext)});
        //}

        #region OnResultExecuted 授权检测已禁用
        //protected override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    if (filterContext.IsChildAction)
        //    {
        //        base.OnResultExecuted(filterContext);
        //        return;
        //    }
        //    if (!MvcApplication.IsAuthorize)
        //    {
        //        //TODO:检测页脚授权 BEN ADD 201309232022
        //        string title = filterContext.Controller.ViewBag.Title as string;
        //        if (!title.Contains(P_DATA))
        //            filterContext.Result = RedirectToAction("ERROR");
        //    }
        //    base.OnResultExecuted(filterContext);
        //} 
        #endregion

        #region 子类实现
        protected abstract void ControllerException(ExceptionContext filterContext);

        #endregion

        #region 错误处理
        protected override void OnException(ExceptionContext filterContext)
        {
            ControllerException(filterContext);
        }
        #endregion 错误处理

        #region 公共方法

        /// <summary>
        /// 根据功能行为编号得到所属权限编号
        /// </summary>
        /// <returns></returns>
        public int GetPermidByActID(int ActionID)
        {
            if (ActHashtab == null) return -1;
            object obj = ActHashtab[ActionID.ToString()];
            if (obj != null && obj.ToString().Length > 0)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }
}
