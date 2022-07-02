<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Xispirito.View.HomeWithMaster.Home" %>

<asp:Content ID="HeaderFooter" ContentPlaceHolderID="HeaderFooter" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <!DOCTYPE html>

    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Home - Xispirito </title>

        <link rel="stylesheet" href="Home-responsive.css" media="screen and (max-width: 630px)" />
        <link rel="stylesheet" href="Home.css" />
        <link rel="stylesheet" href="print.css" media="print" />
    </head>
    <body>
        <div class="home-content">
            <section class="hero">
            <div class="container">
                <div>
                    <h2>
                        <b>Cadastrar-se em um evento nunca foi tão simples.
                        </b>
                    </h2>
                    <p>
                        Uma poderosa ferramenta para organização de eventos. Ganhe TEMPO e aumente a PRODUTIVIDADE ao eliminar todo o trabalho manual.
                    </p>
                    <a href="../Events/Events.aspx" class="button">ENCONTRAR UM EVENTO DE GRAÇA</a>
                </div>
                <image src="/View/Images/Presentation.png"></image>
            </div>
        </section>

        <main>
            <section class="cards">
                <asp:ListView ID="ListViewUpcomingEvents" runat="server" OnItemDataBound="ListViewUpcomingEvents_ItemDataBound">
                    <ItemTemplate>
                        <div class="card">
                            <div class="card-image">
                                <asp:ImageButton ID="UpcomingEventImage" runat="server" AlternateText="Imagem não encontrada" CssClass="image" />
                            </div>

                            <div class="card-title">
                                <asp:Label ID="TitleUpcomingEvent" runat="server" class="title text--medium"> </asp:Label>
                            </div>

                            <div class="content">
                                <div class="info">
                                    <asp:Label ID="TypeUpcomingEvent" runat="server" class="type text--medium"></asp:Label>
                                    <asp:Label ID="TimeUpcomingEvent" runat="server" class="time text--medium"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </section>
        </main>
        </div>
    </body>
    </html>
</asp:Content>
