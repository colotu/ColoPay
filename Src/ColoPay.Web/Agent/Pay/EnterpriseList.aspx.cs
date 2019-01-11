﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YSWL.Accounts.Bus;

namespace ColoPay.Web.Agent.Pay
{
    public partial class EnterpriseList : PageBaseAgent
    {
        ColoPay.BLL.Pay.Agent AgentBll = new BLL.Pay.Agent();
        ColoPay.Model.Pay.Agent AgentModel = new Model.Pay.Agent();

        ColoPay.Model.Pay.Enterprise EnterPriseModel = new Model.Pay.Enterprise();
        ColoPay.BLL.Pay.Enterprise EnterpriseBll = new BLL.Pay.Enterprise();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //设置商户ID，显示商户信息
                //currentUser.UserName
                lbAgentId.Text = AgentBll.GetAgentID(currentUser.UserName).ToString();
                //ShowInfo(lbAgentId.Text);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            DataSet ds = new DataSet();

            StringBuilder strWhere = new StringBuilder();
            string status = ddlStatus.SelectedValue.Trim();
            strWhere.Append(" AgentId="+lbAgentId.Text+" ");

            if (status.Length > 0)
            {
                strWhere.Append(" and Status ='" + status + "' ");
            }

            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.Append(" and Name like '%" + YSWL.Common.InjectionFilter.SqlFilter(txtKeyword.Text) + "%' ");
            }            

            ds = EnterpriseBll.GetList(strWhere.ToString());
            gridView.DataSetSource = ds;
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
        

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = gridView.DataKeys[e.RowIndex].Value.ToString();
           
            try
            {
                EnterpriseBll.Delete(int.Parse(ID));
                YSWL.Common.MessageBox.ShowSuccessTip(this, "删除成功！");
                gridView.OnBind();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 547)
                {
                    YSWL.Common.MessageBox.ShowFailTip(this, Resources.Site.ErrorCannotDeleteUser);
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

        #endregion gridView
    }
}