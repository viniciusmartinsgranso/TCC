<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="RegisteredEvents.aspx.cs" Inherits="Xispirito.View.RegisteredEvents.RegisteredEvents" %>

<asp:Content ID="HeaderFooter" ContentPlaceHolderID="HeaderFooter" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <!DOCTYPE html>

    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Palestras Inscritas - Xispirito </title>

        <link rel="stylesheet" href="RegisteredEvents.css" />
    </head>
    <body>
        <section class="hero">
            <div class="container">
                <div class="title-search-wrapper">
                    <div class="title">
                        <asp:Label ID="MyEvents" runat="server" CssClass="header-title" />
                    </div>
                    <div class="filter-search-wrapper">
                        <div class="events-search">
                            <asp:TextBox ID="FilterEvents" runat="server" CssClass="search-box" PlaceHolder="Digite o Nome do Evento para Filtrar" />
                        </div>
                        <div class="search-button-inline">
                            <asp:ImageButton ID="SearchEvents" runat="server" class="search-button" ImageUrl="~/View/Images/BlackSearch.png" OnClick="SearchEvents_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <main>
            <section class="events">
                <asp:ListView ID="ListViewEvents" runat="server" OnItemDataBound="ListViewEvents_ItemDataBound">
                    <ItemTemplate>
                        <div class="card">
                            <div class="left-div">
                                <div class="event-image">
                                    <asp:ImageButton ID="EventImage" runat="server" AlternateText="Imagem não encontrada" CssClass="event-preview" />
                                </div>

                                <div class="event-title-modality-date-wrapper">
                                    <div class="event-title">
                                        <asp:Label ID="EventTitle" runat="server" class="title text--big" Text="Titulo" />
                                    </div>
                                    <div class="modality-address-wrapper">
                                        <div class="event-modality">
                                            <asp:Label ID="EventModality" runat="server" class="modality text--big" Text="Modalidade" />
                                        </div>
                                        <div class="icon-address-wrapper">
                                            <div class="event-address">
                                                <asp:Image ID="AddressIcon" runat="server" class="address-image" ImageUrl="~/View/Images/Map.png" />
                                                <asp:Label ID="EventAddress" runat="server" class="address text--medium" Text="Endereço" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="icon-date-wrapper">
                                        <div class="event-date">
                                            <asp:Image ID="DateIcon" runat="server" class="date-image" ImageUrl="~/View/Images/RegisteredEvents.png" />
                                            <asp:Label ID="EventDate" runat="server" class="date text--medium" Text="Data" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="right-div">
                                <div class="watch-lecture">
                                    <asp:Button ID="WatchLecture" runat="server" Text="Assistir Palestra" CssClass="watch-button" Visible="False" />
                                </div>
                                <div class="cancel-subscription">
                                    <asp:Button ID="CancelSubscription" runat="server" Text="Cancelar Inscrição" CssClass="cancel-button" OnClick="CancelSubscription_Click" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </section>
        </main>
    </body>
    </html>
</asp:Content>
