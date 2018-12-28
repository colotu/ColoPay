using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColoPay.Web.Admin
{
    public partial class main_wc : PageBaseAdmin
    {
        public string CurrentUserName = string.Empty;
        public string GetDateTime = string.Empty;

        private BLL.Members.UsersExp uBll = new BLL.Members.UsersExp();
        private Model.Members.UsersExpModel uModel = new Model.Members.UsersExpModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(CurrentUser.TrueName))
                {
                    CurrentUserName = CurrentUser.TrueName;
                }
                else
                {
                    CurrentUserName = CurrentUser.UserName;
                }
                if (DateTime.Now.Hour > 6 && DateTime.Now.Hour < 12)
                {
                    GetDateTime = "早上好";
                }
                else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 18)
                {
                    GetDateTime = "下午好";
                }
                else
                {
                    GetDateTime = "晚上好";
                }
                uModel = uBll.GetUsersExpModel(CurrentUser.UserID);
                if (uModel != null)
                {
                    this.LitLastLoginTime.Text = uModel.LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    this.LitLastLoginTime.Text = CurrentUser.User_dateCreate.ToString("yyyy-MM-dd HH:mm:ss");
                }

                this.lblUrl.Text = "http://" + YSWL.Common.Globals.DomainFullName + "/wcapi.aspx";

                #region 系统信息
                litProductLine.Text = MvcApplication.ProductInfo + " " + MvcApplication.Version;
                litOperatingSystem.Text = YSWL.Common.SystemInfo.OperatingSystemSimple;
                litServerDomain.Text = YSWL.Common.SystemInfo.ServerDomain;
                litDotNetVersion.Text = YSWL.Common.SystemInfo.DotNetVersion.ToString();
                litWebServerVersion.Text = YSWL.Common.SystemInfo.WebServerVersion;
                #endregion
            }
        }
    }
}