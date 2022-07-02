<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="View-Users.aspx.cs" Inherits="Xispirito.View.AdminOptions.Admin_Options" %>

<asp:Content ID="HeaderFooter" ContentPlaceHolderID="HeaderFooter" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <!DOCTYPE html>

    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Eventos - Xispirito </title>

        <link rel="stylesheet" href="View-Users.css" />
        <link rel="stylesheet" href="print.css" media="print" />

    </head>
    <body>
        <section class="users-list">
            <h3 style="text-align: center; color: white;">Lista de Palestrantes</h3>
            <div class="create-user">
                <asp:ImageButton ID="Return" runat="server" CssClass="back-button" ImageUrl="~/View/Images/Return.png" PostBackUrl="~/View/AdminOptions/AdminOptions.aspx" />
                <div class="filter-search-wrapper">
                    <div class="events-search">
                        <asp:TextBox ID="FilterUsers" runat="server" CssClass="users-filter-input" PlaceHolder="Pesquisar Palestrantes" />
                    </div>
                    <div class="search-button-inline">
                        <asp:ImageButton ID="SearchUsers" runat="server" CssClass="search-button" ImageUrl="~/View/Images/Search.png" OnClick="SearchUsers_Click" />
                    </div>
                </div>
                <asp:Button ID="CreateUser" runat="server" CssClass="create-user-button" Text="Cadastrar Palestrante" PostBackUrl="~/View/AdminOptions/Users/Create-Speaker/Create-Speaker.aspx" />
            </div>
            <table class="users-list-table">
                <tr>
                    <th>Nome</th>
                    <th>Email</th>
                    <th>Ações</th>
                </tr>
                <asp:ListView ID="ListViewUsers" runat="server" OnItemDataBound="ListViewUsers_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="UserName" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="UserEmail" runat="server" />
                            </td>
                            <td>
                                <div class="buttons-actions">
                                    <%--<asp:Button ID="EditUser" runat="server" CssClass="button-edit" Text="Editar" />--%>
                                    <asp:Button ID="DeleteUser" runat="server" CssClass="button-delete" Text="Excluir" OnClick="DeleteUser_Click" />
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </table>
        </section>
    </body>
    </html>
</asp:Content>
