<%@ Page Title="<%$ Resources:SysManage, ptMenuManage%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="BalanceDetail.aspx.cs" Inherits="ColoPay.Web.Admin.Pay.BalanceDetail" %>

<%@ Register Assembly="ColoPay.Web" Namespace="ColoPay.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
<script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
 <script type="text/javascript">
        $(function () {
            
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);

            $("[id$='txtDateStart']").prop("readonly", true).datepicker({
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("[id$='txtDateEnd']").datepicker("option", "minDate", selectedDate);
                }
            });
            $("[id$='txtDateEnd']").prop("readonly", true).datepicker({
               
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("[id$='txtDateStart']").datepicker("option", "maxDate", selectedDate);
                    $("[id$='txtDateEnd']").val($(this).val());
                }
            });

           
        });
      
    </script>
</asp:Content>
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
                    </asp:DropDownList>
                    类型：<asp:DropDownList ID="ddlType" runat="server" Width="80px">
                        <asp:ListItem Value="" Text="全部">
                        </asp:ListItem>
                        <asp:ListItem Value="0" Text="支付">
                        </asp:ListItem>
                        <asp:ListItem Value="1" Text="提现">
                        </asp:ListItem>
                    </asp:DropDownList>
                         <asp:Literal ID="LiteralCreatedDate" runat="server" Text="创建日期" />：
                    <asp:TextBox ID="txtDateStart" runat="server" Width="120px">                     
                    </asp:TextBox>-<asp:TextBox ID="txtDateEnd" Width="120px" runat="server"></asp:TextBox>

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
                <asp:TemplateField ControlStyle-Width="50" HeaderText="类型"
                    ItemStyle-HorizontalAlign="left" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <%#YSWL.Common.Globals.SafeInt(Eval("Type"),0)==0?"商家":"代理商" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="企业名称"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# GetEnterpriseName(Eval("EnterpriseID"),Eval("AgentId"),Eval("Type")) %>
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
