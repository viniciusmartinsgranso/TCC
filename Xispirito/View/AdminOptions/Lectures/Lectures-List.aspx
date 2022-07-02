<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Lectures-List.aspx.cs" Inherits="Xispirito.View.AdminOptions.Lectures.Lectures_List" %>

<asp:Content ID="HeaderFooter" ContentPlaceHolderID="HeaderFooter" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <!DOCTYPE html>

    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Eventos - Xispirito </title>

        <link rel="stylesheet" href="Lectures-List.css" />
        <link rel="stylesheet" href="print.css" media="print" />

    </head>
    <body>
        <section class="lectures-list">
            <h3 style="text-align: center; color: white;">Lista de Palestras</h3>
            <div class="create-lecture">
                <asp:ImageButton ID="Return" runat="server" CssClass="back-button" ImageUrl="~/View/Images/Return.png" PostBackUrl="~/View/AdminOptions/AdminOptions.aspx" />
                <div class="filter-search-wrapper">
                    <div class="events-search">
                        <asp:TextBox ID="FilterEvents" runat="server" CssClass="lectures-filter-input" PlaceHolder="Pesquisar Palestras" />
                    </div>
                    <div class="search-button-inline">
                        <asp:ImageButton ID="SearchEvents" runat="server" CssClass="search-button" ImageUrl="~/View/Images/Search.png" OnClick="SearchEvents_Click" />
                    </div>
                </div>
                <asp:Button ID="CreateLecture" runat="server" CssClass="create-lecture-button" Text="Cadastrar Palestra" PostBackUrl="~/View/Lectures/CRUD/Lecture-CRUD.aspx" />
            </div>
            <table class="lectures-list-table">
                <tr>
                    <th>Nome</th>
                    <th>Data</th>
                    <th>Modalidade</th>
                    <th>Ações</th>
                </tr>

                <asp:ListView ID="ListViewLectures" runat="server" OnItemDataBound="ListViewLectures_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="LectureName" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="LectureDate" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="LectureModality" runat="server" />
                            </td>
                            <td>
                                <div class="buttons-actions">
                                    <asp:Button ID="EditLecture" runat="server" CssClass="button-edit" Text="Editar" />
                                    <asp:Button ID="DeleteLecture" runat="server" CssClass="button-delete" Text="Excluir" OnClick="DeleteLecture_Click" />
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
