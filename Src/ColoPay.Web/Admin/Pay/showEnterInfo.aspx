<%@ Page Title="<%$ Resources:Site, ptAddUser %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="showEnterInfo.aspx.cs" Inherits="ColoPay.Web.Admin.Pay.showEnterInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="admintitle adminxia" style="margin-top: 10px">
            <div class="sj" style="margin-right: 20px;">
                <img src="../images/info.png" width="21" height="28" />
            </div>
            <strong><asp:Literal ID="Literal1" runat="server" Text="商户信息" /></strong> <asp:Label ID="lbEnterPid" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div class="main_bottomzi">
            <table style="width: 80%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="商户号码" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtEnterpriseNum" TabIndex="1" runat="server"  Width="300px"  BackColor="#cccccc" Enabled="false" ></asp:TextBox>
                            </td>
                            <td class="td_class">
                                <asp:Literal ID="Literal16" runat="server" Text="手机号" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtCellPhone" TabIndex="1" runat="server"  Width="300px"  BackColor="#cccccc" Enabled="false" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="公司简称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtSimpleName" TabIndex="4" runat="server"  Width="300px"  BackColor="#cccccc" Enabled="false" ></asp:TextBox>
                            </td>
                            <td class="td_class">
                                <asp:Literal ID="Literal17" runat="server" Text="账户余额" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtBalance" TabIndex="1" runat="server"  Width="300px"  BackColor="#cccccc" Enabled="false" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="会员级别" />：
                            </td>
                            <td style="height: 3px" height="3">
                                 <asp:DropDownList ID="ddlEnteRank" runat="server" Width="300px"   BackColor="#cccccc" Enabled="false">
                                    <asp:ListItem Selected="True" Text="普通商户" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="重点商户" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="股东商户" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="td_class">
                                <asp:Literal ID="Literal19" runat="server" Text="手续费" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtPayFree" TabIndex="1" runat="server"  Width="300px"  BackColor="#cccccc" Enabled="false" ></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>


        <div class="admintitle adminxia" style="margin-top: 10px">
            <div class="sj" style="margin-right: 20px;">
                <img src="../images/icon_1.gif" width="21" height="28" />
            </div>
            <strong><asp:Literal ID="Literal3" runat="server" Text="公司信息" /></strong> 
        </div>
        <div class="main_bottomzi">
            <table style="width: 80%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="公司全称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtName" TabIndex="1" runat="server"  Width="300px"  BackColor="#cccccc" Enabled="false" ></asp:TextBox>
                            </td>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="证件编号" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtBusinessLicense" TabIndex="1" runat="server"  Width="300px"  BackColor="#cccccc" Enabled="false" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="银行名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAccountInfo" TabIndex="4" runat="server"  Width="300px"  BackColor="#cccccc" Enabled="false" ></asp:TextBox>
                            </td>
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="对公账号" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAccountNum" TabIndex="1" runat="server"  Width="300px"  BackColor="#cccccc" Enabled="false" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal20" runat="server" Text="提现卡银行" />：
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:TextBox ID="txtWithdrawInfo" runat="server" Width="300px"  BackColor="#cccccc" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="td_class">
                                <asp:Literal ID="Literal21" runat="server" Text="提现卡账号" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtWithdrawNum" TabIndex="1" runat="server"  Width="300px"  BackColor="#cccccc" Enabled="false" ></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>

        <div class="admintitle adminxia" style="margin-top: 10px">
            <div class="sj" style="margin-right: 20px;">
                <img src="../images/out32.gif" width="21" height="28" />
            </div>
            <strong><asp:Literal ID="Literal11" runat="server" Text="接口信息" /></strong> 
        </div>
        <div class="main_bottomzi">
            <table style="width: 80%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal12" runat="server" Text="商户号码" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtEnterpriseNum2" TabIndex="1" runat="server" Width="300px"  BackColor="#cccccc" Enabled="false" ></asp:TextBox>
                            </td>
                            <td class="td_class">
                               
                            </td>
                            <td height="25">
                               
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal14" runat="server" Text="应用地址" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAppUrl" TabIndex="4" runat="server"  Width="240px"  BackColor="#6699ff"  ></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="lbtnAppUrl" runat="server" Text="更新"  Width="50px" BackColor="#3399FF" OnClick="lbtnAppUrl_Click" />
                            </td>
                            <td class="td_class">
                                <asp:Literal ID="Literal15" runat="server" Text="回调地址" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAppReturnUrl" TabIndex="1" runat="server"  Width="240px"  BackColor="#6699ff" ></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="lbtnAppReturnUrl" runat="server" Text="更新" Width="50px" BackColor="#3399FF" OnClick="lbtnAppReturnUrl_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal24" runat="server" Text="APPID编号" />：
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:TextBox ID="txtAppId" runat="server" Width="300px"  BackColor="#cccccc" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="td_class">
                                <asp:Literal ID="Literal25" runat="server" Text="SECRIT秘钥" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAppSecrit" TabIndex="1" runat="server"  Width="300px"  BackColor="#cccccc" Enabled="false" ></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
