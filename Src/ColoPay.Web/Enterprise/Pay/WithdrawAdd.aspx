<%@ Page Title="<%$ Resources:Site, ptAddUser %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true"  CodeBehind="WithdrawAdd.aspx.cs" Inherits="ColoPay.Web.Enterprise.Pay.WithdrawAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td bgcolor="#FFFFFF" class="newstitle">
                    <asp:Literal ID="lbTitle" runat="server" Text="申请提现" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFFF" class="newstitlebody">
                  
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
                                <asp:Literal ID="Literal2" runat="server" Text="提现金额" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAmount" TabIndex="1" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>

                             <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="提现人" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtUserName" TabIndex="1" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="提现银行" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtWithdrawBank" TabIndex="4" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="开户行信息" />：
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:TextBox ID="txtWithdrawInfo" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                          <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal1" runat="server" Text="银行账号" />：
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:TextBox ID="txtWithdrawNum" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal27" runat="server" Text="备注" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtRemark" runat="server" Width="296px" Height="60px" TextMode="MultiLine"></asp:TextBox>
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

