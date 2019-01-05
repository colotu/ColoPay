<%@ Page Title="<%$ Resources:SysManage, ptMenuManage%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="MyBalance.aspx.cs" Inherits="ColoPay.Web.Agent.Pay.MyBalance" %>

<%@ Register Assembly="ColoPay.Web" Namespace="ColoPay.Web.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">

        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang padd-no">
            <tr>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    类型：<asp:DropDownList ID="ddlType" runat="server" Width="80px">
                        <asp:ListItem Value="" Text="全部">
                        </asp:ListItem>
                        <asp:ListItem Value="0" Text="支付">
                        </asp:ListItem>
                        <asp:ListItem Value="1" Text="提现">
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
            OnRowDataBound="gridView_RowDataBound"
            Width="100%" PageSize="15" DataKeyNames="DetailId" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px">
            <Columns>


                <asp:TemplateField ControlStyle-Width="150" HeaderText="操作时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="200">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("CreatedTime")).ToString("yyyy-MM-dd HH:mm:ss")%>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField ControlStyle-Width="50" HeaderText="金额"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                         <%#(int)Eval("PayType")==0 ? " <span style='color:green;'>+" : "<span  style='color:red;'>-"%><%#Convert.ToDecimal(Eval("OrderAmount")).ToString("F2")+"</span>" %> 
                    </ItemTemplate>
                </asp:TemplateField>
 
                 <asp:TemplateField ControlStyle-Width="50" HeaderText="支付金额"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#  Convert.ToDecimal(Eval("Amount")).ToString("F2") %>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField ControlStyle-Width="50" HeaderText="手续费"
                    ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#  Convert.ToDecimal(Eval("PaymentFee")).ToString("F2") %>
                    </ItemTemplate>
                </asp:TemplateField>
                 

                <asp:BoundField DataField="OriginalCode" HeaderText="原始订单" ItemStyle-Width="120px"
                    SortExpression="OriginalCode" ItemStyle-HorizontalAlign="Left" />


                <asp:TemplateField HeaderText="操作类型" ItemStyle-HorizontalAlign="center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#(int)Eval("PayType")==0 ? " <span style='color:green;'>支付</span>" : "<span  style='color:red;'>提现<span>"%>
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
