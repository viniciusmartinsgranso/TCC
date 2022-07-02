<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Create-Speaker.aspx.cs" Inherits="Xispirito.View.AdminOptions.Users.Create_User.Create_Speaker" %>

<asp:Content ID="HeaderFooter" ContentPlaceHolderID="HeaderFooter" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <!DOCTYPE html>

    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Eventos - Xispirito </title>

        <link rel="stylesheet" href="Create-Speaker.css" />
        <link rel="stylesheet" href="print.css" media="print" />

    </head>
    <body>
        <section class="create-speaker-admin">
            <div style="width: 75%">
                <div class="back-button-inline">
                    <asp:ImageButton ID="Return" runat="server" CssClass="back-button" ImageUrl="~/View/Images/Return.png" PostBackUrl="~/View/AdminOptions/Users/View-Users.aspx" />
                </div>
            </div>

            <h3 style="text-align: center; color: white; font-size: 3rem; font-weight: bold;">Cadastrar Informações Básicas do Palestrante</h3>
            <div style="width: 100%;">
                <div class="speaker-inputs-admin">
                    <div>
                        <div class="validator">
                            <asp:RequiredFieldValidator ID="NameSpeakerRequired" runat="server" ControlToValidate="SpeakerName" ErrorMessage="O Nome do Palestrante é Obtigatório!" ValidationGroup="GenerateSpeaker" CssClass="field-validator">* O Nome do Palestrante é Obtigatório</asp:RequiredFieldValidator>
                        </div>
                        <asp:TextBox ID="SpeakerName" runat="server" CssClass="input-text" placeholder="Informe o Nome do Palestrante" />
                    </div>
                    <div>
                        <div class="validator">
                            <asp:RequiredFieldValidator ID="EmailSpeakerRequired" runat="server" ControlToValidate="SpeakerEmail" ErrorMessage="O E-mail do Palestrante é Obtigatório!" ValidationGroup="GenerateSpeaker" CssClass="field-validator">* O E-mail do Palestrante é Obtigatório</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmailSpeaker" runat="server" ControlToValidate="SpeakerEmail" ErrorMessage="E-mail Inválido!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="GenerateSpeaker" CssClass="field-validator" Style="margin-left: -70%;">* E-mail Inválido</asp:RegularExpressionValidator>
                        </div>
                        <asp:TextBox ID="SpeakerEmail" runat="server" CssClass="input-text" placeholder="Informe o Email do Palestrante" />
                    </div>
                </div>
            </div>
            <div>
                <asp:ValidationSummary ID="vsGenerateSpeaker" runat="server" ForeColor="#E50914" ShowMessageBox="True" ShowSummary="False" ValidationGroup="GenerateSpeaker" />
                <asp:Button ID="GenerateSpeaker" runat="server" CssClass="create-password" Text="Gerar Acesso do Palestrante" OnClick="GenerateSpeaker_Click" ValidationGroup="GenerateSpeaker" />
            </div>
        </section>
    </body>
    </html>
</asp:Content>
