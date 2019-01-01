using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColoPay.Web.Admin.Pay
{
    public partial class OrderList : PageBaseAdmin
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
            int enterpriseID = YSWL.Common.Globals.SafeInt(this.ddlEnterprise.SelectedValue,0);
            

            if (enterpriseID>0)
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
                strWhere.AppendFormat("PaymentStatus={0}", YSWL.Common.Globals.SafeInt(status,0));
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

        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (gridView.DataKeys[i].Value != null)
                    {
                        //idlist += gridView.Rows[i].Cells[1].Text + ",";
                        idlist += gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
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