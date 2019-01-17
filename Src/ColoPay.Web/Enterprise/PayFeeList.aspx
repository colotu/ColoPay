<%@ Page Title="<%$ Resources:SysManage, ptMenuManage%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="PayFeeList.aspx.cs" Inherits="ColoPay.Web.Enterprise.PayFeeList" %>

<%@ Register Assembly="ColoPay.Web" Namespace="ColoPay.Web.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">

        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang padd-no">
            <tr>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal1" runat="server" Text="商家名称" />：
                    <asp:Label ID="lbEnterName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="支付通道列表" />：
                    <asp:Label ID="lbEnterPid" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>

        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting"
            Width="100%" PageSize="15" DataKeyNames="ModeId" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px">
            <Columns>
                <asp:BoundField DataField="ModeId" HeaderText="支付通道ID" SortExpression="ModeId" ControlStyle-Width="50px" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />

                <asp:BoundField DataField="Name" HeaderText="支付通道名称" SortExpression="Name" ControlStyle-Width="400px" ItemStyle-Width="300px" ItemStyle-HorizontalAlign="Center" />

                <asp:BoundField DataField="Charge" HeaderText="费率" ItemStyle-HorizontalAlign="Left" />

                <asp:TemplateField ControlStyle-Width="100px" HeaderText="操作"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="关闭通道"
                            Text="关闭通道"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </cc1:GridViewEx>

        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td height="10px;"></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="返回列表"
                        OnClick="btnCancle_Click" class="adminsubmit_short"></asp:Button>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
