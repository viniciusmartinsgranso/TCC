<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WatchLecture.aspx.cs" Inherits="Xispirito.View.Lectures.ViewLectures.WatchLecture" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Assistir Palestra - Xispirito</title>

    <link rel="stylesheet" href="WatchLecture.css" />
</head>
<body>
    <form id="frmWatchLecture" runat="server">
        <section class="lecture-view">
            <div class="action">
                <div class="lecture-view-button" onclick="menuToggle();">
                    <asp:Image ID="Menu" runat="server" CssClass="lecture-view-button-options" ImageUrl="~/View/Images/Menu.png" />
                </div>
                <div class="menu">
                    <div class="menu-wrapper">
                        <div class="list-users">
                            <asp:Image ID="UserList" runat="server" ImageUrl="~/View/Images/UserList.png" CssClass="list-image" />
                        </div>
                        <%--<div class="admin-panel">
                            <asp:Image ID="AdministratorPanel" runat="server" ImageUrl="~/View/Images/AdminOptions.png" CssClass="admin-image" />
                        </div>--%>
                    </div>

                    <div class="users-panel">
                        <div class="list-title-wrapper">
                            <asp:Label ID="ListTitle" runat="server" Text="Lista de Usuários" CssClass="list-title" />
                        </div>
                        <asp:ListView ID="UsersListView" runat="server" OnItemDataBound="UsersListView_ItemDataBound">
                            <ItemTemplate>
                                <div class="name-function-wrapper">
                                    <asp:Image ID="UserImage" runat="server" CssClass="user-image" />
                                    <asp:Label ID="UserName" runat="server" Text="Nome" CssClass="user-name" />
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>

            <%--<h3 style="text-align: center; color: white;">Assistir Palestra</h3>
            <div class="back-button-line">
                <button class="back-button">Sair da Palestra</button>
            </div>--%>

            <div class="lecture-view-iframe">
                <iframe #iframe src="https://www.youtube.com/embed/6Qq2OMFh8Pc" width="100%" height="100%" allowfullscreen></iframe>
            </div>
        </section>

        <script>
            function menuToggle() {
                const toggleMenu = document.querySelector('.menu');
                toggleMenu.classList.toggle('active');

                const toggleButton = document.querySelector('.lecture-view-button');
                toggleButton.classList.toggle('active');
            }
        </script>
    </form>
</body>
</html>
