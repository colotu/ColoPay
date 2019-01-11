using System;
using YSWL.Accounts.Bus;
using ColoPay.BLL.Members;
using ColoPay.Model.Members;
using System.Web;

namespace ColoPay.Web.Admin.Pay
{
    public partial class PayFeeEdit : PageBaseAdmin
    {
        public string adminname = "Management";
        private UserType userTypeManage = new UserType();


        BLL.Members.Users userbll = new BLL.Members.Users();
        Model.Members.Users userModel = new Model.Members.Users();

        ColoPay.BLL.Pay.Enterprise EnterpriseBll = new BLL.Pay.Enterprise();
        ColoPay.Model.Pay.Enterprise EnterPriseModel = new Model.Pay.Enterprise();


        ColoPay.Model.Pay.PaymentTypes PaytypesModel = new Model.Pay.PaymentTypes();
        ColoPay.BLL.Pay.PaymentTypes PaytypesBll = new BLL.Pay.PaymentTypes();

        Model.Pay.EnterprisePayFee PayFreeModel = new Model.Pay.EnterprisePayFee();
        ColoPay.BLL.Pay.EnterprisePayFee PayFreeBll = new BLL.Pay.EnterprisePayFee();

        //protected override int Act_PageLoad { get { return 196; } } //系统管理_是否显示用户管理 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Request.Params["PayModelid"] != null) && (Request.Params["PayModelid"].ToString() != ""))
                {
                    lbPayModelid.Text = Request.Params["PayModelid"].Trim();
                    if (lbPayModelid.Text != "")
                    {
                        ShowInfoPayType(lbPayModelid.Text);
                    }
                }
                if ((Request.Params["Enterpid"] != null) && (Request.Params["Enterpid"].ToString() != ""))
                {
                    lbEnterPid.Text = Request.Params["Enterpid"].Trim();
                    if (lbEnterPid.Text != "")
                    {
                        ShowInfoEnter(lbEnterPid.Text);
                    }
                }
                //操作类型，新增通道 typeid是0，编辑通道费率 typeid是1，
                if ((Request.Params["typeid"] != null) && (Request.Params["typeid"].ToString() != ""))
                {
                    lbType.Text = Request.Params["typeid"].Trim();
                    if (lbType.Text == "1")
                    {
                        Model.Pay.EnterprisePayFee payfeeModel = PayFreeBll.GetModel(int.Parse(lbEnterPid.Text), int.Parse(lbPayModelid.Text));
                        txtPayFree.Text = payfeeModel.FeeRate.ToString();
                    }
                }

            }
        }

        public void ShowInfoEnter(string strEnerpid)
        {
            EnterPriseModel = EnterpriseBll.GetModel(int.Parse(strEnerpid));
            lbEnterName.Text = EnterPriseModel.UserName; 
        }

        public void ShowInfoPayType (string strPayTypeid)
        {
            PaytypesModel = PaytypesBll.GetModel(int.Parse(strPayTypeid));
            lbPayTypeName.Text = PaytypesModel.Name;
        }


        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnSave_Click(object sender, System.EventArgs e)
        {
            if (txtPayFree.Text.Trim() == "")
            {
                YSWL.Common.MessageBox.ShowSuccessTip(this, string.Format("设置费率{0}！",""));
                return;
            }

            PayFreeModel.EnterpriseID = int.Parse(lbEnterPid.Text);
            PayFreeModel.PayModeId = int.Parse(lbPayModelid.Text);
            PayFreeModel.FeeRate = decimal.Parse(txtPayFree.Text);



            if (lbType.Text == "0")
            {
                //开通通道
                if (PayFreeBll.Add(PayFreeModel))
                {
                    YSWL.Common.MessageBox.ShowSuccessTip(this, string.Format("开通通道成功，费率：【{0}】！", txtPayFree.Text));
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format(" 商户" + lbEnterPid.Text + " ,设置费率：【{0}】", txtPayFree.Text), this);
                    Response.Redirect("PayFeeList.aspx?Enterpid=" + lbEnterPid.Text + "");
                }
            }
            else
            {
                if (PayFreeBll.Update(PayFreeModel))                
                {
                    YSWL.Common.MessageBox.ShowSuccessTip(this, string.Format("修改费率：【{0}】成功！", txtPayFree.Text));
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format(" 商户" + lbEnterPid.Text + " ,设置费率：【{0}】", txtPayFree.Text), this);
                    Response.Redirect("PayFeeList.aspx?Enterpid="+lbEnterPid.Text+"");
                }
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("PayFeeList.aspx?Enterpid=" + lbEnterPid.Text + "");
        }
        
    }
}
