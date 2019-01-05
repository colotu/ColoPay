<%@ Page Title="<%$ Resources:SysManage, ptMenuManage%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="ColoPay.Web.Admin.Pay.OrderList" %>

<%@ Register Assembly="ColoPay.Web" Namespace="ColoPay.Web.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang padd-no">
               <tr>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                     <asp:Literal ID="Literal1" runat="server" Text="代理商" />：
                    <asp:DropDownList ID="ddlAgent" runat="server" Width="200px" OnSelectedIndexChanged="ddlAgent_Changed">
                    </asp:DropDownList>
                    <asp:Literal ID="Literal2" runat="server" Text="商户" />：
                    <asp:DropDownList ID="ddlEnterprise" runat="server" Width="200px">
                    </asp:DropDownList> 状态：<asp:DropDownList ID="ddlStatus" runat="server" Width="80px">
                        <asp:ListItem Value="" Text="全部">
                        </asp:ListItem>
                        <asp:ListItem Value="0" Text="未支付">
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
            OnRowDataBound="gridView_RowDataBound"   
            Width="100%" PageSize="15" DataKeyNames="OrderId" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"   >
            <Columns>
              
             
               <asp:TemplateField ControlStyle-Width="150" HeaderText="下单时间" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="200">
                    <ItemTemplate>
                        <%#Convert.ToDateTime(Eval("CreatedTime")).ToString("yyyy-MM-dd HH:mm:ss")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="OrderCode" HeaderText="平台订单编码"
                    SortExpression="OrderCode" ControlStyle-Width="80px" ItemStyle-Width="120px" ItemStyle-HorizontalAlign="Center" />
                 
                <asp:BoundField DataField="EnterOrder" HeaderText="商家订单编码" ItemStyle-Width="120px"
                    SortExpression="EnterOrder" ItemStyle-HorizontalAlign="Left" />

                 <asp:TemplateField ControlStyle-Width="50" HeaderText="企业名称"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250px" >
                    <ItemTemplate>
                        <%# GetEnterpriseName(Eval("EnterpriseID")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField ControlStyle-Width="50" HeaderText="代理商"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="250px" >
                    <ItemTemplate>
                        <%# GetAgentName(Eval("Agentd")) %>
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

                  <asp:TemplateField ControlStyle-Width="50" HeaderText="费率"
                    ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80">
                    <ItemTemplate>
                        <%# Convert.ToDecimal(Eval("FeeRate")).ToString("F2") %>%
                    </ItemTemplate>
                </asp:TemplateField>

                   <asp:TemplateField ControlStyle-Width="50" HeaderText="订单金额"
                    ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="80">
                    <ItemTemplate>
                        <%# Convert.ToDecimal(Eval("OrderAmount")).ToString("F2")%>
                    </ItemTemplate>
                </asp:TemplateField>

                    <asp:TemplateField ControlStyle-Width="100" HeaderText="支付方式"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150">
                    <ItemTemplate>
                        <%#  Eval("PaymentTypeName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="center"  ItemStyle-Width="80">
                    <ItemTemplate>
                     <%#(int)Eval("PaymentStatus")==2 ? " <span style='color:green;'>已支付</span>" : "<span  style='color:red;'>未支付<span>"%> 
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField  HeaderText="订单信息"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#  Eval("OrderInfo") %>
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
