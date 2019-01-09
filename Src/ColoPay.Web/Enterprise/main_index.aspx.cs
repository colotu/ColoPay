using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ColoPay.Web.Enterprise
{
    public partial class main_index : PageBaseEnterprise
    {
        public string adminname = "Management";

        Model.Members.Users userModel = new Model.Members.Users();

        ColoPay.BLL.Pay.Enterprise EnterpriseBll = new BLL.Pay.Enterprise();
        ColoPay.Model.Pay.Enterprise EnterPriseModel = new Model.Pay.Enterprise();

        BLL.Members.Users userbll = new BLL.Members.Users();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //设置商户ID，显示商户信息
                //currentUser.UserName
                lbEnterPid.Text = EnterpriseBll.GetEnterpriseID(currentUser.UserName).ToString();
                ShowInfo(lbEnterPid.Text);
            }
        }

        public void ShowInfo(string strEnerpid)
        {
            EnterPriseModel = EnterpriseBll.GetModel(int.Parse(strEnerpid));

            txtName.Text = EnterPriseModel.Name;
            txtSimpleName.Text = EnterPriseModel.SimpleName;
            txtEnterpriseNum.Text = EnterPriseModel.EnterpriseNum;
            txtBusinessLicense.Text = EnterPriseModel.BusinessLicense;
            txtCellPhone.Text = EnterPriseModel.CellPhone;
            //txtAccountBank.Text = EnterPriseModel.AccountBank;
            txtAccountInfo.Text = EnterPriseModel.AccountInfo;
            txtAccountNum.Text = EnterPriseModel.AccountNum;
            //txtWithdrawBank.Text = EnterPriseModel.WithdrawBank;
            txtWithdrawInfo.Text = EnterPriseModel.WithdrawInfo;
            txtWithdrawNum.Text = EnterPriseModel.WithdrawNum;
            txtBalance.Text = EnterPriseModel.Balance.ToString();
            txtAppId.Text = EnterPriseModel.AppId;
            txtAppSecrit.Text = EnterPriseModel.AppSecrit;
            txtAppUrl.Text = EnterPriseModel.AppUrl;
            txtAppReturnUrl.Text = EnterPriseModel.AppReturnUrl;
            // txtContactMail.Text = EnterPriseModel.ContactMail;
            // txtAddress.Text = EnterPriseModel.Address;
            ddlEnteRank.SelectedValue = EnterPriseModel.EnteRank.ToString();            
        }

        /// <summary>
        /// 更新应用地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnAppUrl_Click(object sender, EventArgs e)
        {
            string strappUrl = YSWL.Common.InjectionFilter.SqlFilter(txtAppUrl.Text);
            if (strappUrl.Length > 0)
            {
                EnterPriseModel = EnterpriseBll.GetModel(int.Parse(lbEnterPid.Text));
                EnterPriseModel.AppUrl = strappUrl;
                EnterpriseBll.Update(EnterPriseModel);
                YSWL.Common.MessageBox.ShowSuccessTip(this, "应用地址设置成功！");
            }
            else
            {
                YSWL.Common.MessageBox.ShowFailTip(this, "请填写应用地址！");
            }
        }

        /// <summary>
        /// 更新回调应用地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnAppReturnUrl_Click(object sender, EventArgs e)
        {
            string strAppReturnUrl = YSWL.Common.InjectionFilter.SqlFilter(txtAppReturnUrl.Text);

            if (strAppReturnUrl.Length > 0)
            {
                EnterPriseModel = EnterpriseBll.GetModel(int.Parse(lbEnterPid.Text));
                EnterPriseModel.AppReturnUrl = strAppReturnUrl;
                EnterpriseBll.Update(EnterPriseModel);
                YSWL.Common.MessageBox.ShowSuccessTip(this, "回调地址设置成功！");
            }
            else
            {
                YSWL.Common.MessageBox.ShowFailTip(this, "请填写回调地址！");
            }
        }
    }
}