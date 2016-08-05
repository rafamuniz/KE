<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/tanklogger.Master" CodeBehind="users.aspx.vb" Inherits="tanklogger.users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <table style="width:100%;">
            <tr>
                <td class="HeaderBars">
                    <strong class="VertMid">Waterminder User List for:</strong>&nbsp
                    <asp:DropDownList ID="ddCustList" runat="server" CssClass="VertMid" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td  style="width:100%;">
                    <asp:GridView ID="grdUsers" runat="server" AllowPaging="true" pagesize="20" GridLines="None" 
                    AutoGenerateColumns="False"  BorderWidth="0px"  Width="100%" 
                    DataKeyNames="usrID"  CssClass="TableDetails">
                    <HeaderStyle CssClass="TableHeader" HorizontalAlign="Left" ></HeaderStyle>
                    <Columns>
                        <asp:ButtonField CommandName="cmdSelected" HeaderText="Name" DataTextField="usrName" HeaderStyle-HorizontalAlign="left"><ItemStyle  ForeColor="#003366" HorizontalAlign="left"  /></asp:ButtonField>
                        <asp:BoundField DataField="usrEmail" HeaderText="Email" HeaderStyle-HorizontalAlign="left" ><ItemStyle HorizontalAlign="left"  /></asp:BoundField>
                        <asp:BoundField DataField="usrRole" HeaderText="Role" HeaderStyle-HorizontalAlign="left" ><ItemStyle HorizontalAlign="left"  /></asp:BoundField>
                        </Columns>
                        <PagerStyle CssClass="PagerLink" HorizontalAlign="left" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnAdd" runat="server" Text="New" />
</asp:Content>

