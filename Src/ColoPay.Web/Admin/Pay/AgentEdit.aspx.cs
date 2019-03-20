using System;
using YSWL.Accounts.Bus;
using ColoPay.BLL.Members;
using ColoPay.Model.Members;
using System.Web;
using System.Drawing;

namespace ColoPay.Web.Admin.Pay
{
    public partial class AgentEdit : PageBaseAdmin
    {
        public string adminname = "Management";
        private UserType userTypeManage = new UserType();

        
        Model.Members.Users userModel = new Model.Members.Users();

        ColoPay.BLL.Pay.Agent AgentBll = new BLL.Pay.Agent();
        ColoPay.Model.Pay.Agent AgentModel = new Model.Pay.Agent();

        BLL.Members.Users userbll = new BLL.Members.Users();
        //protected override int Act_PageLoad { get { return 196; } } //系统管理_是否显示用户管理 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if ((Request.Params["AgentId"] != null) && (Request.Params["AgentId"].ToString() != ""))
                {
                    txtUserName.ReadOnly = true;
                    txtUserName.BackColor = Color.AntiqueWhite;
                    lbAgentid.Text = Request.Params["AgentId"].Trim();
                    if (lbAgentid.Text != "")
                    {
                        lbTitle.Text = "编辑代理商信息";
                        ShowInfo(lbAgentid.Text);
                    }
                }
            }
        }

        public void ShowInfo(string strEnerpid)
        {
            AgentModel = AgentBll.GetModel(int.Parse(strEnerpid));

            txtUserName.Text = AgentModel.UserName;
            txtName.Text = AgentModel.Name;

            if (AgentModel.ParentId.ToString().Length > 0 && AgentModel.ParentId != 0)
            {
                txtParentLd.Text = AgentBll.GetModel(AgentModel.ParentId).UserName;// AgentModel.SimpleName;//显示推荐人账号
            }
            else
            {
                txtParentLd.Text = "";
            }

            ddlStatus.SelectedValue = AgentModel.Status.ToString();

            txtBalance.Text = AgentModel.Balance.ToString();
            txtBusinessLicense.Text = AgentModel.BusinessLicense;
            txtTelPhone.Text = AgentModel.TelPhone;
            txtCellPhone.Text = AgentModel.CellPhone;
            txtAccountBank.Text = AgentModel.AccountBank;
            txtAccountInfo.Text = AgentModel.AccountInfo;
            txtAccountNum.Text = AgentModel.AccountNum;
            txtWithdrawBank.Text = AgentModel.WithdrawBank;
            txtWithdrawInfo.Text = AgentModel.WithdrawInfo;
            txtWithdrawNum.Text = AgentModel.WithdrawNum;
           

            txtContactMail.Text = AgentModel.ContactMail;
            txtAddress.Text = AgentModel.Address;

            txtRemark.Text = AgentModel.Remark;

        }

        public void btnSave_Click(object sender, System.EventArgs e)
        {
            if (lbAgentid.Text.Trim() != "")
            {
                AgentModel = AgentBll.GetModel(int.Parse(lbAgentid.Text));
                userModel = userbll.GetModel(AgentModel.UserName);
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

 
            AgentModel.UserName = txtUserName.Text;
            AgentModel.Name = txtName.Text;
            
            AgentModel.ParentId = AgentBll.GetAgentId(txtParentLd.Text.Trim());// 显示推荐人ID 

            AgentModel.Status = int.Parse(ddlStatus.SelectedValue);
            
            AgentModel.BusinessLicense = txtBusinessLicense.Text;
            AgentModel.TelPhone = txtTelPhone.Text;
            AgentModel.CellPhone = txtCellPhone.Text;
            AgentModel.AccountBank = txtAccountBank.Text;
            AgentModel.AccountInfo = txtAccountInfo.Text;
            AgentModel.AccountNum = txtAccountNum.Text;
            AgentModel.WithdrawBank = txtWithdrawBank.Text;
            AgentModel.WithdrawInfo = txtWithdrawInfo.Text;
            AgentModel.WithdrawNum = txtWithdrawNum.Text;
            AgentModel.Balance = decimal.Parse(txtBalance.Text.Trim());
          

            AgentModel.ContactMail = txtContactMail.Text;
            AgentModel.Address = txtAddress.Text;

            AgentModel.CreatedDate = DateTime.Now;
            AgentModel.CreatedUserId = CurrentUser.UserID;

            AgentModel.RegisterIp = "";
            AgentModel.Remark = txtRemark.Text;

            if (lbAgentid.Text == "")
            {
                if (AgentBll.ExistsUsername(txtUserName.Text))
                {
                    lblMsg.Text = "用户名已存在，请重新输入！";
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

                    //新增代理商成功
                    if (AgentBll.Add(AgentModel) > 0)
                    {
                        YSWL.Common.MessageBox.ShowSuccessTip(this, string.Format("新增代理商：【{0}】成功！", txtUserName.Text));
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("新增代理商：【{0}】", txtUserName.Text), this);
                        Response.Redirect("AgentList.aspx");
                    }
                }
            }
            else
            {

                if (AgentBll.ExistsUsername(lbAgentid.Text, txtUserName.Text))
                {
                    lblMsg.Text = "用户名已存在，请重新输入！";
                }
                else
                {
                    userModel.UserName = txtUserName.Text;
                    AgentBll.Update(AgentModel);//修改企业信息
                    YSWL.Common.MessageBox.ShowSuccessTip(this, string.Format("修改代理商信息：【{0}】成功！", txtUserName.Text));
                    Response.Redirect("AgentList.aspx");
                }
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgentList.aspx");
        }

        /// <summary>
        /// 输入用户名，获得代理商姓名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtParentLd_TextChanged(object sender, EventArgs e)
        {
            string strName= AgentBll.GetNameByusername(txtParentLd.Text.Trim());
            if (strName.Trim().Length > 0)
            {
                lbParenUsername.Text = strName;
            }
            else
            {
                lbParenUsername.Text = "输入的用户名不存在！";
            }
        }
    }
}
