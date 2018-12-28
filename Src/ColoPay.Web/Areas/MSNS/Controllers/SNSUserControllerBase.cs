using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YSWL.Accounts.Bus;
using YSWL.MALL.Web.Areas.MSNS.Controllers;

namespace YSWL.MALL.Web.Areas.MSNS.Controllers
{
    /// <summary>
    ///  SNS用户中心基类（需要权限验证和用户登录才能访问）
    /// </summary>
    [MSNSError]
    public class SNSUserControllerBase : YSWL.MALL.Web.Controllers.ControllerBaseUser
    {
        #region UserName
        public string UserOpen
        {
            get
            {
                if (Session["WeChat_UserName"] != null)
                {
                    return Session["WeChat_UserName"].ToString();
                }
                return String.Empty;
            }
        }
        #endregion

        #region  OpenId
        public string OpenId
        {
            get
            {
                if (Session["WeChat_OpenId"] != null)
                {
                    return Session["WeChat_OpenId"].ToString();
                }
                return String.Empty;
            }
        }
        #endregion
        /// <summary>
        /// 重写父类的登录跳转, 指向SNS登录
        /// </summary>
        public override ActionResult RedirectToLogin(ActionExecutingContext filterContext)
        {
            string rawurl = Request.RawUrl;
            bool IsAutoLogin = Common.Globals.SafeBool(YSWL.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AutoLogin", -1, "AA"), false);

            #region  自动登陆
            bool IsNeedBind = YSWL.MALL.BLL.SysManage.ConfigSystem.GetBoolValueByCache("SyStem_WeChat_UserBind");

            if (Session[YSWL.Common.Globals.SESSIONKEY_USER] != null && CurrentUser != null && CurrentUser.UserType != "AA")
            {
                BLL.Shop.Products.ShoppingCartHelper.LoadShoppingCart(currentUser.UserID);
                return String.IsNullOrWhiteSpace(rawurl) ? Redirect(ViewBag.BasePath) : Redirect(rawurl);
            }
            YSWL.WeChat.BLL.Core.User wUserBll = new WeChat.BLL.Core.User();
            if (String.IsNullOrWhiteSpace(OpenId) || String.IsNullOrWhiteSpace(UserOpen))
            {
                return Redirect(ViewBag.BasePath + "Account/Login?returnUrl=" + Server.UrlEncode(rawurl));
            }
            YSWL.WeChat.Model.Core.User wUserModel = wUserBll.GetUser(OpenId, UserOpen);
            if (IsNeedBind)
            {
                if (wUserModel.UserId <= 0)
                {
                    return Redirect(ViewBag.BasePath + "Account/Login?returnUrl=" + Server.UrlEncode(rawurl));
                }
                AccountsPrincipal userPrincipal = new AccountsPrincipal(wUserModel.UserId);
                User currentUser = new YSWL.Accounts.Bus.User(userPrincipal);
                if (!currentUser.Activity)
                {
                    return Redirect(ViewBag.BasePath + "Account/Login?returnUrl=" + Server.UrlEncode(rawurl));
                }
                HttpContext.User = userPrincipal;

                #region 自动加入小组
                int groupId = BLL.SysManage.ConfigSystem.GetIntValueByCache("V_SNS_GroupId");
                YSWL.MALL.Model.SNS.GroupUsers groupModel = new Model.SNS.GroupUsers();
                YSWL.MALL.BLL.SNS.GroupUsers bll = new YSWL.MALL.BLL.SNS.GroupUsers();
                if (!bll.Exists(groupId, currentUser.UserID))
                {
                    groupModel.GroupID = groupId;
                    groupModel.JoinTime = DateTime.Now;
                    groupModel.NickName = currentUser.NickName;
                    groupModel.UserID = currentUser.UserID;
                    groupModel.Status = 1;
                    if (!bll.AddEx(groupModel))
                    {
                        ViewBag.joined = "error";
                    }
                }  
                #endregion

                Session[YSWL.Common.Globals.SESSIONKEY_USER] = currentUser;
                FormsAuthentication.SetAuthCookie(currentUser.UserName, true);
                return String.IsNullOrWhiteSpace(rawurl) ? 
                     Redirect(ViewBag.BasePath + "Home") : Redirect(rawurl);
            }
            if (IsAutoLogin)
            {
                string AutoLoginUrl = "/COM/Account/RegBind?returnUrl=" + Server.UrlEncode(rawurl);
                if (wUserModel.UserId <= 0)
                {
                    return Redirect(AutoLoginUrl);
                }
                AccountsPrincipal userPrincipal = new AccountsPrincipal(wUserModel.UserId);
                if (userPrincipal == null)
                {
                    return Redirect(AutoLoginUrl);
                }
                User currentUser = new YSWL.Accounts.Bus.User(userPrincipal);
                if (!currentUser.Activity)
                {
                    return Redirect(AutoLoginUrl);
                }
                HttpContext.User = userPrincipal;
                Session[YSWL.Common.Globals.SESSIONKEY_USER] = currentUser;
                FormsAuthentication.SetAuthCookie(currentUser.UserName, true);
                return String.IsNullOrWhiteSpace(rawurl) ? Redirect(ViewBag.BasePath) : Redirect(rawurl);
            }

            #endregion

            return Redirect(ViewBag.BasePath + "Account/Login?returnUrl=" + Server.UrlEncode(rawurl));
         
        }

      

    }
}
