<%@ Page Title="<%$ Resources:Site, ptAddUser %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="PayFeeEdit.aspx.cs" Inherits="ColoPay.Web.Agent.Pay.PayFeeEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td bgcolor="#FFFFFF" class="newstitle">
                    <asp:Literal ID="Literal1" runat="server" Text="设置费率信息" /> <asp:Label ID="lbAgentId" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal6" runat="server" Text="商户名称：" /><asp:Literal ID="lbEnterName" runat="server" Text="" />
                    <asp:Literal ID="Literal4" runat="server" Text="支付通道：" /><asp:Literal ID="lbPayTypeName" runat="server" Text="" />
                    <asp:Label ID="lbEnterPid" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lbPayModelid" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:Label ID="lbType" runat="server" Text="" Visible="false"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>

        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="费率" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtPayFree" TabIndex="1" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                                （默认密码：小数，例如;0.006）
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class"></td>
                            <td height="25">
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="返回列表"
                                    OnClick="btnCancle_Click" class="adminsubmit_short"></asp:Button>
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
