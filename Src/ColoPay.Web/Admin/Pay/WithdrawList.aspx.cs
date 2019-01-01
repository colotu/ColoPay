using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColoPay.Web.Admin.Pay
{
    public partial class WithdrawList : PageBaseAdmin
    {
        private ColoPay.BLL.Pay.Withdraw withdrawBll = new BLL.Pay.Withdraw();
        private ColoPay.BLL.Pay.Enterprise enterpriseBll = new BLL.Pay.Enterprise();
        private ColoPay.BLL.Members.Users userBll = new BLL.Members.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindEnterprise();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }



        #region 绑定菜单树
        private void BindEnterprise()
        {
            this.ddlEnterprise.Items.Clear();
            this.ddlEnterprise.Items.Add(new ListItem(Resources.Site.All, ""));


            List<ColoPay.Model.Pay.Enterprise> modelList = enterpriseBll.GetModelList("");
            if (modelList != null && modelList.Count > 0)
            {

                foreach (var item in modelList)
                {

                    this.ddlEnterprise.Items.Add(new ListItem(item.Name, item.EnterpriseID.ToString()));

                }

            }
            this.ddlEnterprise.DataBind();

        }

        #endregion

        #region gridView

        public void BindData()
        {


            StringBuilder strWhere = new StringBuilder();
            int enterpriseID = YSWL.Common.Globals.SafeInt(this.ddlEnterprise.SelectedValue, 0);


            if (enterpriseID > 0)
            {
                strWhere.AppendFormat(" EnterpriseID={0}", enterpriseID);
            }
            string status = ddlStatus.SelectedValue;

            if (!string.IsNullOrWhiteSpace(status))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("Status={0}", YSWL.Common.Globals.SafeInt(status, 0));
            }

            string keyWord = this.txtKeyword.Text;
            if (!string.IsNullOrWhiteSpace(keyWord))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }

                strWhere.AppendFormat("( WithdrawCode like '%{0}%'  )", keyWord);
            }

            gridView.DataSetSource = withdrawBll.GetList(0, strWhere.ToString(), "CreatedDate desc");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
            }
        }

        protected string GetStatus(object target)
        {
            int status = YSWL.Common.Globals.SafeInt(target, 0);

            switch (status)
            {
                case 0:
                    return "未审核";
                case 1:
                    return "已审核";
                case 2:
                    return "已付款";
                default:
                    return "未知";

            }
        }

        protected string GetDateStr(object target)
        {
            DateTime date = YSWL.Common.Globals.SafeDateTime(target, DateTime.MinValue);
            if (date == DateTime.MinValue)
                return "";
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
        

        protected string GetConfirmText(object target)
        {
            int status = YSWL.Common.Globals.SafeInt(target, 0);

            switch (status)
            {
                case 0:
                    return "您确认审核此提现单吗？";
                case 1:
                    return "您确认已经付款了此提现单吗";
                case 2:
                    return "";
                default:
                    return "";

            }
        }

        protected string GetOperateText(object target)
        {
            int status = YSWL.Common.Globals.SafeInt(target, 0);

            switch (status)
            {
                case 0:
                    return "审核";
                case 1:
                    return "付款";
                case 2:
                    return "";
                default:
                    return "";

            }
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Audit")
            {
                if (e.CommandArgument != null)
                {
                    int Id = 0;
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Id = YSWL.Common.Globals.SafeInt(Args[0], 0); 
                    int status = YSWL.Common.Globals.SafeInt(Args[1], 0);

                    withdrawBll.Audit(Id, status, CurrentUser.UserID);
                    gridView.OnBind();
                }
            }
            if (e.CommandName == "Pay")
            {
                if (e.CommandArgument != null)
                {
                    int Id = 0;
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Id = YSWL.Common.Globals.SafeInt(Args[0], 0);
                    int status = YSWL.Common.Globals.SafeInt(Args[1], 0);

                    withdrawBll.Pay(Id, status, CurrentUser.UserID);
                    gridView.OnBind();
                }
            }
        }

        #endregion

        #region 获取商家名称
        /// <summary>
        /// 获取商家名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetEnterpriseName(object target)
        {
            int enterpriseId = YSWL.Common.Globals.SafeInt(target, 0);

            ColoPay.Model.Pay.Enterprise enterpriseModel = enterpriseBll.GetModelByCache(enterpriseId);

            return enterpriseModel == null ? "未知" : enterpriseModel.Name;
        }
        #endregion

        #region 获取用户名称
        /// <summary>
        /// 获取用户名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetUserName(object target)
        {
            int userid = YSWL.Common.Globals.SafeInt(target, 0);

            ColoPay.Model.Members.Users userModel = userBll.GetModelByCache(userid);

            return userModel == null ? "" : userModel.UserName;
        }
        #endregion
    }
}