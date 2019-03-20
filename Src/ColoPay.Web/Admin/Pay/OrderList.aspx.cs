using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YSWL.Common;

namespace ColoPay.Web.Admin.Pay
{
    public partial class OrderList : PageBaseAdmin
    {
        private ColoPay.BLL.Pay.Order orderBll = new BLL.Pay.Order();
        private ColoPay.BLL.Pay.Enterprise enterpriseBll = new BLL.Pay.Enterprise();
        private ColoPay.BLL.Pay.Agent agentBll = new BLL.Pay.Agent();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindAgent();
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
            int agentId = YSWL.Common.Globals.SafeInt(this.ddlAgent.SelectedValue, 0);
            List<ColoPay.Model.Pay.Enterprise> modelList = new List<ColoPay.Model.Pay.Enterprise>();
            if (agentId > 0)
            {
                modelList = enterpriseBll.GetModelList(" agentId=" + agentId);
            }
            else
            {
                modelList = enterpriseBll.GetModelList("");
            }
            if (modelList != null && modelList.Count > 0)
            {

                foreach (var item in modelList)
                {

                    this.ddlEnterprise.Items.Add(new ListItem(item.Name, item.EnterpriseID.ToString()));

                }
            }
            this.ddlEnterprise.DataBind();

        }

        private void BindAgent()
        {
            this.ddlAgent.Items.Clear();
            this.ddlAgent.Items.Add(new ListItem(Resources.Site.All, ""));
            List<ColoPay.Model.Pay.Agent> modelList = agentBll.GetModelList("");
            if (modelList != null && modelList.Count > 0)
            {
                foreach (var item in modelList)
                {
                    this.ddlAgent.Items.Add(new ListItem(item.Name, item.AgentId.ToString()));
                }
            }
            this.ddlAgent.DataBind();
        }

        #endregion
        public void ddlAgent_Changed(object sender, System.EventArgs e)
        {
            BindEnterprise();
        }

        #region gridView

        public void BindData()
        {
            StringBuilder strWhere = new StringBuilder();
            int agentId = YSWL.Common.Globals.SafeInt(this.ddlAgent.SelectedValue, 0);
            int enterpriseID = YSWL.Common.Globals.SafeInt(this.ddlEnterprise.SelectedValue, 0);

            string startStr = this.txtDateStart.Text;
            string endStr = this.txtDateEnd.Text;

            if (agentId > 0)
            {
                strWhere.AppendFormat(" AgentID={0}", agentId);
            }

            if (enterpriseID>0)
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
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


        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
          
            if (e.CommandName == "Pay")
            {
                string orderCode = e.CommandArgument.ToString();
                ColoPay.Model.Pay.Order orderInfo = orderBll.GetModel(orderCode);
                bool isSuccess = true;
                if (orderInfo.PaymentStatus < 2)
                {
                    isSuccess = orderBll.CompleteOrder(orderInfo);
                } 
                if (isSuccess)//成功之后需要回调商家回调地址
                {
                    try
                    {
                        ColoPay.BLL.Pay.Enterprise.Notify(orderInfo);
                    }
                    catch (Exception ex)
                    {
                        ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单【{0}】支付回调通知失败：{1}", orderInfo.OrderCode, ex.Message), ex.StackTrace); 
                    }
                } 
                MessageBox.ShowSuccessTip(this, "操作成功！");
            }

            if (e.CommandName == "Notify")
            {
                string orderCode = e.CommandArgument.ToString();
                ColoPay.Model.Pay.Order orderInfo = orderBll.GetModel(orderCode);
                bool isSuccess = true;
                string responseStr = "";
                if (orderInfo.PaymentStatus < 2)
                {
                    isSuccess = orderBll.CompleteOrder(orderInfo);
                }
                if (isSuccess)//成功之后需要回调商家回调地址
                {
                    try
                    {
                        responseStr= ColoPay.BLL.Pay.Enterprise.Notify(orderInfo);
                    }
                    catch (Exception ex)
                    {
                        responseStr = ex.Message;
                        ColoPay.BLL.SysManage.LogHelp.AddErrorLog(String.Format("订单【{0}】支付回调通知失败：{1}", orderInfo.OrderCode, ex.Message), ex.StackTrace);
                    }
                }
                if (responseStr == "success")
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    responseStr = "回调通知失败，" + responseStr;
                    MessageBox.ShowFailTip(this, responseStr);
                }
               
            }
            gridView.OnBind();

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

        #region 获取代理商名称
        /// <summary>
        /// 获取代理商名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetAgentName(object target)
        {
            int agentId = YSWL.Common.Globals.SafeInt(target, 0);

            ColoPay.Model.Pay.Agent  Model = agentBll.GetModelByCache(agentId);

            return Model == null ? "平台" : Model.Name;
        }
        #endregion


        #region 获取订单状态
        protected string GetPayStatus(object paytarget,object ordertarget)
        {
            int paystatus = YSWL.Common.Globals.SafeInt(paytarget, 0);
            int orderstatus = YSWL.Common.Globals.SafeInt(ordertarget, 0);
            if (paystatus == 0)
            {
                return "<span  style = 'color:red;' > 未支付 </span >";
            }
            if (paystatus == 2 && orderstatus == 1)
            {
                return "<span  style = 'color:#EE9A00;' > 已支付，通知未成功 </span >";
            }
            if (paystatus == 2 && orderstatus == 2)
            {
                return "<span  style = 'color:green;' > 已支付，通知成功 </span >";
            }
            return "<span  style = 'color:red;' > 未知 </span >";
            //  return enterpriseModel == null ? "未知" : enterpriseModel.Name;
        }
        #endregion
    }
}