using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YSWL.Accounts.Bus;
using YSWL.Common;
using YSWL.Components.Setting;
using ColoPay.Model.SysManage;
using Webdiyer.WebControls.Mvc;

namespace ColoPay.Web.Areas.Supplier.Controllers
{
    public class AccountController : Web.Controllers.ControllerBase
    {
        private readonly BLL.Members.UsersExp userEXBll = new BLL.Members.UsersExp();


        #region 固定当前区域的基础路径
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            #region 固定当前区域的基础路径
            ViewBag.BasePath = "/SP/";
            #endregion
        } 
        #endregion

        #region 商家登录
        public ActionResult Login()
        {
          
            //bool IsCloseLogin = BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_Close_Login");
            //if (IsCloseLogin)
            //{
            //    return RedirectToAction("TurnOff", "Error");
            //}
            if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType == "SP")
            {
                return Redirect(ViewBag.BasePath + "Home/Index");
            }
            
            return View();
        }
        [HttpPost]
        public ActionResult Logout()
        {
            if (Session[Globals.SESSIONKEY_SUPPLIER] != null)
            {
                User currentUser = (User)Session[Globals.SESSIONKEY_SUPPLIER];
                LogHelp.AddUserLog(currentUser.UserName, currentUser.UserType, "退出系统");
                #region 更新最新的登录时间
                ColoPay.BLL.Members.UsersExp uBll = new BLL.Members.UsersExp();
                Model.Members.UsersExpModel uModel = new Model.Members.UsersExpModel();
                uModel = uBll.GetUsersExpModel(currentUser.UserID);
                if (uModel != null)
                {
                    uModel.LastAccessIP = Request.UserHostAddress;
                    uModel.LastLoginTime = DateTime.Now;
                    uBll.Update(uModel);
                }
                #endregion
            }
            FormsAuthentication.SignOut();
            Session.Remove(Globals.SESSIONKEY_SUPPLIER);
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
        #endregion

    }
}
