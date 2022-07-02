<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registry.aspx.cs" Inherits="Xispirito.View.Registry.Registry" %>

<asp:Content ID="HeaderFooter" ContentPlaceHolderID="HeaderFooter" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Evento - Xispirito </title>

        <link rel="stylesheet" href="Registry.css" />
        <link rel="stylesheet" href="print.css" media="print" />
    </head>

    <body>
        <div class="background-image">
            <asp:Image ID="BackgroundEventImage" runat="server" AlternateText="Imagem não encontrada" />
        </div>
        <section class="event">
            <div class="form-body">
                <div class="event-image">
                    <asp:Image ID="EventImage" runat="server" AlternateText="Imagem não encontrada" />
                </div>
            </div>

            <div class="form-body">
                <div class="event-information">
                    <div class="wrapper-title-time-type">
                        <div class="event-title">
                            <asp:Label ID="EventTitle" runat="server" class="title-text"></asp:Label>
                        </div>

                        <div class="wrapper-type-address">
                            <div class="event-type">
                                <asp:Button ID="EventType" runat="server" class="type-text" />
                            </div>

                            <div class="event-address">
                                <asp:Label ID="EventAddress" runat="server" Text="address-text"></asp:Label>
                            </div>
                        </div>

                        <div class="event-time">
                            <asp:Label ID="EventTime" runat="server" class="time-text"></asp:Label>
                        </div>
                    </div>
                    <div class="wrapper-registry">
                        <asp:Button ID="EventSubscribe" runat="server" class="registry-button" Text="Inscrever-se" OnClick="Subscribe_Click" />
                    </div>
                </div>
            </div>

            <div class="form-body">
                <div class="event-description">
                    <div class="event-description-title">
                        <asp:Label ID="EventDescriptionTitle" runat="server" class="description-title-text" Text="Sobre a Palestra"></asp:Label>
                    </div>
                    <div class="event-description-text">
                        <asp:Label ID="EventDescription" runat="server" class="description-text"></asp:Label>
                    </div>
                </div>
            </div>
        </section>
    </body>
    </html>
</asp:Content>
