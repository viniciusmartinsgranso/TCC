<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Certificates.aspx.cs" Inherits="Xispirito.View.Certificates.Viewers.ViewerCertificates" %>

<asp:Content ID="HeaderFooter" ContentPlaceHolderID="HeaderFooter" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <!DOCTYPE html>

    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Certificados - Xispirito </title>

        <link rel="stylesheet" href="Certificates.css" />
    </head>
    <body>
        <section class="hero">
            <div class="container">
                <div class="title-search-wrapper">
                    <div class="title">
                        <asp:Label ID="MyCertificates" runat="server" CssClass="header-title" />
                    </div>
                    <div class="filter-search-wrapper">
                        <div class="certificate-search">
                            <asp:TextBox ID="FilterCertificate" runat="server" CssClass="search-box" PlaceHolder="Digite o Nome do Certificado para Filtrar" />
                        </div>
                        <div class="search-button-inline">
                            <asp:ImageButton ID="SearchCertificate" runat="server" class="search-button" ImageUrl="~/View/Images/BlackSearch.png" OnClick="SearchCertificate_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <main>
            <section class="certificates">
                <asp:ListView ID="ListViewCertificates" runat="server" OnItemDataBound="ListViewCertificates_ItemDataBound">
                    <ItemTemplate>
                        <div class="card">
                            <div class="left-div">
                                <div class="certificate-image">
                                    <asp:ImageButton ID="CertificateImage" runat="server" AlternateText="Imagem não encontrada" CssClass="certificate-preview" />
                                </div>

                                <div class="certificate-title-date-wrapper">
                                    <div class="certificate-title">
                                        <asp:Label ID="CertificateTitle" runat="server" class="title text--big" Text="Titulo" />
                                    </div>
                                    <div class="certificate-date">
                                        <asp:Label ID="CertificateDate" runat="server" class="date text--medium" Text="Data" />
                                    </div>
                                </div>
                            </div>
                            <div class="right-div">
                                <div class="download-certificate">
                                    <asp:Button ID="DownloadCertificate" runat="server" Text="Baixar Certificado" CssClass="donwload-button" OnClick="DownloadCertificate_Click" />
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
