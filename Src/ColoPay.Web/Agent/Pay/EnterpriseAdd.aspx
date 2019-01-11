<%@ Page Title="<%$ Resources:Site, ptAddUser %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="EnterpriseAdd.aspx.cs" Inherits="ColoPay.Web.Agent.Pay.EnterpriseAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td bgcolor="#FFFFFF" class="newstitle">
                    <asp:Literal ID="lbTitle" runat="server" Text="添加商户信息" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Label ID="lbEnterPid" runat="server" Text="" Visible="false"></asp:Label>
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
                                <asp:Literal ID="Literal2" runat="server" Text="登录用户名" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtUserName" TabIndex="1" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                    Display="Dynamic" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>（默认密码：111111）
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="企业名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtName" TabIndex="4" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="企业简称" />：
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:TextBox ID="txtSimpleName" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="状态" />：
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Selected="True" Text="正常" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="冻结" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="商户号" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtEnterpriseNum" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="证件编号" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtBusinessLicense" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal11" runat="server" Text="企业电话" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtTelPhone" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal12" runat="server" Text="手机号码" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtCellPhone" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal13" runat="server" Text="银行名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAccountBank" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal14" runat="server" Text="开户行信息" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAccountInfo" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal15" runat="server" Text="银行账号" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAccountNum" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal16" runat="server" Text="提现银行" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtWithdrawBank" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal17" runat="server" Text="提现开户行信息" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtWithdrawInfo" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal18" runat="server" Text="提现开户行账号" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtWithdrawNum" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal19" runat="server" Text="商家余额" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtBalance" runat="server" Width="200px">0</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal20" runat="server" Text="商家AppId" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAppId" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal21" runat="server" Text="密钥" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAppSecrit" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal22" runat="server" Text="应用地址" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAppUrl" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal23" runat="server" Text="应用回调地址" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAppReturnUrl" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal24" runat="server" Text="联系邮箱" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtContactMail" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal25" runat="server" Text="企业地址" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAddress" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal26" runat="server" Text="商户等级" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlEnteRank" runat="server">
                                    <asp:ListItem Selected="True" Text="普通商户" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="重点商户" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="股东商户" Value="2"></asp:ListItem>
                                </asp:DropDownList>
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
