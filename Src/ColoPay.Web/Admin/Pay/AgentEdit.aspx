<%@ Page Title="<%$ Resources:Site, ptAddUser %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="AgentEdit.aspx.cs" Inherits="ColoPay.Web.Admin.Pay.AgentEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td bgcolor="#FFFFFF" class="newstitle">
                    <asp:Literal ID="lbTitle" runat="server" Text="添加代理商信息" />
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Label ID="lbAgentid" runat="server" Text="" Visible="false"></asp:Label>
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
                                <asp:Literal ID="Literal5" runat="server" Text="姓名" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtName" TabIndex="4" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
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
                                <asp:Literal ID="Literal7" runat="server" Text="推荐人认账号" />：
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:TextBox ID="txtParentLd" runat="server" Width="200px" OnTextChanged="txtParentLd_TextChanged"></asp:TextBox><asp:Label ID="lbParenUsername" runat="server" Text="" Visible="false"></asp:Label>
                                （当推荐人账户不存在时，系统默认为0）</td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="状态" />：
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Value="2" Text="未审核"></asp:ListItem>
                                    <asp:ListItem Selected="True" Text="正常" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="冻结" Value="0"></asp:ListItem>
                                </asp:DropDownList>
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
                                <asp:Literal ID="Literal10" runat="server" Text="营业执照" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtBusinessLicense" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal11" runat="server" Text="公司电话" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtTelPhone" runat="server" Width="200px"></asp:TextBox>
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
