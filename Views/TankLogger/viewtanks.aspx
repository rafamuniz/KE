<%@ Page  Title="" Language="vb" AutoEventWireup="false"  MasterPageFile="~/tanklogger.Master" CodeBehind="viewtanks.aspx.vb" Inherits="tanklogger.viewtanks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <asp:PlaceHolder id="MetaPlaceHolder" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table style="width:100%; margin:auto;">
            <tr>
                <td class="HeaderBars">
                    <strong class="VertMid">Waterminder Tank Inventory Overview</strong>&nbsp
                    <asp:DropDownList ID="ddSiteList" runat="server" CssClass="VertMid" AutoPostBack="true"></asp:DropDownList>&nbsp
                    <asp:RadioButtonList ID="rblView" runat="server"></asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="text-align:center;">
                    <asp:Datalist ID="dlistTanks" runat="server" 
                        RepeatColumns="2" RepeatDirection="Horizontal" 
                        DataKeyNames="tdefID">
                        <ItemTemplate>
                            <table style="border:1px solid black; height:300px; width:600px; margin:auto;">
                                <tr>
                                    <td style="text-align:right; width:33%;">
                                        <asp:ImageButton ID="ImageButton2" runat="server" 
                                                         width="50" ImageUrl="images/warning.png" 
                                                         visible="<%# bind('calcAlarmBool') %>"
                                                         CommandName="tankAlarm" CommandArgument="<%# bind('tdefID') %>"/>
                                    </td>
                                    <td style="text-align:center; width:33%;">
                                        <asp:Label ID="Label1" runat="server" Text="<%# bind('tdefName') %>"></asp:Label><br />
                                        Volume:&nbsp<asp:Label ID="Label4" runat="server" Text="<%# bind('Volume_Gal') %>"></asp:Label>&nbspGallons
                                    </td>
                                    <td style="text-align:left; width:33%;"></td>
                                </tr>
                                <tr>
                                    <td colspan="3"  style="text-align:center;">
                                        <asp:ImageButton ID="ImageButton1" runat="server" 
                                                         width="250" ImageUrl="<%# bind('calcStateImage') %>" 
                                                         CommandName="tankdetail" CommandArgument="<%# bind('tdefID') %>"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align:left; width:33%;">
                                        <asp:Label ID="Label2" runat="server" Text="<%# bind('Volume_Remaining_Gal') %>"></asp:Label>&nbspGals Remaining</td>
                                    <td style="text-align:center; width:33%;"><asp:LinkButton ID="LinkButton1" runat="server" CommandName="tankdetail" CommandArgument="<%# bind('tdefID') %>">Details</asp:LinkButton></td>
                                    <td style="text-align:right; width:33%;"><asp:Label ID="Label3" runat="server" Text="<%# bind('PctFull') %>"></asp:Label>&nbsp%</td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Datalist>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnAdd" runat="server" Text="New" />
    </div>
</asp:Content>
