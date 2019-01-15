using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColoPay.Web.Agent.Pay
{
    public partial class MyBalance : PageBaseAgent
    {
        private ColoPay.BLL.Pay.BalanceDetail detailBll = new BLL.Pay.BalanceDetail();
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
            strWhere.AppendFormat(" AgentId={0} and Type=1", CurrAgentID);
            string type = ddlType.SelectedValue;

            string startStr = this.txtDateStart.Text;
            string endStr = this.txtDateEnd.Text;

            if (!string.IsNullOrWhiteSpace(type))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("PayType={0}", YSWL.Common.Globals.SafeInt(type, 0));
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

                strWhere.AppendFormat("( OriginalCode like '%{0}%' )", keyWord);
            }

            gridView.DataSetSource = detailBll.GetList(0, strWhere.ToString(), "CreatedTime desc");
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

        #endregion


    }
}