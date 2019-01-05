﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColoPay.Web.Agent
{
    public partial class Top : PageBaseAgent
    {
        public string username = "";
        public string strMenu = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            username = string.IsNullOrWhiteSpace(CurrentUser.TrueName) ? CurrentUser.UserName : CurrentUser.TrueName;
            if (!IsPostBack)
            {
                ColoPay.BLL.SysManage.SysTree sm = new ColoPay.BLL.SysManage.SysTree();

                //0:admin后台 1:企业后台  2:代理商后台 3:用户后台
                DataSet ds = sm.GetEnabledTreeByParentId(0, 1, true);
                LoadTree(ds.Tables[0]);

            }
            //  hfCurrentID.Value = CurrentUser.UserID.ToString();

            //this.lblTotal.Text = (new ColoPay.BLL.Messages.ReceivedMessages().GetTotal(Convert.ToInt64(CurrentUser.UserID))).ToString();
        }

        public void LoadTree(DataTable dt)
        {
            int n = 1;
            StringBuilder strtemp = new StringBuilder();
            string url = "Left.aspx";
            foreach (DataRow r in dt.Rows)
            {
                string nodeid = r["NodeID"].ToString();
                string text = r["TreeText"].ToString();

                //if (TreeListofLang[nodeid] != null)
                //{
                //    text = TreeListofLang[nodeid].ToString();
                //}
                string parentid = r["ParentID"].ToString();
                string location = r["Location"].ToString();
                if (r["Url"] != null && r["Url"].ToString().Length > 0)
                {
                    url = r["Url"].ToString();
                }

                int permissionid = -1;
                string imageurl = r["ImageUrl"].ToString();
                if (r["PermissionID"] != null)
                {
                    permissionid = int.Parse(r["PermissionID"].ToString().Trim());
                }

                if ((permissionid == -1) || (UserPrincipal.HasPermissionID(permissionid)))
                {
                    strtemp.Append("<li id=\"Tab" + n.ToString() + "\">");
                    strtemp.Append("<a href=\"left2.aspx?id=" + nodeid + "\" target=\"leftFrame\" onclick=\"javascript:switchTab('TabPage1','Tab" + n.ToString() + "');\">" + text);
                    strtemp.Append("</a></li>");
                }
                n++;
            }
            strtemp.AppendFormat("<script language=\"JavaScript\" type=\"text/javascript\">window.top.document.title='{1}{0}';</script>",
                (!MvcApplication.IsAuthorize) ?
                " Powered by 银河支付" : "", (String.IsNullOrWhiteSpace(MvcApplication.SiteName) ? "" : MvcApplication.SiteName));
            strMenu = strtemp.ToString();
        }
    }
}