using System;
using YSWL.Accounts.Bus;
using ColoPay.BLL.Members;
using ColoPay.Model.Members;
using System.Web;

namespace ColoPay.Web.Admin.Pay
{
    public partial class AgentPayFeeEdit : PageBaseAdmin
    {
        public string adminname = "Management";
        private UserType userTypeManage = new UserType();


        BLL.Members.Users userbll = new BLL.Members.Users();
        Model.Members.Users userModel = new Model.Members.Users();

        ColoPay.BLL.Pay.Agent AgentBll = new BLL.Pay.Agent();
        ColoPay.Model.Pay.Agent AgentModel = new Model.Pay.Agent();


        ColoPay.Model.Pay.PaymentTypes PaytypesModel = new Model.Pay.PaymentTypes();
        ColoPay.BLL.Pay.PaymentTypes PaytypesBll = new BLL.Pay.PaymentTypes();

        Model.Pay.AgentPayFee PayFreeModel = new Model.Pay.AgentPayFee();
        ColoPay.BLL.Pay.AgentPayFee PayFreeBll = new BLL.Pay.AgentPayFee();

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
                if ((Request.Params["AgentId"] != null) && (Request.Params["AgentId"].ToString() != ""))
                {
                    lbAgentId.Text = Request.Params["AgentId"].Trim();
                    if (lbAgentId.Text != "")
                    {
                        ShowInfoEnter(lbAgentId.Text);
                    }
                }
                //操作类型，新增通道 typeid是0，编辑通道费率 typeid是1，
                if ((Request.Params["typeid"] != null) && (Request.Params["typeid"].ToString() != ""))
                {
                    lbType.Text = Request.Params["typeid"].Trim();
                    if (lbType.Text == "1")
                    {
                        Model.Pay.AgentPayFee payfeeModel = PayFreeBll.GetModel(int.Parse(lbAgentId.Text), int.Parse(lbPayModelid.Text));
                        txtPayFree.Text = payfeeModel.FeeRate.ToString();
                    }
                }

            }
        }

        public void ShowInfoEnter(string strEnerpid)
        {
            AgentModel = AgentBll.GetModel(int.Parse(strEnerpid));
            lbEnterName.Text = AgentModel.UserName; 
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

            PayFreeModel.AgentID = int.Parse(lbAgentId.Text);
            PayFreeModel.PayModeId = int.Parse(lbPayModelid.Text);
            PayFreeModel.FeeRate = decimal.Parse(txtPayFree.Text);



            if (lbType.Text == "0")
            {
                //开通通道
                if (PayFreeBll.Add(PayFreeModel))
                {
                    YSWL.Common.MessageBox.ShowSuccessTip(this, string.Format("开通代理商通道成功，费率：【{0}】！", txtPayFree.Text));
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format(" 代理商" + lbAgentId.Text + " ,设置费率：【{0}】", txtPayFree.Text), this);
                    Response.Redirect("AgentPayFeeList.aspx?AgentId=" + lbAgentId.Text + "");
                }
            }
            else
            {
                if (PayFreeBll.Update(PayFreeModel))                
                {
                    YSWL.Common.MessageBox.ShowSuccessTip(this, string.Format("修改费率：【{0}】成功！", txtPayFree.Text));
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format(" 代理商" + lbAgentId.Text + " ,设置费率：【{0}】", txtPayFree.Text), this);
                    Response.Redirect("AgentPayFeeList.aspx?AgentId=" + lbAgentId.Text+"");
                }
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgentPayFeeList.aspx?AgentId=" + lbAgentId.Text + "");
        }
        
    }
}
