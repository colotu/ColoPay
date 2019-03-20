using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using YSWL.Accounts.Bus;
using YSWL.Common;

namespace ColoPay.Web.Agent
{
    public partial class Login : System.Web.UI.Page
    {
        private string returnUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (String.IsNullOrWhiteSpace(returnUrl))
                {
                    return;
                }
#pragma warning disable CS0219 // 变量“currentUser”已被赋值，但从未使用过它的值
                User currentUser = null;
#pragma warning restore CS0219 // 变量“currentUser”已被赋值，但从未使用过它的值

                Page.Title = MvcApplication.SiteName + "-系统登录";

                if (Session[Globals.SESSIONKEY_ENTERPRISE] != null)
                {
                    Response.Redirect("main.htm");
                }
              
            }
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            if ((Session["PassErrorCountAdmin"] != null) && (Session["PassErrorCountAdmin"].ToString() != ""))
            {
                int PassErroeCount = Convert.ToInt32(Session["PassErrorCountAdmin"]);
                if (PassErroeCount > 3)
                {
                    txtUsername.Enabled = false;
                    txtPass.Enabled = false;
                    btnLogin.Enabled = false;
                    this.lblMsg.Text = "对不起，你已经登录错误三次，系统锁定，请联系管理员！";
                    return;
                }
            }
            if ((Session["CheckCode"] != null) && (Session["CheckCode"].ToString() != ""))
            {
                if (Session["CheckCode"].ToString().ToLower() != this.CheckCode.Value.ToLower())
                {
                    this.lblMsg.Text = "验证码错误!";
                    Session["CheckCode"] = null;
                    return;
                }
                else
                {
                    Session["CheckCode"] = null;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            #region

            string userName = YSWL.Common.PageValidate.InputText(txtUsername.Text.Trim(), 30);
            string Password = YSWL.Common.PageValidate.InputText(txtPass.Text.Trim(), 30);
            AccountsPrincipal userPrincipal = AccountsPrincipal.ValidateLogin(userName, Password);
            if (userPrincipal != null)
            {
                User currentUser = new YSWL.Accounts.Bus.User(userPrincipal);
                if (currentUser.UserType != "EE")
                {
                    this.lblMsg.Text = "您非商家用户，您没有权限登录商家后台系统！";
                    return;
                }
                Context.User = userPrincipal;
                if (((SiteIdentity)User.Identity).TestPassword(Password) == 0)
                {
                    try
                    {
                        this.lblMsg.Text = "密码错误！";
                        LogHelp.AddUserLog(userName, "", lblMsg.Text, this);
                    }
                    catch
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    if (!currentUser.Activity)
                    {
                        YSWL.Common.MessageBox.ShowSuccessTip(this, "对不起，该帐号已被冻结，请联系管理员！");
                        return;
                    }

                    #region 单用户登录模式

                    //单用户登录模式
                    //SingleLogin slogin = new SingleLogin();

                    ////if (slogin.IsLogin(currentUser.UserID))
                    ////{
                    ////    YSWL.Common.MessageBox.ShowSuccessTip(this, "对不起，你的帐号已经登录！");
                    ////    return;
                    ////}
                    //slogin.UserLogin(currentUser.UserID);

                    #endregion 单用户登录模式

                    FormsAuthentication.SetAuthCookie(userName, false);

                    Session[YSWL.Common.Globals.SESSIONKEY_ENTERPRISE] = currentUser;
                    Session["Style"] = currentUser.Style;

                    //log
                    LogHelp.AddUserLog(currentUser.UserName, currentUser.UserType, "登录成功", this);


                    if (Session["returnPage"] != null)
                    {
                        string returnpage = Session["returnPage"].ToString();
                        Session["returnPage"] = null;
                        Response.Redirect(returnpage);
                    }
                    else
                    {
                        Response.Redirect("main.htm");
                    }
                }
            }
            else
            {
                this.lblMsg.Text = "登录失败，请确认用户名或密码是否正确。";
                if ((Session["PassErrorCountAdmin"] != null) && (Session["PassErrorCountAdmin"].ToString() != ""))
                {
                    int PassErroeCount = Convert.ToInt32(Session["PassErrorCountAdmin"]);
                    Session["PassErrorCountAdmin"] = PassErroeCount + 1;
                }
                else
                {
                    Session["PassErrorCountAdmin"] = 1;
                }

                //log
                LogHelp.AddUserLog(userName, "", "登录失败!", this);
            }

            #endregion
        }
    }
}