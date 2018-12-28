
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main_wc.aspx.cs" Inherits="ColoPay.Web.Admin.main_wc" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>云商框架</title>
    <link href="/admin/css/admin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="admincenter">
        <div class="admintitle">
            <div class="sj">
                <img src="images/zao.gif" width="34" height="25" /></div>
            <span><strong style="font-size: 14px;"><%=CurrentUserName%> <%=GetDateTime%></strong> <a href="Accounts/userinfo.aspx" target="mainFrame">
                账号设置</a></span></div>
    </div>
    <div class="main_main">
        <div class="sj">
            <img src="images/icon5.gif" width="22" height="20" /></div>
        <span class="main_time">您最后一次登录的时间：<asp:Literal ID="LitLastLoginTime" runat="server"></asp:Literal>
            (不是您登录的？<a href="Logout.aspx">请点这里</a>)</span></div>
    <div class="main_line">
    </div>
    <div class="main_iconmenu" style="line-height:normal">
        <span><a href="SysManage/WebSiteConfig.aspx"  target="mainFrame"><img src="images/mian_webSit.png" width="48" height="48" /><br /> 网站设置 </a></span>
          <span><a href="WeChat/User/RequestMsg.aspx" target="mainFrame"><img src="images/main_contentManage.png" width="48" height="48" /><br />消息管理</a></span> 
         <span><a href="WeChat/User/UserList.aspx" target="mainFrame"><img src="images/main_userManage.png" width="48" height="48" /><br />用户管理</a></span> 
         <span><a href="WeChat/PostMsg/RuleList.aspx" target="mainFrame"><img src="images/icon_4.gif"  width="48" height="48"/><br />智能客服</a></span> 
         <span><a href="WeChat/Command/CommandList.aspx" target="mainFrame"><img src="images/main_CommentManage.png"  width="48" height="48"/><br />导航菜单</a></span> 

    </div>
    <div class="main_tj" style="display:none" >
        <a href="sysmanage/treefavorite.aspx"  target="mainFrame">新增新的快捷功能</a></div>
    <div class="admintitle adminxia" style="margin-top: 10px" >
        <div class="sj" style="margin-right: 20px;">
            <img src="images/icon6.gif" width="21" height="28" /></div>
        <strong>帐号设置</strong></div>
    <div class="main_bottomzi">
         <ul>
            <li>您可以快速绑定系统微博，进行微博营销</li>
            <li><a href="Accounts/UserBind.aspx">微博绑定</a></li>
        </ul>
        <ul>
            <li>您可以快速设置移动相关配置信息</li>
            <li>
                <li><a href="WeChat/Setting/Config.aspx">移动功能配置</a></li>
            </li>
        </ul>
             <ul>
            <li>您可以快速设置系统所需要的API接口相关信息</li>
            <li><a href="Settings/APIConfig.aspx">API接口设置</a></li></ul>
        <ul>
            <li>您可以快速清除缓存，及时更新缓存数据</li>
            <li><a href="sysManage/ClearCache.aspx">清除缓存数据</a></li>
        </ul>
        <ul>
            <li>您的微信公众平台帐号URL：</li>
            <li>
            <strong style="font-size: 14px"><asp:Label ID="lblUrl" runat="server" Text=""></asp:Label> </strong>
            </li>
        </ul>
        
    </div>
    <div class="main_main" style="display: none;">
        <span class="main_time">域名：<asp:Literal ID="litServerDomain" runat="server"></asp:Literal></span>
    </div>
    <div class="admintitle adminxia" >
        <div class="sj" style="margin-right: 20px;">
            <img src="images/icon_3.gif" width="21" height="28" /></div>
    <strong>系统环境</strong></div>
    <div class="main_bottomzi systeminfo">
        <ul>
            <li>程序版本：</li>
            <li><asp:Literal ID="litProductLine" runat="server"></asp:Literal></li>
        </ul>
        <ul>
            <li>操作系统：</li>
            <li><asp:Literal ID="litOperatingSystem" runat="server"></asp:Literal></li>
        </ul>
        <ul>
            <li>服务器IIS：</li>
            <li><asp:Literal ID="litWebServerVersion" runat="server"></asp:Literal></li>
        </ul>
        <ul>
            <li>.NET框架：</li>
            <li><asp:Literal ID="litDotNetVersion" runat="server"></asp:Literal></li>
        </ul>
    </div>
    <div class="main_line_1">
    </div>
    </form>
</body>
</html>

