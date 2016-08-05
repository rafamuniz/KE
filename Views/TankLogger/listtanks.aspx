<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/tanklogger.Master" CodeBehind="listtanks.aspx.vb" Inherits="tanklogger.listtanks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:PlaceHolder id="MetaPlaceHolder" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table style="width:100%;">
            <tr>
                <td class="HeaderBars">
                    <strong class="VertMid">Waterminder Tank Inventory Overview</strong>&nbsp
                    <asp:DropDownList ID="ddSiteList" runat="server" CssClass="VertMid" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td  style="width:100%;">
                    <asp:GridView ID="grdTanks" runat="server" AllowPaging="true" pagesize="20" GridLines="None" 
                    AutoGenerateColumns="False"  BorderWidth="0px"  Width="100%" 
                    DataKeyNames="tdefID"  CssClass="TableDetails">
                    <HeaderStyle CssClass="TableHeader" HorizontalAlign="Left" ></HeaderStyle>
                    <Columns>
                        <asp:ButtonField CommandName="cmdSelected" HeaderText="Name" DataTextField="tdefName" HeaderStyle-HorizontalAlign="left"><ItemStyle  ForeColor="#003366" HorizontalAlign="left"  /></asp:ButtonField>
                        <asp:BoundField DataField="Volume_Remaining_Gal" HeaderText="Gals" HeaderStyle-HorizontalAlign="left" ><ItemStyle HorizontalAlign="left"  /></asp:BoundField>
                        <asp:BoundField DataField="PctFull" HeaderText="%Full" HeaderStyle-HorizontalAlign="left" ><ItemStyle HorizontalAlign="left"  /></asp:BoundField>
                        <asp:BoundField DataField="tdATemp" HeaderText="AmbTemp" HeaderStyle-HorizontalAlign="left" ><ItemStyle HorizontalAlign="left"  /></asp:BoundField>
                        <asp:BoundField DataField="tdWTemp" HeaderText="WaterTemp" HeaderStyle-HorizontalAlign="left" ><ItemStyle HorizontalAlign="left"  /></asp:BoundField>
                        <asp:BoundField DataField="calcAlarm" HeaderText="Alarms" HeaderStyle-HorizontalAlign="left" ><ItemStyle HorizontalAlign="left"  /></asp:BoundField>
                        <asp:BoundField DataField="tdDateTime" HeaderText="Last Measurement" HeaderStyle-HorizontalAlign="left" ><ItemStyle HorizontalAlign="left"  /></asp:BoundField>
                        </Columns>
                        <PagerStyle CssClass="PagerLink" HorizontalAlign="left" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnAdd" runat="server" Text="New" />
    </div>
</asp:Content>
