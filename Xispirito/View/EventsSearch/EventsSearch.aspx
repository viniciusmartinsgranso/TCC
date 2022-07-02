<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="EventsSearch.aspx.cs" Inherits="Xispirito.View.EventsSearch.EventsSearch" %>

<asp:Content ID="HeaderFooter" ContentPlaceHolderID="HeaderFooter" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Todos os Eventos - Xispirito </title>

        <link rel="stylesheet" href="EventsSearch.css" />
    </head>
    <body>
        <main>
            <div class="all-events-content">
                <section class="events find-events">
                    <div class="event-form-body">
                        <div class="event-form">
                            <div class="event-div-first-line">
                                <div class="event-div-first-line-search">
                                    <asp:TextBox ID="EventSearch" runat="server" class="event-input-first-line-search-input" placeholder="Pesquisar por eventos" />
                                </div>
                                <div class="event-div-first-line-button">
                                    <asp:ImageButton ID="EventSearchImage" runat="server" class="event-input-first-line-search-button" ImageUrl="~/View/Images/Search.png" OnClick="EventSearchImage_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <section class="cards">
                        <asp:ListView ID="ListViewAllEvents" runat="server" OnItemDataBound="ListViewAllEvents_ItemDataBound">
                            <ItemTemplate>
                                <div class="card">
                                    <div class="card-image">
                                        <asp:ImageButton ID="AllEventsImage" runat="server" AlternateText="Imagem não encontrada" CssClass="image" />
                                    </div>

                                    <div class="card-title">
                                        <asp:Label ID="TitleAllEvents" runat="server" class="title text--medium"> </asp:Label>
                                    </div>

                                    <div class="content">
                                        <div class="info">
                                            <asp:Label ID="TypeAllEvents" runat="server" class="type text--medium"></asp:Label>
                                            <asp:Label ID="TimeAllEvents" runat="server" class="time text--medium"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </section>
                </section>
            </div>
        </main>
    </body>
    </html>
</asp:Content>
