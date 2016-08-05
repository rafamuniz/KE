<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/tanklogger.Master" CodeBehind="site.aspx.vb" Inherits="tanklogger.site" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div  style="width:100%;" class="auto-style4">Site Editor<asp:Label ID="lblSite" runat="server" Visible="false"></asp:Label></div><br />
        <table class="formstyle">
            <tr>
                <td>                    
                    <table style="width:100%;">
                        <tr>
                            <td >Site Name</td>
                            <td ><asp:TextBox ID="tbName" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td >Site Customer Number</td>
                            <td ><asp:TextBox ID="tbCustID" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td >Site SPOT GPS ID</td>
                            <td ><asp:TextBox ID="txtSPOT_ID" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td >Site Sensor Master Code</td>
                            <td ><asp:TextBox ID="tbSiteCode" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td >Site Contact</td>
                            <td ><asp:TextBox ID="tbContact" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td >Address</td>
                            <td ><asp:TextBox ID="tbAddr1" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td ></td>
                            <td ><asp:TextBox ID="tbAddr2" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td >City</td>
                            <td ><asp:TextBox ID="tbCity" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td >State</td>
                            <td ><asp:DropDownList ID="ddlState" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td >Zip</td>
                            <td ><asp:TextBox ID="tbZip" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td >Contact Phone</td>
                            <td ><asp:TextBox ID="tbPhone" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td >Contact Email</td>
                            <td ><asp:TextBox ID="tbemail" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td >Status</td>
                            <td >
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Value="A">Active</asp:ListItem>
                                    <asp:ListItem Value="I">Inactive</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table style="width:100%;">
                        <tr>
                            <td style="width:33%; text-align:center;"><asp:Button ID="btnDelete" runat="server" Text="Delete" /></td>
                            <td style="width:33%; text-align:center;"><asp:Button ID="btnSave" runat="server" Text="Save" /></td>
                            <td style="width:33%; text-align:center;"><asp:Button ID="btnCancel" runat="server" Text="Cancel" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
