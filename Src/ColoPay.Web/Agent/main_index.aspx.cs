using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColoPay.Web.Agent
{
    public partial class main_index : PageBaseAgent
    {
        public string adminname = "Management";

        Model.Members.Users userModel = new Model.Members.Users();

        ColoPay.BLL.Pay.Agent AgentBll = new BLL.Pay.Agent();
        ColoPay.Model.Pay.Agent AgentModel = new Model.Pay.Agent();

        BLL.Members.Users userbll = new BLL.Members.Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //设置商户ID，显示商户信息
                //currentUser.UserName
                lbAgentId.Text = AgentBll.GetAgentID(currentUser.UserName).ToString();
                ShowInfo(lbAgentId.Text);
            }
        }

        public void ShowInfo(string strEnerpid)
        {
            AgentModel = AgentBll.GetModel(int.Parse(strEnerpid));

            txtUsername.Text = AgentModel.UserName;
            txtName.Text = AgentModel.Name;
            if (AgentModel.ParentId != 0 && AgentModel.ParentId.ToString().Length > 0)
            {
                txtPraent.Text = AgentBll.GetModel(AgentModel.ParentId).UserName;
            }
            else
            {
                txtPraent.Text = "";
            }
            //txtPraent.Text= AgentBll. AgentModel.ParentId

            txtBusinessLicense.Text = AgentModel.BusinessLicense;
            txtCellPhone.Text = AgentModel.CellPhone;
            //txtAccountBank.Text = AgentModel.AccountBank;
            txtAccountInfo.Text = AgentModel.AccountInfo;
            txtAccountNum.Text = AgentModel.AccountNum;
            //txtWithdrawBank.Text = AgentModel.WithdrawBank;
            txtWithdrawInfo.Text = AgentModel.WithdrawInfo;
            txtWithdrawNum.Text = AgentModel.WithdrawNum;
            txtBalance.Text = AgentModel.Balance.ToString();
        }

        
    }
}