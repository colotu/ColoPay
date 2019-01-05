using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YSWL.Accounts.Bus;

namespace ColoPay.Web.Agent
{
    public partial class UserPass : PageBaseAgent
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Context.User.Identity.IsAuthenticated) return;
                this.lblName.Text = this.CurrentUser.UserName;
            }
        }


        public void btnSave_Click(object sender, System.EventArgs e)
        {
            if (Page.IsValid)
            {
                SiteIdentity SID = new SiteIdentity(User.Identity.Name);
                if (SID.TestPassword(txtOldPassword.Text) == 0)
                {
                    this.lblMsg.ForeColor = Color.Red;
                    this.lblMsg.Text = Resources.Site.ErrorPasswprdError;
                }
                else
                    if (this.txtPassword.Text.Trim() != this.txtPassword1.Text.Trim())
                {
                    this.lblMsg.ForeColor = Color.Red;
                    this.lblMsg.Text = Resources.Site.ErrorPasswprd;
                }
                else
                {
                    User currentUser = this.CurrentUser;

                    currentUser.Password = AccountsPrincipal.EncryptPassword(txtPassword.Text);

                    if (!currentUser.Update())
                    {
                        YSWL.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
                        //日志
                        //UserLog.AddLog(currentUser.UserName, currentUser.UserType, Request.UserHostAddress, Request.Url.AbsoluteUri,Resources.Site.TooltipUpdateFailPassword);
                    }
                    else
                    {
                        YSWL.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK);
                        //日志
                        //UserLog.AddLog(currentUser.UserName, currentUser.UserType, Request.UserHostAddress, Request.Url.AbsoluteUri,Resources.Site.TooltipUpdateSuccessfulPassword);
                    }
                }
            }
        }
    }
}
