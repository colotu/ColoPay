<%@ Page Title="<%$ Resources:SysManage, ptMenuManage%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="AgentList.aspx.cs" Inherits="ColoPay.Web.Admin.Pay.AgentList" %>

<%@ Register Assembly="ColoPay.Web" Namespace="ColoPay.Web.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">

        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang padd-no">
            <tr>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:DropDownList ID="seleEnterprise" runat="server" Width="200px">
                    </asp:DropDownList>
                    状态：<asp:DropDownList ID="ddlStatus" runat="server" Width="80px">
                        <asp:ListItem Value="" Text="全部">
                        </asp:ListItem>
                        <asp:ListItem Value="2" Text="未审核">
                        </asp:ListItem>
                        <asp:ListItem Value="1" Text="正常">
                        </asp:ListItem>
                        <asp:ListItem Value="0" Text="冻结">
                        </asp:ListItem>
                    </asp:DropDownList>
                    <asp:Literal ID="Literal3" runat="server" Text="代理商名称" />：
                    <asp:TextBox ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>

                    <asp:Button ID="btnSearch" runat="server" Text="查询"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <div class="newslist mar-bt">
            <div class="newsicon">
                <ul>
                    <li class="add-btn" id="liAdd" runat="server"><a href="AgentEdit.aspx">
                        <asp:Literal ID="Literal5" runat="server" Text="添加代理商" /></a>
                    </li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting"
            Width="100%" PageSize="15" DataKeyNames="AgentId" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px">
            <Columns>
                <asp:BoundField DataField="AgentId" HeaderText="代理商ID"
                    SortExpression="AgentId" ControlStyle-Width="50px" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center" />

                <asp:BoundField DataField="UserName" HeaderText="用户名"
                    SortExpression="UserName" ControlStyle-Width="200px" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" />

                <asp:BoundField DataField="Name" HeaderText="姓名"
                    SortExpression="Name" ItemStyle-HorizontalAlign="Left" />

                <asp:BoundField DataField="CellPhone" HeaderText="手机号"
                    SortExpression="CellPhone" ItemStyle-HorizontalAlign="Left" />

                <asp:BoundField DataField="Balance" HeaderText="代理商余额"
                    SortExpression="Balance" ItemStyle-HorizontalAlign="Left" />
               
                <asp:BoundField DataField="Status" HeaderText="状态"
                    SortExpression="Status" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="CreatedDate" HeaderText="时间"
                    SortExpression="CreatedDate" ItemStyle-HorizontalAlign="Left" />

                <asp:HyperLinkField HeaderText="设置费率" ControlStyle-Width="80"
                    DataNavigateUrlFields="AgentId" DataNavigateUrlFormatString="AgentPayFeeList.aspx?AgentId={0}"
                    Text="设置代理商费率" ItemStyle-HorizontalAlign="Center" />

                <asp:HyperLinkField HeaderText="操作" ControlStyle-Width="50"
                    DataNavigateUrlFields="AgentId" DataNavigateUrlFormatString="AgentEdit.aspx?AgentId={0}"
                    Text="编辑" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50px" HeaderText="删除"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="删除代理商"
                            Text="删除"></asp:LinkButton>
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
                    <%--<asp:Button ID="btnDelete" runat="server" Text="删除"
                        OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="删除"
                        class="add-btn" OnClick="btnDelete_Click" />--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
