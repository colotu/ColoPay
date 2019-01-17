using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YSWL.Accounts.Bus;

namespace ColoPay.Web.Enterprise
{
    public partial class PayInfo : PageBaseEnterprise
    {
        ColoPay.BLL.Pay.Enterprise EnterpriseBll = new BLL.Pay.Enterprise();
        ColoPay.Model.Pay.Enterprise EnterPriseModel = new Model.Pay.Enterprise();


        ColoPay.Model.Pay.PaymentTypes PaytypesModel = new Model.Pay.PaymentTypes();
        ColoPay.BLL.Pay.PaymentTypes PaytypesBll = new BLL.Pay.PaymentTypes();

        ColoPay.BLL.Pay.EnterprisePayFee PayFreeBll = new BLL.Pay.EnterprisePayFee();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lbEnterPid.Text = EnterpriseBll.GetEnterpriseID(currentUser.UserName).ToString();
                ShowInfo(lbEnterPid.Text);
            }
        }

        public void ShowInfo(string strEnerpid)
        {
            EnterPriseModel = EnterpriseBll.GetModel(int.Parse(strEnerpid));
                      
            txtAppId.Text = EnterPriseModel.AppId;
            txtAppSecrit.Text = EnterPriseModel.AppSecrit;
            txtAppUrl.Text = EnterPriseModel.AppUrl;
            txtAppReturnUrl.Text = EnterPriseModel.AppReturnUrl;
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