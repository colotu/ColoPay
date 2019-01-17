<%@ Page Title="<%$ Resources:SysManage, ptMenuManage%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="PayInfo.aspx.cs" Inherits="ColoPay.Web.Enterprise.PayInfo" %>

<%@ Register Assembly="ColoPay.Web" Namespace="ColoPay.Web.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">

        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang padd-no">
            <tr>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal1" runat="server" Text="商家名称" />：
                    <asp:Label ID="lbEnterName" runat="server" Text=""></asp:Label><asp:Label ID="lbEnterPid" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 90%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal12" runat="server" Text="商户号码" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtEnterpriseNum2" TabIndex="1" runat="server" Width="300px" BackColor="#cccccc" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="td_class"></td>
                            <td height="25"></td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal14" runat="server" Text="应用地址" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAppUrl" TabIndex="4" runat="server" Width="240px" BackColor="#6699ff"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="lbtnAppUrl" runat="server" Text="更新" Width="50px" BackColor="#3399FF" OnClick="lbtnAppUrl_Click" />
                            </td>
                            <td class="td_class">
                                <asp:Literal ID="Literal15" runat="server" Text="回调地址" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAppReturnUrl" TabIndex="1" runat="server" Width="240px" BackColor="#6699ff"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="lbtnAppReturnUrl" runat="server" Text="更新" Width="50px" BackColor="#3399FF" OnClick="lbtnAppReturnUrl_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal24" runat="server" Text="APPID编号" />：
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:TextBox ID="txtAppId" runat="server" Width="300px" BackColor="#cccccc" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="td_class">
                                <asp:Literal ID="Literal25" runat="server" Text="SECRIT秘钥" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAppSecrit" TabIndex="1" runat="server" Width="300px" BackColor="#cccccc" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="接口文档" />：
                            </td>
                            <td style="height: 3px" height="3">
                                
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Upload/银河支付接口对接文档.doc" Target="_blank">接口说明下载</asp:HyperLink>
                                
                            </td>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="Demo" />：
                            </td>
                            <td height="25">
                                
                                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Upload/Demo.rar" Target="_blank">Demo(.NET版本)下载</asp:HyperLink>
                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
