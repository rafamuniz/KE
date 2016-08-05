<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MyMeter.aspx.vb" Inherits="tanklogger.MyMeter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="position: relative; left: 0; top: 0;">
            <img src="images/FlowMeter/Dial.png" style="position: relative; top: 0; left: 0;"/>
            <img src="images/FlowMeter/ANIM.gif" style="position: absolute; top: 60px; left: 120px;"/>
            <img src="DialIndicator.aspx?flowrate=<%=flowrate %>&indicatormax=140&anglemax=252&image=images/FlowMeter/Red-Needle1.png” style="position: absolute; top: 45px; left: 45px;" />
            <asp:Label runat="server" ID="lblValue" style="position: absolute; top: 215px;  left: 110px; text-align:center; color:dimgray; font-family:Arial; font-size:larger; " ></asp:Label>  
            <asp:Label runat="server" ID="Label1" style="position: absolute; top: 235px;  left: 140px; text-align:center; color:dimgray; font-family:Arial; font-size:larger; " >barrels</asp:Label>  
        </div>
        Flow rate:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
       <br />
        Totalizer:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
         <br />
        <asp:Button ID="Button1" runat="server" Text="Go" />
        <br />
        
    </form>
</body>
</html>
