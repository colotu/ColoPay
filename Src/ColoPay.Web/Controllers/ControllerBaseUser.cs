﻿/**
* ControllerBaseUser.cs
*
* 功 能： 需要权限验证的页面基类：如用户中心
* 类 名： ControllerBaseUser
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/7/23 14:50:11   Ben    初版
*
* Copyright (c) 2012 YS56 Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：云商未来（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System.Web;
using System.Web.Mvc;
using YSWL.Accounts.Bus;
using YSWL.Common;
using YSWL.Log;

namespace ColoPay.Web.Controllers
{
    /// <summary>
    /// 需要权限验证的页面基类：如用户中心（需要权限验证和用户登录才能访问）
    /// </summary>
    public abstract class ControllerBaseUser : ControllerBase
    {
        #region 权限控制
        //子类通过 new 重写该值
        //protected int Act_DeleteList = 1; //批量删除按钮       
        protected int permissionid = -1;

        /// <summary>
        /// 页面访问需要的权限。可以在不同页面继承里来控制不同页面的权限。默认-1为无限制。
        /// </summary>
        public int PermissionID
        {
            set
            {
                permissionid = value;
            }
            get
            {
                return permissionid;
            }
        }

        #endregion

        #region 初始化

        protected override bool InitializeComponent(ActionExecutingContext filterContext)
        {

            if (MvcApplication.IsAutoConn)//如果是SAAS 自动化链接
            {
                long enterpriseId = YSWL.Common.Globals.SafeLong(YSWL.Common.CallContextHelper.GetClearTag(), 0);
                if (enterpriseId <= 0)
                {
                    string returnUrl = YSWL.Common.ConfigHelper.GetConfigString("SAASLoginUrl");
                    filterContext.Result= Redirect(returnUrl);
                    return false;
                }
            }
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = RedirectToLogin(filterContext);
                return false;
            }
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
                filterContext.Result = RedirectToLogin(filterContext);
                return false;
            }
            if (Session[Globals.SESSIONKEY_USER] == null)
            {
                currentUser = new YSWL.Accounts.Bus.User(userPrincipal);
                Session[Globals.SESSIONKEY_USER] = currentUser;
                Session["Style"] = currentUser.Style;
            }
            else
            {
                currentUser = (YSWL.Accounts.Bus.User)Session[Globals.SESSIONKEY_USER];
                Session["Style"] = currentUser.Style;
            }

            if ( CurrentUser == null ||  CurrentUser.UserType == "AA")
            {
                filterContext.Result = RedirectToLogin(filterContext);
                return false;
            }

            //追加权限验证
            ValidatingPermission();
            return true;
        }

        public abstract ActionResult RedirectToLogin(ActionExecutingContext filterContext);

        private void ValidatingPermission()
        {
            if (HttpContext.User.Identity.IsAuthenticated && userPrincipal != null)
            {
                if ((PermissionID != -1) && (!userPrincipal.HasPermissionID(PermissionID)))
                {
                    Response.Clear();
                    Response.Write("<script defer>window.alert('" + Resources.Site.TooltipNoPermission + "');history.back();</script>");
                    Response.End();
                }
            }
            else
            {
                Response.Clear();
                Response.Write("<script defer>window.alert('" + Resources.Site.TooltipNoAuthenticated + "');parent.location='" + DefaultLogin + "';</script>");
                Response.End();
            }
        }
        #endregion
    }
}
