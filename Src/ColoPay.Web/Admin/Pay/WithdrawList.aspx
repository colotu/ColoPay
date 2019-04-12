<%@ Page Title="<%$ Resources:SysManage, ptMenuManage%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="WithdrawList.aspx.cs" Inherits="ColoPay.Web.Admin.Pay.WithdrawList" %>

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
                <asp:TemplateField ControlStyle-Width="50" HeaderText="企业名称"
                    ItemStyle-HorizontalAlign="left" ItemStyle-Width="250px">
                    <ItemTemplate>
                         <%# GetEnterpriseName(Eval("EnterpriseID"),Eval("AgentId"),Eval("Type")) %>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField ControlStyle-Width="50" HeaderText="提现类型"
                    ItemStyle-HorizontalAlign="left" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <%#YSWL.Common.Globals.SafeInt(Eval("Type"),0)==0?"商家":"代理商" %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="50" HeaderText="提现金额"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <%#  Convert.ToDecimal(Eval("Amount")).ToString("F2") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="100" HeaderText="提现人"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100">
                    <ItemTemplate>
                        <%#  Eval("UserName") %>
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
               <asp:TemplateField ControlStyle-Width="160" HeaderText="操作" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <span class="audit" style="display:none" status='<%#Eval("Status")%>' >
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Audit"
                                CommandArgument='<%#Eval("WithdrawId")+","+Eval("Status")%>'
                                OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="您确认要审核通过吗？ " Text="审核"></asp:LinkButton>
                        </span>
                        <span class="pay" style="display:none" status='<%#Eval("Status")%>'>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Pay"
                                CommandArgument='<%#Eval("WithdrawId")+","+Eval("Status")%>'
                                OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="您确认付款了吗？ " Text="已付款"></asp:LinkButton>
                        </span>
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
      <script type="text/javascript">
        $(document).ready(function () {


            //显示、移除 除查看外的其他操作  
            $(".audit").each(function () {
                //主订单  有子订单  并且已支付  或者 是 货到付款/银行转账  ( 就是可以看到子单了)  就不能再对主单操作，只能查看
                var status = parseInt($(this).attr("status"));
                if (status == 0) {
                    $(this).show();
                }
            });

            $(".pay").each(function () {
                //主订单  有子订单  并且已支付  或者 是 货到付款/银行转账  ( 就是可以看到子单了)  就不能再对主单操作，只能查看
                var status = parseInt($(this).attr("status"));
                if (status == 1) {
                    $(this).show();
                }
            });
        });

    </script>
</asp:Content>
