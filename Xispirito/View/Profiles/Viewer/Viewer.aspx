<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Viewer.aspx.cs" Inherits="Xispirito.View.Profile_Viewer.Profile_Viewer" %>

<asp:Content ID="HeaderFooter" ContentPlaceHolderID="HeaderFooter" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>Eventos - Xispirito </title>

        <link rel="stylesheet" href="Viewer.css" />
        <link rel="stylesheet" href="print.css" media="print" />

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js">
        </script>
    </head>
    <body>
        <section class="profile">
            <div class="profile-label">
                <h3 style="color: white;">Editar Perfil do Aluno</h3>
            </div>
            <div class="profile-form">
                <div class="profile-form-left">
                    <h4 style="color: white;">Informações da sua Conta:</h4>
                </div>
                <div class="profile-form-left">
                    <asp:TextBox ID="NameViewer" runat="server" class="input-text" placeholder="Informe o Nome" />
                </div>
                <div class="profile-form-left">
                    <asp:TextBox ID="EmailViewer" runat="server" class="input-text" placeholder="Informe o E-mail" ReadOnly="True" />
                </div>
                <div class="profile-form-left">
                    <h4 style="color: white;">Alterar a Senha:</h4>
                </div>
                <div class="profile-form-left">
                    <asp:TextBox ID="PasswordViewer" runat="server" class="input-text" placeholder="Informe a Senha" TextMode="Password" />
                </div>
                <div class="profile-form-left">
                    <div>
                        <asp:CompareValidator ID="cvRepeatPasswordViewer" runat="server" ControlToCompare="PasswordViewer" ControlToValidate="RepeatPasswordViewer" ErrorMessage="As Senhas não coincidem!" ValidationGroup="SubmitUpdateGroup" CssClass="field-validator">*</asp:CompareValidator>
                    </div>
                    <div>
                        <asp:TextBox ID="RepeatPasswordViewer" runat="server" class="input-text" placeholder="Informe a Senha Novamente" TextMode="Password" />
                    </div>
                </div>
                <div class="profile-form-pic-viewer" shape="round">
                    <asp:Image ID="ImageViewer" runat="server" CssClass="profile-picture" />
                </div>
                <div class="profile-form-set-photo-button">
                    <div class="lecture-form-set-photo-button">
                        <asp:FileUpload ID="ViewerPhotoUpload" runat="server" class="chose-photo" accept="image/*" ToolTip="Anexar uma Foto" />
                    </div>
                </div>
            </div>
            <div>
                <asp:ValidationSummary ID="vsSubmitUpdate" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="SubmitUpdateGroup" />
            </div>
            <div class="profile-form-submit">
                <asp:Button ID="SubmitUpdate" runat="server" class="submit-button px-5 py-2 btn btn-success fs-4" Text="Salvar Alterações" ValidationGroup="SubmitUpdateGroup" OnClick="SubmitUpdate_Click" />
            </div>
        </section>

        <script>
            $("#<%=ViewerPhotoUpload.ClientID%>").on('change', function () {
                if (this.files[0].type.indexOf("image") > -1) {
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        $('#<%=ImageViewer.ClientID%>').attr('src', e.target.result);
                    }
                    reader.readAsDataURL(this.files[0]);
                }
                else {
                    $('#<%=ImageViewer.ClientID%>').attr('src', '');
                    alert('Não é uma imagem válida')
                }
            });
        </script>

    </body>
    </html>
</asp:Content>
