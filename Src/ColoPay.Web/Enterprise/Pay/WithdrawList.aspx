
<%@ Page Title="<%$ Resources:SysManage, ptMenuManage%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="WithdrawList.aspx.cs" Inherits="ColoPay.Web.Enterprise.Pay.WithdrawList" %>

<%@ Register Assembly="ColoPay.Web" Namespace="ColoPay.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/Admin/js/tab.js" type="text/javascript"></script>
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.cookie.js" type="text/javascript"></script>
    <link href="/Scripts/jBox/Skins/Blue/jbox.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jBox/jquery.jBox-2.3.min.js" type="text/javascript"></script>
    <script src="/Scripts/jBox/i18n/jquery.jBox-zh-CN.js" type="text/javascript"></script>
    <link href="/Admin/js/select2-3.4.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-3.4.1/select2.min.js" type="text/javascript" charset="utf-8"></script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">

        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang padd-no">
            <tr>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    
                    状态：<asp:DropDownList ID="ddlStatus" runat="server" Width="80px">
                        <asp:ListItem Value="" Text="全部">
                        </asp:ListItem>
                        <asp:ListItem Value="0" Text="未审核">
                        </asp:ListItem>
                        <asp:ListItem Value="1" Text="已审核">
                        </asp:ListItem>
                        <asp:ListItem Value="2" Text="已支付">
                        </asp:ListItem>
                    </asp:DropDownList>
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, lblKeyword %>" />：
                    <asp:TextBox ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>

        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowCommand="gridView_RowCommand" 
            Width="100%" PageSize="15" DataKeyNames="WithdrawId" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px">
            <Columns>


                <asp:TemplateField ControlStyle-Width="150" HeaderText="提交时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="200">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("CreatedDate")).ToString("yyyy-MM-dd HH:mm:ss")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="WithdrawCode" HeaderText="订单编码"
                    SortExpression="WithdrawCode" ControlStyle-Width="80px" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" />
                

                <asp:TemplateField ControlStyle-Width="50" HeaderText="提现金额"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#  Convert.ToDecimal(Eval("Amount")).ToString("F2") %>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField ControlStyle-Width="100" HeaderText="提现银行"
                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150">
                    <ItemTemplate>
                        <%#  Eval("WithdrawBank") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="100" HeaderText="开户行信息"
                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150">
                    <ItemTemplate>
                        <%#  Eval("WithdrawInfo") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="100" HeaderText="银行帐号"
                    ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150">
                    <ItemTemplate>
                        <%#  Eval("WithdrawNum") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="150" HeaderText="审核时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="200">
                    <ItemTemplate>
                        <%#GetDateStr(Eval("AuditDate"))%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="审核人" ItemStyle-HorizontalAlign="center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#GetUserName(Eval("AuditUserId"))%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="150" HeaderText="付款时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="200">
                    <ItemTemplate>
                        <%#GetDateStr(Eval("PayDate"))%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="付款人" ItemStyle-HorizontalAlign="center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#GetUserName(Eval("PayUserId"))%>
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

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
    
</asp:Content>
