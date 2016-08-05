<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/tanklogger.Master" CodeBehind="ModelPick.aspx.vb" Inherits="tanklogger.ModelPick" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:DataList ID="dlModels" DataKeyField="tmid" OnItemCommand="dlModels_ItemCommand" runat="server" 
        RepeatColumns="3" RepeatDirection="Horizontal"
        ItemStyle-BorderStyle="Solid" ItemStyle-BorderColor="Black"  ItemStyle-BorderWidth="1" >
        <ItemTemplate>
            <table class="formstyle" style="text-align:center;">
                <tr>
                    <td>
                        <asp:ImageButton ID="imgChosen" CommandName="cmdImgChoice"  runat="server" ImageUrl='<%# Eval("tmimage")%>' Height="150px" /><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblModelID" runat="server" Text='<%# Eval("tmid")%>'></asp:Label><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblModelDesc" runat="server" Text='<%# Eval("tmdesc")%>'></asp:Label><br />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>
