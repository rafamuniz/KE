<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/tanklogger.Master" CodeBehind="tanklogi.aspx.vb" Inherits="tanklogger.tanklogi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script>
        window.onload = function () {
            var x = document.getElementById('<%= tbLoc.ClientID%>');
            var options = {
                enableHighAccuracy: true,
                timeout: 5000,
                maximumAge: 0
            };

            function success(position) {
                x.value =  position.coords.latitude + "," + position.coords.longitude;
            }

            function error(err) {
                x.value =   err.code + ':' + err.message;
            };

            if (navigator.geolocation) {
                x.value =  navigator.geolocation.getCurrentPosition(success, error, options);
            } else {
                x.value =  "No Geo this browser.";
            };
        };

    </script>

    <div>
        <table>
            <tr>
                <td>
                    Site ID:
                </td>
                <td>
                    <asp:TextBox id="tbSiteID" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Tank ID:
                </td>
                <td>
                    <asp:TextBox id="tbTankID" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Date/Time:
                </td>
                <td>
                    <asp:TextBox id="tbDateTime" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="ddRType" runat="server">
                        <asp:ListItem Text="Distance" Value="R"></asp:ListItem>
                        <asp:ListItem Text="Depth" Value="D"></asp:ListItem>
                        <asp:ListItem Text="Volume" Value="G"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox id="tbDepth" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Temp:
                </td>
                <td>
                    <asp:TextBox id="tbTemp" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Location:
                </td>
                <td>
                    <asp:TextBox id="tbLoc" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="submit" runat="server" text="submit"/>
                </td>
            </tr>
        </table>

        <asp:TextBox id="tbSample" runat="server"></asp:TextBox>
    
    </div>
</asp:Content>
