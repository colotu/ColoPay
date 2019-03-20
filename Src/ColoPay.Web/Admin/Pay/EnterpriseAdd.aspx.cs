using System;
using YSWL.Accounts.Bus;
using ColoPay.BLL.Members;
using ColoPay.Model.Members;
using System.Web;
using System.Drawing;

namespace ColoPay.Web.Admin.Pay
{
    public partial class EnterpriseAdd : PageBaseAdmin
    {
        public string adminname = "Management";
        private UserType userTypeManage = new UserType();

        
        Model.Members.Users userModel = new Model.Members.Users();

        ColoPay.BLL.Pay.Enterprise EnterpriseBll = new BLL.Pay.Enterprise();
        ColoPay.Model.Pay.Enterprise EnterPriseModel = new Model.Pay.Enterprise();

        BLL.Members.Users userbll = new BLL.Members.Users();
        //protected override int Act_PageLoad { get { return 196; } } //系统管理_是否显示用户管理 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Request.Params["Enterpid"] != null) && (Request.Params["Enterpid"].ToString() != ""))
                {
                    txtUserName.ReadOnly = true;
                    txtUserName.BackColor = Color.AntiqueWhite;
                    lbEnterPid.Text = Request.Params["Enterpid"].Trim();
                    if (lbEnterPid.Text != "")
                    {
                        lbTitle.Text = "编辑商户信息";
                        ShowInfo(lbEnterPid.Text);
                    }
                }
            }
        }

        public void ShowInfo(string strEnerpid)
        {
            EnterPriseModel = EnterpriseBll.GetModel(int.Parse(strEnerpid));

            txtUserName.Text = EnterPriseModel.UserName;
            txtName.Text = EnterPriseModel.Name;
            txtSimpleName.Text = EnterPriseModel.SimpleName;
            ddlStatus.SelectedValue = EnterPriseModel.Status.ToString();
            txtEnterpriseNum.Text = EnterPriseModel.EnterpriseNum;
            txtBusinessLicense.Text = EnterPriseModel.BusinessLicense;
            txtTelPhone.Text = EnterPriseModel.TelPhone;
            txtCellPhone.Text = EnterPriseModel.CellPhone;
            txtAccountBank.Text = EnterPriseModel.AccountBank;
            txtAccountInfo.Text = EnterPriseModel.AccountInfo;
            txtAccountNum.Text = EnterPriseModel.AccountNum;
            txtWithdrawBank.Text = EnterPriseModel.WithdrawBank;
            txtWithdrawInfo.Text = EnterPriseModel.WithdrawInfo;
            txtWithdrawNum.Text = EnterPriseModel.WithdrawNum;
            txtBalance.Text = EnterPriseModel.Balance.ToString();
            txtAppId.Text = EnterPriseModel.AppId;
            txtAppSecrit.Text = EnterPriseModel.AppSecrit;
            txtAppUrl.Text = EnterPriseModel.AppUrl;
            txtAppReturnUrl.Text = EnterPriseModel.AppReturnUrl;
            txtContactMail.Text = EnterPriseModel.ContactMail;
            txtAddress.Text = EnterPriseModel.Address;
            ddlEnteRank.SelectedValue = EnterPriseModel.EnteRank.ToString();

            txtRemark.Text = EnterPriseModel.Remark;

        }

        public void btnSave_Click(object sender, System.EventArgs e)
        {
            if (lbEnterPid.Text.Trim() != "")
            {
                EnterPriseModel = EnterpriseBll.GetModel(int.Parse(lbEnterPid.Text));
                userModel = userbll.GetModel(EnterPriseModel.UserName);
            }

            User newUser = new User();
#pragma warning disable CS0219 // 变量“strErr”已被赋值，但从未使用过它的值
            string strErr = "";
#pragma warning restore CS0219 // 变量“strErr”已被赋值，但从未使用过它的值
            //if (newUser.HasUserByUserName(txtUserName.Text))
            //{
            //    strErr += Resources.Site.TooltipUserExist;
            //}
            //if (strErr != "")
            //{
            //    YSWL.Common.MessageBox.ShowSuccessTip(this, strErr);
            //    return;
            //}
            newUser.UserName = txtUserName.Text;
            newUser.Password = AccountsPrincipal.EncryptPassword("111111");
            newUser.NickName = newUser.UserName;    //昵称和用户名相同 SNS模块使用
            newUser.TrueName = txtName.Text;
            newUser.Sex = "1";
           

            newUser.EmployeeID = 0;
            newUser.Activity = true;
            newUser.UserType = "EE";
            newUser.Style = 1;
            newUser.User_dateCreate = DateTime.Now;
            newUser.User_iCreator = CurrentUser.UserID;
            newUser.User_dateValid = DateTime.Now;
            newUser.User_cLang = "zh-CN";

 
            EnterPriseModel.UserName = txtUserName.Text;
            EnterPriseModel.Name = txtName.Text;
            EnterPriseModel.SimpleName = txtSimpleName.Text;
            EnterPriseModel.Status = int.Parse(ddlStatus.SelectedValue);
            EnterPriseModel.EnterpriseNum = txtEnterpriseNum.Text;
            EnterPriseModel.BusinessLicense = txtBusinessLicense.Text;
            EnterPriseModel.TelPhone = txtTelPhone.Text;
            EnterPriseModel.CellPhone = txtCellPhone.Text;
            EnterPriseModel.AccountBank = txtAccountBank.Text;
            EnterPriseModel.AccountInfo = txtAccountInfo.Text;
            EnterPriseModel.AccountNum = txtAccountNum.Text;
            EnterPriseModel.WithdrawBank = txtWithdrawBank.Text;
            EnterPriseModel.WithdrawInfo = txtWithdrawInfo.Text;
            EnterPriseModel.WithdrawNum = txtWithdrawNum.Text;
            EnterPriseModel.Balance = decimal.Parse(txtBalance.Text.Trim());
            EnterPriseModel.AppId = txtAppId.Text;
            EnterPriseModel.AppSecrit = txtAppSecrit.Text;
            EnterPriseModel.AppUrl = txtAppUrl.Text;
            EnterPriseModel.AppReturnUrl = txtAppReturnUrl.Text;
            EnterPriseModel.ContactMail = txtContactMail.Text;
            EnterPriseModel.Address = txtAddress.Text;
            EnterPriseModel.EnteRank = int.Parse(ddlEnteRank.SelectedValue);
            EnterPriseModel.CreatedDate = DateTime.Now;
            EnterPriseModel.CreatedUserID = CurrentUser.UserID;

            EnterPriseModel.RegisterIp = "";
            EnterPriseModel.Remark = txtRemark.Text;

            if (lbEnterPid.Text == "")
            {
                if (EnterpriseBll.ExistsUsername(txtUserName.Text))
                {
                    lblMsg.Text = "用户名已存在，请重新输入！";
                    return;
                }

                if (EnterpriseBll.ExistsName(txtName.Text))
                {
                    lblMsg.Text = "商户名称已存在，请重新输入！";
                    return;
                }
                else
                {
                    int userid = newUser.Create();
                    if (userid == -100)
                    {
                        //ERROR
                        YSWL.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUserExist);
                        return;
                    }

                    //新增商户成功
                    if (EnterpriseBll.Add(EnterPriseModel) > 0)
                    {
                        YSWL.Common.MessageBox.ShowSuccessTip(this, string.Format("新增商户：【{0}】成功！", txtUserName.Text));
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("新增商户：【{0}】", txtUserName.Text), this);
                        Response.Redirect("EnterpriseList.aspx");
                    }
                }
            }
            else
            {
                

                if (EnterpriseBll.ExistsUsername(lbEnterPid.Text, txtUserName.Text))
                {
                    lblMsg.Text = "用户名已存在，请重新输入！";
                }
                if (EnterpriseBll.ExistsName(lbEnterPid.Text, txtName.Text))
                {
                    lblMsg.Text = "商户名称已存在，请重新输入！";
                    return;
                }
                else
                {
                    //userModel.UserName = txtUserName.Text;
                    EnterpriseBll.Update(EnterPriseModel);//修改企业信息
                    YSWL.Common.MessageBox.ShowSuccessTip(this, string.Format("修改商户信息：【{0}】成功！", txtUserName.Text));
                    Response.Redirect("EnterpriseList.aspx");
                }
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("EnterpriseList.aspx");
        }
        
    }
}
