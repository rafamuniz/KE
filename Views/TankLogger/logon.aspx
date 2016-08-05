<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="logon.aspx.vb" Inherits="tanklogger.logon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <link href="StyleSheet1.css" rel="stylesheet" type="text/css" /></head>
<body class="bodystyle" >
    <form class="formstyle" id="form1" runat="server">
    <div  class="formstyle">
        <table class="splashstyle" style="width:50%; margin:auto;" >
            <tr>
                <td style="text-align:center;"><img alt="" width="350" src="images/logo1.png" /></td>
            </tr>
            <tr>
                <td>
                    <table class="formstyle" style="width:50%; text-align:left;">
                    <tr>
                        <td>Email:</td>
                        <td><asp:TextBox ID="tbEmail" runat="server" TextMode="SingleLine"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>Password:</td>
                        <td><asp:TextBox ID="tbPwd" runat="server"   TextMode="Password"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align:left;"><asp:Button ID="btnLogin" runat="server" Text="Login" /></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:center;"><asp:label ID="lblMessage" runat="server" /></td>
                    </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
