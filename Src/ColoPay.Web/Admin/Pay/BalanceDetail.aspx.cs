using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColoPay.Web.Admin.Pay
{
    public partial class BalanceDetail : PageBaseAdmin
    {
        private ColoPay.BLL.Pay.BalanceDetail detailBll = new BLL.Pay.BalanceDetail();
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
            List<ColoPay.Model.Pay.Enterprise> modelList= new List<ColoPay.Model.Pay.Enterprise>();
            if (agentId > 0)
            {
                modelList = enterpriseBll.GetModelList(" agentId="+ agentId);
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

            if (agentId > 0)
            {
                strWhere.AppendFormat(" AgentID={0}", agentId);
            }

            if (enterpriseID > 0)
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" EnterpriseID={0}", enterpriseID);
            }
            string type = ddlType.SelectedValue;

            if (!string.IsNullOrWhiteSpace(type))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("PayType={0}", YSWL.Common.Globals.SafeInt(type, 0));
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

        #region 获取企业名称
        /// <summary>
        /// 获取企业名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetEnterpriseName(object objEnterprise, object objAgent,object objType)
        {
            int enterpriseId = YSWL.Common.Globals.SafeInt(objEnterprise, 0);
            int agentId = YSWL.Common.Globals.SafeInt(objAgent, 0);
            int type = YSWL.Common.Globals.SafeInt(objType, 0);

            if (type == 0)
            {
                ColoPay.Model.Pay.Enterprise enterpriseModel = enterpriseBll.GetModelByCache(enterpriseId);
                return enterpriseModel == null ? "未知" : enterpriseModel.Name;
            }
            else
            {
                ColoPay.Model.Pay.Agent agentModel = agentBll.GetModelByCache(agentId);
                return agentModel == null ? "未知" : agentModel.Name;
            } 
           
        }
        #endregion
    }
}