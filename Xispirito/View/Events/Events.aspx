<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Events.aspx.cs" Inherits="Xispirito.View.Events.Events" %>

<asp:Content ID="HeaderFooter" ContentPlaceHolderID="HeaderFooter" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <!DOCTYPE html>

    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Eventos - Xispirito </title>

        <link rel="stylesheet" href="Events.css" />
        <link rel="stylesheet" href="print.css" media="print" />

        <link rel="stylesheet" href="https://unpkg.com/swiper/swiper-bundle.min.css" />
        <script src="https://unpkg.com/swiper/swiper-bundle.js"></script>
        <script src="https://unpkg.com/swiper/swiper-bundle.min.js"></script>
    </head>
    <body>
        <section class="events-header">
            <div class="events-h1">
                <h1>Gerencie eventos</h1>
            </div>
            <div class="events-h3">
                <h3>Confira os eventos que vão ocorrer na Athon Descubra eventos de seu interesse ou crie os seus próprios utilizando a Xispirito
                </h3>
            </div>
        </section>
        <section class="events find-events">
            <div class="event-form-body">
                <div class="event-form">
                    <div class="event-div-first-line">
                        <div class="event-div-first-line-search">
                            <asp:TextBox ID="EventSearch" runat="server" class="event-input-first-line-search-input" placeholder="Pesquisar por Eventos" />
                        </div>
                        <div class="event-div-first-line-button">
                            <asp:ImageButton ID="EventSearchImage" runat="server" class="event-input-first-line-search-button" ImageUrl="~/View/Images/Search.png" OnClick="EventSearchImage_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="events-list">
                <div class="events-upcoming">
                    <div class="events-list-title">
                        <h3 class="events-list-title-h3">Próximos Eventos</h3>
                    </div>
                    <section class="section">
                        <div class="recent-carousel">
                            <div class="swiper">
                                <div class="swiper-wrapper">
                                    <asp:ListView ID="ListViewUpcomingEvents" runat="server" OnItemDataBound="ListViewEvents_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="swiper-slide">
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
                                            </div>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>

                <div class="events-area">
                    <div class="events-list-title">
                        <h3 class="events-list-title-h3">Eventos na sua Área</h3>
                    </div>
                    <section class="section">
                        <div class="area-carousel">
                            <div class="swiper">
                                <div class="swiper-wrapper">
                                    <asp:ListView ID="ListViewAreaEvents" runat="server" OnItemDataBound="ListViewAreaEvents_ItemDataBound">
                                        <ItemTemplate>
                                            <div class="swiper-slide">
                                                <div class="card">
                                                    <div class="card-image">
                                                        <asp:ImageButton ID="AreaEventImage" runat="server" AlternateText="Imagem não encontrada" CssClass="image" />
                                                    </div>

                                                    <div class="card-title">
                                                        <asp:Label ID="TitleArea" runat="server" class="title text--medium"> </asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>
            </div>

            <div class="more-events">
                <asp:Button ID="AllEvents" runat="server" Text="Ver todos os Eventos" CssClass="button-all-events" PostBackUrl="~/View/EventsSearch/EventsSearch.aspx" />
            </div>
        </section>

        <script>
            var swiper = new Swiper('.swiper', {
                slidesPerView: 5.5,
                spaceBetween: 20,
                slidesPerGroup: 1,
                loop: false,
                loopFillGroupWithBlank: true,
                pagination: {
                    el: '.swiper-pagination',
                    clickable: true,
                },
            });
        </script>
    </body>
    </html>
</asp:Content>
