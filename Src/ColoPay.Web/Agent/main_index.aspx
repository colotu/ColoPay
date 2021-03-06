﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main_index.aspx.cs" Inherits="ColoPay.Web.Agent.main_index" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>银河支付代理商后台</title>
    <script src="/admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link href="/admin/css/admin.css" rel="stylesheet" type="text/css" />
    <link href="/admin/js/easyui/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/easyui/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function addTab(title, url) {
            if ($('#tabs').tabs('exists', title)) {
                $('#tabs').tabs('select', title); //选中并刷新
                var currTab = $('#tabs').tabs('getSelected');
                var url = $(currTab.panel('options').content).attr('src');
                if (url != undefined && currTab.panel('options').title != '我的桌面') {
                    $('#tabs').tabs('update', {
                        tab: currTab,
                        options: {
                            content: createFrame(url)
                        }
                    });
                }
            } else {
                var index = $('#tabs').find(".tabs").find("li").length;
                if (index == 20) {
                    alert("开启的菜单太多，请先关闭部分菜单！");
                    return;
                }
                var content = createFrame(url);
                $('#tabs').tabs('add', {
                    title: title,
                    content: content,
                    closable: true
                });
            }
            tabClose();
        }
        function createFrame(url) {
            var s = '<iframe scrolling="auto" frameborder="0"  border="0px" src="' + url + '" style="width:100%;height:99%;"></iframe>';
            return s;
        }

        function tabClose() {
            /*双击关闭TAB选项卡*/
            $(".tabs-inner").dblclick(function () {
                var subtitle = $(this).children(".tabs-closable").text();
                $('#tabs').tabs('close', subtitle);
            });
            /*为选项卡绑定右键*/
            $(".tabs-inner").bind('contextmenu', function (e) {
                $('#mm').menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });

                var subtitle = $(this).children(".tabs-closable").text();

                $('#mm').data("currtab", subtitle);
                $('#tabs').tabs('select', subtitle);
                return false;
            });
        }
        //绑定右键菜单事件
        function tabCloseEven() {
            //刷新
            $('#mm-tabupdate').click(function () {
                var currTab = $('#tabs').tabs('getSelected');
                var url = $(currTab.panel('options').content).attr('src');
                if (url != undefined && currTab.panel('options').title != '我的桌面') {
                    $('#tabs').tabs('update', {
                        tab: currTab,
                        options: {
                            content: createFrame(url)
                        }
                    })
                }
            })
            //关闭当前
            $('#mm-tabclose').click(function () {
                var currtab_title = $('#mm').data("currtab");
                $('#tabs').tabs('close', currtab_title);
            })
            //全部关闭
            $('#mm-tabcloseall').click(function () {
                $('.tabs-inner span').each(function (i, n) {
                    var t = $(n).text();
                    if (t != '我的桌面') {
                        $('#tabs').tabs('close', t);
                    }
                });
            });
            //关闭除当前之外的TAB
            $('#mm-tabcloseother').click(function () {
                var prevall = $('.tabs-selected').prevAll();
                var nextall = $('.tabs-selected').nextAll();
                if (prevall.length > 0) {
                    prevall.each(function (i, n) {
                        var t = $('a:eq(0) span', $(n)).text();
                        if (t != '我的桌面') {
                            $('#tabs').tabs('close', t);
                        }
                    });
                }
                if (nextall.length > 0) {
                    nextall.each(function (i, n) {
                        var t = $('a:eq(0) span', $(n)).text();
                        if (t != '我的桌面') {
                            $('#tabs').tabs('close', t);
                        }
                    });
                }
                return false;
            });
            //关闭当前右侧的TAB
            $('#mm-tabcloseright').click(function () {
                var nextall = $('.tabs-selected').nextAll();
                if (nextall.length == 0) {
                    //msgShow('系统提示','后边没有啦~~','error');
                    alert('后边没有啦~~');
                    return false;
                }
                nextall.each(function (i, n) {
                    var t = $('a:eq(0) span', $(n)).text();
                    $('#tabs').tabs('close', t);
                });
                return false;
            });
            //关闭当前左侧的TAB
            $('#mm-tabcloseleft').click(function () {
                var prevall = $('.tabs-selected').prevAll();
                if (prevall.length == 0) {
                    alert('到头了，前边没有啦~~');
                    return false;
                }
                prevall.each(function (i, n) {
                    var t = $('a:eq(0) span', $(n)).text();
                    $('#tabs').tabs('close', t);
                });
                return false;
            });

            //退出
            $("#mm-exit").click(function () {
                $('#mm').menu('hide');
            });
        }

        $(function () {
            tabCloseEven();
            $('#tabIndex').find('a').click(function () {
                var $this = $(this);
                var href = $this.attr('src');
                var title = $this.text();
                addTab(title, href);
            });
        });

    </script>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
        <div region="center" id="mainPanle">
            <div id="tabs" class="easyui-tabs" fit="true" border="false">
                <div title="我的桌面">
                    <div id="tabIndex" style="overflow: scroll; overflow-x: hidden; overflow-y: hidden">
                        <div class="admintitle adminxia" style="margin-top: 10px">
                            <div class="sj" style="margin-right: 20px;">
                                <img src="../images/info.png" width="21" height="28" />
                            </div>
                            <strong>
                                <asp:Literal ID="Literal1" runat="server" Text="代理商信息" /></strong>
                            <asp:Label ID="lbAgentId" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="main_bottomzi">
                            <table style="width: 80%;" cellpadding="2" cellspacing="1" class="border">
                                <tr>
                                    <td class="tdbg">
                                        <table cellspacing="0" cellpadding="3" width="100%" border="0">
                                            <tr>
                                                <td class="td_class">
                                                    <asp:Literal ID="Literal2" runat="server" Text="代理商用户" />：
                                                </td>
                                                <td height="25">
                                                    <asp:TextBox ID="txtUsername" TabIndex="1" runat="server" Width="300px" BackColor="#cccccc" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td class="td_class">
                                                    <asp:Literal ID="Literal16" runat="server" Text="手机号" />：
                                                </td>
                                                <td height="25">
                                                    <asp:TextBox ID="txtCellPhone" TabIndex="1" runat="server" Width="300px" BackColor="#cccccc" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_class">
                                                    <asp:Literal ID="Literal5" runat="server" Text="上级推荐人" />：
                                                </td>
                                                <td height="25">
                                                    <asp:TextBox ID="txtPraent" TabIndex="4" runat="server" Width="300px" BackColor="#cccccc" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td class="td_class">
                                                    <asp:Literal ID="Literal17" runat="server" Text="账户余额" />：
                                                </td>
                                                <td height="25">
                                                    <asp:TextBox ID="txtBalance" TabIndex="1" runat="server" Width="300px" BackColor="#cccccc" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_class">
                                                    <asp:Literal ID="Literal19" runat="server" Text="手续费" />：
                                                </td>
                                                <td style="height: 3px" height="3">
                                                    <asp:TextBox ID="txtPayFree" TabIndex="1" runat="server" Width="300px" BackColor="#cccccc" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td class="td_class"></td>
                                                <td height="25"></td>
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
                            <strong>
                                <asp:Literal ID="Literal3" runat="server" Text="公司信息" /></strong>
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
                                                    <asp:TextBox ID="txtName" TabIndex="1" runat="server" Width="300px" BackColor="#cccccc" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td class="td_class">
                                                    <asp:Literal ID="Literal6" runat="server" Text="证件编号" />：
                                                </td>
                                                <td height="25">
                                                    <asp:TextBox ID="txtBusinessLicense" TabIndex="1" runat="server" Width="300px" BackColor="#cccccc" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_class">
                                                    <asp:Literal ID="Literal8" runat="server" Text="银行名称" />：
                                                </td>
                                                <td height="25">
                                                    <asp:TextBox ID="txtAccountInfo" TabIndex="4" runat="server" Width="300px" BackColor="#cccccc" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td class="td_class">
                                                    <asp:Literal ID="Literal10" runat="server" Text="对公账号" />：
                                                </td>
                                                <td height="25">
                                                    <asp:TextBox ID="txtAccountNum" TabIndex="1" runat="server" Width="300px" BackColor="#cccccc" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="td_class">
                                                    <asp:Literal ID="Literal20" runat="server" Text="提现卡银行" />：
                                                </td>
                                                <td style="height: 3px" height="3">
                                                    <asp:TextBox ID="txtWithdrawInfo" runat="server" Width="300px" BackColor="#cccccc" Enabled="false"></asp:TextBox>
                                                </td>
                                                <td class="td_class">
                                                    <asp:Literal ID="Literal21" runat="server" Text="提现卡账号" />：
                                                </td>
                                                <td height="25">
                                                    <asp:TextBox ID="txtWithdrawNum" TabIndex="1" runat="server" Width="300px" BackColor="#cccccc" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <div id="mm" class="easyui-menu" style="width: 120px;">
        <div id="mm-tabupdate">
            刷新
        </div>
        <div class="menu-sep">
        </div>
        <div id="mm-tabclose">
            关闭
        </div>
        <div id="mm-tabcloseother">
            关闭其他
        </div>
        <div id="mm-tabcloseall">
            关闭全部
        </div>
    </div>
</body>
</html>
