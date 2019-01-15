using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColoPay.Web.Enterprise.Pay
{
    public partial class WithdrawList : PageBaseEnterprise
    {
        private ColoPay.BLL.Pay.Withdraw withdrawBll = new BLL.Pay.Withdraw();
        private ColoPay.BLL.Members.Users userBll = new BLL.Members.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                 
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }



         

        #region gridView

        public void BindData()
        {


            StringBuilder strWhere = new StringBuilder();
            int enterpriseID = CurrEnterpriseID;
            string startStr = this.txtDateStart.Text;
            string endStr = this.txtDateEnd.Text;

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

            if (!String.IsNullOrWhiteSpace(startStr))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" CreatedTime>='{0}'", startStr);
            }

            if (!String.IsNullOrWhiteSpace(endStr))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" CreatedTime<='{0}'", endStr);
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