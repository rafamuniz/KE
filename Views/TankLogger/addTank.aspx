<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/tanklogger.Master" CodeBehind="addTank.aspx.vb" Inherits="tanklogger.addTank" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div  style="width:100%;" class="auto-style4">Tank Editor
            <asp:Label ID="tbModel" runat="server" Visible="false"></asp:Label>
            <asp:Label ID="tbDefID" runat="server" Visible="false"></asp:Label>
        </div><br />
        <table class="formstyle">
            <tr>
                <td style="width:40%;">                    
                    
                    <table style="width:100%;">
                        <tr>
                            <td >Company</td>
                            <td ><asp:DropDownList ID="ddlCompany" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td >Site</td>
                            <td ><asp:DropDownList ID="ddlSite" runat="server"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td >Tank Name</td>
                            <td ><asp:TextBox ID="tbName" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td >Description</td>
                            <td ><asp:TextBox ID="tbDesc" runat="server" TextMode="MultiLine" Height="100px"></asp:TextBox></td>
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
                            <td >Site Contact</td>
                            <td ><asp:TextBox ID="tbContact" runat="server"></asp:TextBox></td>
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
                            <td >Sensor</td>
                            <td ><asp:TextBox ID="tbLogID" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td >Status</td>
                            <td >
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Value="A">Active</asp:ListItem>
                                    <asp:ListItem Value="I">Inactive</asp:ListItem>
                                </asp:DropDownList>
                                <br />
&nbsp;</td>
                        </tr>
                        <tr>
                            <td >&nbsp;</td>
                            <td style="text-align: center; background-color: #CCCCCC; font-weight: bold"  >SPOT GPS</td>
                        </tr>
                        <tr>
                            <td >SPOT ID</td>
                            <td ><asp:TextBox ID="txtSPOT_ID" runat="server"></asp:TextBox>&nbsp;
                                <asp:Button ID="cmdFind" runat="server" Text="Find SPOT" />
                            </td>
                        </tr>
                        <tr>
                            <td >Name</td>
                            <td >
                                <asp:Label ID="lblSPOT_Name" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >Battery</td>
                            <td >
                                <asp:Label ID="lblSPOT_Battery" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >Last Contact</td>
                            <td >
                                <asp:Label ID="lblSPOT_Last" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >Last Location</td>
                            <td >
                                <asp:Label ID="lblSPOT_Location" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td >Model</td>
                            <td >
                                <asp:Label ID="lblSPOT_Model" runat="server"></asp:Label>
                            </td>
                        </tr>
                        </table>
                </td>
            <td style="width:60%;">   
                <table class="formstyle">
                    <tr>
                        <td colspan="2">
                            <asp:Image id="imgModel" runat="server" width="600px"  />
                        </td>
                    </tr>
                        <tr>
                            <td ><asp:label id="lblDimDesc1" runat="server" Visible="false" Text=""></asp:label></td>
                            <td ><asp:TextBox ID="tbDim1" runat="server" Visible="false" Text="0"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td ><asp:label id="lblDimDesc2" runat="server" Visible="false" Text=""></asp:label></td>
                            <td ><asp:TextBox ID="tbDim2" runat="server" Visible="false" Text="0"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td ><asp:label id="lblDimDesc3" runat="server" Visible="false" Text=""></asp:label></td>
                            <td ><asp:TextBox ID="tbDim3" runat="server" Visible="false" Text="0"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td ><asp:label id="lblDimDesc4" runat="server" Visible="false" Text=""></asp:label></td>
                            <td ><asp:TextBox ID="tbDim4" runat="server"  Visible="false" Text="0"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td ><asp:label id="lblDimDesc5" runat="server" Visible="false" Text=""></asp:label></td>
                            <td ><asp:TextBox ID="tbDim5" runat="server"  Visible="false" Text="0"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td ><asp:label id="Label1" runat="server" Visible="true" Text="Min. Distance"></asp:label></td>
                            <td ><asp:TextBox ID="tbMinDist" runat="server"  Visible="true" ></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td ><asp:label id="Label2" runat="server" Visible="true" Text="Max Distance"></asp:label></td>
                            <td ><asp:TextBox ID="tbMaxDist" runat="server"  Visible="true" ></asp:TextBox></td>
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
