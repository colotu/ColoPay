using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColoPay.Web.Enterprise.Pay
{
    public partial class WithdrawAdd : PageBaseEnterprise
    {
        ColoPay.BLL.Pay.Withdraw withdrawBll = new BLL.Pay.Withdraw();
        ColoPay.BLL.Pay.Enterprise enterpriseBll = new BLL.Pay.Enterprise();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ColoPay.Model.Pay.Enterprise enterpriseModel = enterpriseBll.GetModel(CurrEnterpriseID);
                this.txtAmount.Text = enterpriseModel.Balance.ToString("F");
                this.txtWithdrawBank.Text = enterpriseModel.WithdrawBank;
                this.txtWithdrawInfo.Text = enterpriseModel.WithdrawInfo;
                this.txtWithdrawNum.Text = enterpriseModel.WithdrawNum;

            }
        }

        public void btnSave_Click(object sender, System.EventArgs e)
        {
            ColoPay.Model.Pay.Enterprise enterpriseModel = enterpriseBll.GetModel(CurrEnterpriseID);
            decimal amount = YSWL.Common.Globals.SafeDecimal(this.txtAmount.Text, 0);

            string withdrawBank = this.txtWithdrawBank.Text;
            string withdrawInfo = this.txtWithdrawInfo.Text;
            string withdrawNum = this.txtWithdrawNum.Text;
            string userName = this.txtUserName.Text;

            if (amount > enterpriseModel.Balance)
            {
                lblMsg.Text = "提现金额不能超过余额，请重新输入！";
                return;
            }
            if (String.IsNullOrWhiteSpace(withdrawBank) || String.IsNullOrWhiteSpace(withdrawInfo) || String.IsNullOrWhiteSpace(withdrawNum))
            {
                lblMsg.Text = "提现银行不完善，请检查！";
                return;
            }
            ColoPay.Model.Pay.Withdraw model = new Model.Pay.Withdraw();
            model.AgentId = 0;
            model.WithdrawCode = "TX" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            model.Amount = amount;
            model.CreatedDate = DateTime.Now;
            model.CreatedUserId = currentUser.UserID;
            model.UserName = userName;
            model.EnterpriseID = CurrEnterpriseID;
            model.Type = 0;
            model.WithdrawBank = withdrawBank;
            model.WithdrawInfo = withdrawInfo;
            model.WithdrawNum = withdrawNum;
            model.Remark = txtRemark.Text;
            model.Status = 0;
            bool isSuccess = withdrawBll.AddWithdraw(model);

            //申请成功
            if (isSuccess)
            {
                YSWL.Common.MessageBox.ShowSuccessTip(this, "申请提现成功！");
                Response.Redirect("WithdrawList.aspx");
            }
            else
            {
                YSWL.Common.MessageBox.ShowFailTip(this, "申请提现失败，请稍后再试！");
                return;
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("WithdrawList.aspx");
        }

    }
}