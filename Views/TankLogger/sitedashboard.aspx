<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="sitedashboard.aspx.vb" Inherits="tanklogger.sitedashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <asp:PlaceHolder id="MetaPlaceHolder" runat="server" />
    <link href="StyleSheet1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table  class="formstyle" style="width:100%; vertical-align:middle;">
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td style="width:95%; text-align:center;">
                                <img alt="" width="600" src="images/logoheader1.png" />
                            </td>
                            <td style="width:5%">
                                <asp:DropDownList ID="ddMenu" runat="server" AutoPostBack="true">
                                    <asp:ListItem Selected="True" Value="Menu"></asp:ListItem>
                                    <asp:ListItem Value="Main"></asp:ListItem>
                                    <asp:ListItem Value="Settings"></asp:ListItem>
                                    <asp:ListItem Value="Logoff"></asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Datalist ID="dlistTanks" runat="server" 
                        Width="100%"
                        DataKeyNames="tdefID">
                        <ItemTemplate>
                            <table style="width:100%; border:solid;">
                                <tr>
                                    <td style="width:10%; text-align:center; vertical-align:middle;">
                                        <asp:Label ID="lblTankID" runat="server" Text="<%# bind('tdefID') %>"></asp:Label>
                                    </td>
                                    <td style="width:60%;">
                                        <asp:HiddenField ID="hfTankID" runat="server" Value="<%# bind('tdSensorID') %>" />
                                        <asp:Chart ID="RangeChart" runat="server" Height="200px" Width="700px">
                                            <Series>
                                                <asp:Series Name="RangeSeries" ChartType="Line" ></asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="RangeArea1"></asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>    
                                    </td>
                                    <td style="width:20%; text-align:center;  vertical-align:middle;
                                               background-image:url('<%# Eval ( "calcStateImage", "{0}" ) %>'); 
                                               background-size:200px; background-repeat:no-repeat;
                                               background-position:center;">
                                    <td style="width:10%; vertical-align:middle;">
                                        <asp:ImageButton ID="imgAlarm" runat="server" 
                                                         width="50" ImageUrl="images/warning.png" 
                                                         visible="<%# bind('calcAlarmBool') %>"
                                                         CommandName="tankAlarm" CommandArgument="<%# bind('tdefID') %>"/>
                                        <br />
                                        <asp:Label ID="Label3" runat="server" Text="<%# bind('PctFull') %>"></asp:Label>&nbsp%
                                        <br />
                                        <asp:Label ID="Label2" runat="server" Text="<%# bind('Volume_Remaining_Gal') %>"></asp:Label>&nbspgals.
                                        <br />
                                        <asp:Label ID="Label4" runat="server" Text="<%# bind('tdDateTime') %>"></asp:Label>                                    </td>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Datalist>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
