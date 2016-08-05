<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/tanklogger.Master" CodeBehind="customers.aspx.vb" Inherits="tanklogger.customers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <table style="width:100%;">
            <tr>
                <td class="HeaderBars">
                    <strong class="VertMid">Waterminder Customer List</strong>&nbsp
                </td>
            </tr>
            <tr>
                <td  style="width:100%;">
                    <asp:GridView ID="grdCustomers" runat="server" AllowPaging="true" pagesize="20" GridLines="None" 
                    AutoGenerateColumns="False"  BorderWidth="0px"  Width="100%" 
                    DataKeyNames="custID"  CssClass="TableDetails">
                    <HeaderStyle CssClass="TableHeader" HorizontalAlign="Left" ></HeaderStyle>
                    <Columns>
                        <asp:ButtonField CommandName="cmdSelected" HeaderText="Name" DataTextField="custName" HeaderStyle-HorizontalAlign="left"><ItemStyle  ForeColor="#003366" HorizontalAlign="left"  /></asp:ButtonField>
                        <asp:BoundField DataField="custContact" HeaderText="Contact" HeaderStyle-HorizontalAlign="left" ><ItemStyle HorizontalAlign="left"  /></asp:BoundField>
                        <asp:BoundField DataField="custStatus" HeaderText="Status" HeaderStyle-HorizontalAlign="left" ><ItemStyle HorizontalAlign="left"  /></asp:BoundField>
                        </Columns>
                        <PagerStyle CssClass="PagerLink" HorizontalAlign="left" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <asp:Button ID="btnAdd" runat="server" Text="New" />
</asp:Content>

