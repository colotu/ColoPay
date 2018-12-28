<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="upgradepackage.aspx.cs" Inherits="YSWL.Web.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    
    <p>
      &#12288;旧文件夹 ： <asp:TextBox ID="txtoldFolder" runat="server" Width="600px"></asp:TextBox>
    </p>
    <p>
       &#12288;新文件夹 ： <asp:TextBox ID="txtnewFolder" runat="server"  Width="600px"></asp:TextBox>
    </p>
    <p>
       目标文件夹 ： <asp:TextBox ID="txttargetFolder" runat="server"  Width="600px"></asp:TextBox>
    </p>
    <p>
    <asp:Button ID="butSave" runat="server" Text="生成升级包" onclick="butSave_Click" />
    </p>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Label" ></asp:Label>
    </div>
    </form>
</body>
</html>
