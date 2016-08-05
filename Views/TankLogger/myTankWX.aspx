<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="myTankWX.aspx.vb" Inherits="tanklogger.myTankWX" %>
<%@ Register assembly="MjcWeather" namespace="Mjc.Web.UI" tagprefix="MJC" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="formstyle" style="width:100%; text-align:center;">
            <tr>
                <td  class="HeaderBars">
                    <asp:Label ID="lblTankID" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblTankName" runat="server" Text="My Tank"></asp:Label>&nbsp Weather Forecast
                </td>
            </tr>
            <tr>
                <td style="width:100%; text-align:center;">
                    <MJC:Weather ID="Weather1" runat="server" 
                         PartnerId="1204114473" LicenseKey="22ba097bf82deed0"/>
                    </td>
                </tr>
            </table>
    </div>
    </form>
</body>
</html>
