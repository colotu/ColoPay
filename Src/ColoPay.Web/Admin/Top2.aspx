<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top2.aspx.cs" Inherits="ColoPay.Web.Admin.Top2" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/admin/css/admin.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('.adminmenu a').click(function () {
                var $this = $(this);
                var href = $this.attr('src');
                var title = $this.text();
                if (href) {
                    title = $.trim(title);
                    if (!title) {
                        title = "系统消息";
                    }
                    window.parent.mainFrame.addTab(title, href);
                }

            });
        });
    </script>
    <%--    <script type="text/javascript">
        var hasNewInfo = 0;
        var loadComment = function () {
            $.ajax({
                url: "CMSContent.aspx",
                type: "post", dataType: 'text', timeout: 10000, async: false,
                data: { action: "MessageInfo", cid: $("[id$='hfCurrentID']").val() },
                success: function (resultData) {
                    hasNewInfo = resultData;
                }
            });
        };
        $(function () {
            loadComment(); //先启动一次
            setInterval(loadComment, 60 * 1000);
        });

        setInterval(function () {
            if (hasNewInfo) {
                $("[id$='lblTotal']").text(hasNewInfo);
            }
            else {
                $("[id$='lblTotal']").text('0');
            }
        }, 500);
    </script> --%>
</head>
<body>
    <form id="Form1" runat="server">
    <div class="top">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="193" valign="top">
                    <img src="images/logo.gif" alt="" />
                </td>
                <td>
                    <div class="nav">
                        <ul class="TabBarLevel1" id="TabPage1">
                            <%=strMenu %>
                        </ul>
                    </div>
                </td>
            </tr>
        </table>
        <div class="righttop">
            <ul>
                <li class="righttophelp"><a href="http://www.ys56.com/lianxiwomen.html" target="mainFrame">意见反馈</a></li>
                <li class="righttopabout"><a id="maticsoft" href="http://www.ys56.com/" target="_blank">
                    关于</a></li>
            </ul>
        </div>
    </div>
    <div class="adminmenu">
        欢迎您，<%=username%>
        <em>|</em> <a src="Accounts/userinfo.aspx" href="javascript:;" target="mainFrame">基本信息</a>
        <em>|</em> <a src="Accounts/userpass.aspx" href="javascript:;" target="mainFrame">修改密码</a>
        <em>|</em> <a src="sysmanage/treefavorite.aspx?TreeType=0" href="javascript:;" target="mainFrame">
            定制菜单</a> <em>|</em> <a href="/" target="_blank">网站前台</a> <em>|</em> <a src="Members/SiteMessages/List.aspx"
                href="javascript:;" target="mainFrame" class="hover_url"><font color="#3186c8" style="text-decoration: underline;">
                    <asp:HiddenField ID="hfCurrentID" runat="server"></asp:HiddenField>
                    <asp:Label ID="lblTotal" runat="server"></asp:Label></font> </a>
    </div>
    <%--<div class="logionout">
        <div class="adminleft">
            <ul>
                <li><a href="QuickLinks.aspx" target="leftFrame">快捷菜单</a></li>
                <li class="padd1"><a href="logout.aspx">退出</a></li>
            </ul>
        </div>
        <div class="adminright" style="display: none">
            <a href="main_index.aspx" target="mainFrame" class="daohanglink" style="line-height:15px;">首页</a></div>
    </div>--%>
    <script language="JavaScript" type="text/javascript">
        //Switch Tab Effect
        function switchTab(tabpage, tabid) {
            //         var oItem = document.getElementById(tabpage);
            //         for (var i = 0; i < oItem.children.length; i++) {
            //             var x = oItem.children(i);
            //             x.className = "";
            ////             var y = x.getElementsByTagName('a');
            //         }
            $("#TabPage1 li").removeClass('Selected');
            $("#TabPage1").find("#" + tabid).addClass('Selected');

            //         document.getElementById(tabid).className = "Selected";
            //         var dvs = document.getElementById("cnt").getElementsByTagName("div");
            //         for (var i = 0; i < dvs.length; i++) {
            //             if (dvs[i].id == ('d' + tabid)) dvs[i].style.display = 'block';
            //             else dvs[i].style.display = 'none';
            //         }
        }
    </script>
    </form>
</body>
</html>
