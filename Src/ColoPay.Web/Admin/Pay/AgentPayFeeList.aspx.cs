using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YSWL.Accounts.Bus;

namespace ColoPay.Web.Admin.Pay
{
    public partial class AgentPayFeeList : PageBaseAdmin
    {
        ColoPay.BLL.Pay.Agent AgentBll = new BLL.Pay.Agent();
        ColoPay.Model.Pay.Agent AgentModel = new Model.Pay.Agent();


        ColoPay.Model.Pay.PaymentTypes PaytypesModel = new Model.Pay.PaymentTypes();
        ColoPay.BLL.Pay.PaymentTypes PaytypesBll = new BLL.Pay.PaymentTypes();

        ColoPay.BLL.Pay.AgentPayFee AgentPayFreeBll = new BLL.Pay.AgentPayFee();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if ((Request.Params["AgentId"] != null) && (Request.Params["AgentId"].ToString() != ""))
                {
                    lbAgentId.Text = Request.Params["AgentId"].Trim();
                    if (lbAgentId.Text != "")
                    {
                        ShowInfo(lbAgentId.Text);
                    }
                }
            }
        }

        public void ShowInfo(string strEnerpid)
        {
            AgentModel = AgentBll.GetModel(int.Parse(strEnerpid));

            lbEnterName.Text = AgentModel.Name;

        }


        #region gridView

        public void BindData()
        {
            DataSet ds = new DataSet();

            ds = PaytypesBll.GetList(" ModeId>0");

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

                string strPayModedId = gridView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                if (strPayModedId.Length > 0 && strPayModedId != "0")
                {
                    Model.Pay.AgentPayFee payfeeModel = AgentPayFreeBll.GetModel(int.Parse(lbAgentId.Text), int.Parse(strPayModedId));
                    if (payfeeModel != null)
                    {
                        e.Row.Cells[2].Text = payfeeModel.FeeRate.ToString();
                        e.Row.Cells[3].Text = "<a href=\"AgentPayFeeEdit.aspx?PayModelid=" + strPayModedId + "&AgentId=" + lbAgentId.Text + "&typeid=1\">设置费率</ a > ";
                    }
                    else
                    {
                        e.Row.Cells[2].Text = "<a href=\"AgentPayFeeEdit.aspx?PayModelid=" + strPayModedId + "&AgentId=" + lbAgentId.Text + "&typeid=0\">设置费率</ a > ";
                        e.Row.Cells[3].Text = "";
                    }
                }

            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = gridView.DataKeys[e.RowIndex].Value.ToString();
            try
            {
                AgentPayFreeBll.Delete(int.Parse(lbAgentId.Text), int.Parse(ID));

                YSWL.Common.MessageBox.ShowSuccessTip(this, "成功关闭通道！");
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

        /// <summary>
        /// 返回代理商列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgentList.aspx");
        }

        #endregion gridView
    }
}