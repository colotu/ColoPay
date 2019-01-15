using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColoPay.Web.Agent.Pay
{
    public partial class OrderList : PageBaseAgent
    {
        private ColoPay.BLL.Pay.Order orderBll = new BLL.Pay.Order();
        private ColoPay.BLL.Pay.Enterprise enterpriseBll = new BLL.Pay.Enterprise();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindEnterprise();
            }
        }

        private void BindEnterprise()
        {
            this.ddlEnterprise.Items.Clear();
            this.ddlEnterprise.Items.Add(new ListItem(Resources.Site.All, ""));
            int agentId = CurrAgentID;
            List<ColoPay.Model.Pay.Enterprise> modelList = new List<ColoPay.Model.Pay.Enterprise>();

            modelList = enterpriseBll.GetModelList(" agentId=" + agentId);

            if (modelList != null && modelList.Count > 0)
            {

                foreach (var item in modelList)
                {

                    this.ddlEnterprise.Items.Add(new ListItem(item.Name, item.EnterpriseID.ToString()));

                }
            }
            this.ddlEnterprise.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }
         

        #region gridView

        public void BindData()
        {
            StringBuilder strWhere = new StringBuilder();
            int enterpriseID = currentUser.EnterpriseId;


            string startStr = this.txtDateStart.Text;
            string endStr = this.txtDateEnd.Text;

            strWhere.AppendFormat(" Agentd={0}", CurrAgentID);

            if (enterpriseID > 0)
            {
                strWhere.AppendFormat(" and  EnterpriseID={0}", enterpriseID);
            }
            string status = ddlStatus.SelectedValue;
            if (!string.IsNullOrWhiteSpace(status))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("PaymentStatus={0}", YSWL.Common.Globals.SafeInt(status, 0));
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

                strWhere.AppendFormat("( OrderCode like '%{0}%' or EnterOrder like '%{0}%' or OrderInfo like '%{0}%')", keyWord);
            }

            gridView.DataSetSource = orderBll.GetList(0, strWhere.ToString(), "CreatedTime desc");
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
    }
}