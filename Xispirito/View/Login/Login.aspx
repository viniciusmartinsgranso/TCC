<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPage/MasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Xispirito.View.Login.Login" %>

<asp:Content ID="HeaderFooter" ContentPlaceHolderID="HeaderFooter" runat="server">
</asp:Content>
<asp:Content ID="Content" ContentPlaceHolderID="Content" runat="server">
    <!DOCTYPE html>
    <link rel="stylesheet" href="Login.css" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <div class="login-gradient">
        <div class="login-wrap">
            <div class="login-html">
                <input id="tab-1" type="radio" name="tab" class="sign-in" checked><label for="tab-1" class="tab">Entrar</label>
                <input id="tab-2" type="radio" name="tab" class="sign-up"><label for="tab-2" class="tab">Inscrever-se</label>
                <div class="login-form">
                    <div class="sign-in-htm">
                        <asp:Login ID="SignIn" runat="server" OnAuthenticate="SignIn_Authenticate" CssClass="sign-in-table">
                            <LayoutTemplate>
                                <div class="group">
                                    <asp:Label ID="SignInEmailLabel" runat="server" AssociatedControlID="UserName" CssClass="label" >E-mail</asp:Label>
                                    <asp:RequiredFieldValidator ID="SignInEmailRequired" runat="server" ControlToValidate="UserName" ErrorMessage="Preencha o Campo de E-mail!" ValidationGroup="SignIn" CssClass="field-validator">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revSignInEmail" runat="server" ControlToValidate="UserName" ErrorMessage="Informe um E-mail Válido!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="SignIn" CssClass="field-validator">*</asp:RegularExpressionValidator>
                                    <asp:TextBox ID="UserName" runat="server" type="text" CssClass="input"></asp:TextBox>
                                </div>
                                <div class="group">
                                    <asp:Label ID="SignInPasswordLabel" runat="server" AssociatedControlID="Password" CssClass="label">Senha</asp:Label>
                                    <asp:RequiredFieldValidator ID="SignInPasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="A Senha é Obrigatória!" ToolTip="A senha é obrigatória." ValidationGroup="SignIn" CssClass="field-validator">*</asp:RequiredFieldValidator>
                                    <asp:TextBox ID="Password" runat="server" TextMode="Password" type="text" CssClass="input"></asp:TextBox>
                                </div>
                                <div class="group">
                                    <input id="check" type="checkbox" class="check" checked>
                                    <label for="check"><span class="icon"></span> Lembrar Senha </label>
                                </div>
                                <div class="group">
                                    <asp:ValidationSummary ID="vsSignIn" runat="server" ForeColor="#E50914" ShowMessageBox="True" ShowSummary="False" ValidationGroup="SignIn" />
                                </div>
                                <div class="group">
                                    <asp:Button ID="btnSignIn" runat="server" CommandName="Login" Text="Entrar" ValidationGroup="SignIn" CssClass="button"/>
                                </div>
                            </LayoutTemplate>
                        </asp:Login>

                        <div class="hr"></div>
                        <div class="foot-lnk">
                            <a href="#forgot">Esqueceu sua senha?</a>
                        </div>
                    </div>

                    <div class="sign-up-htm">
                        <div class="group">
                            <label for="user" class="label">Nome</label>
                            <asp:RequiredFieldValidator ID="SignUpNameRequired" runat="server" ControlToValidate="SignUpName" ErrorMessage="O Nome é Obrigatório!" ValidationGroup="SignUp" CssClass="field-validator">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="SignUpName" runat="server" type="text" class="input"></asp:TextBox>
                        </div>
                        <div class="group">
                            <label for="pass" class="label">E-mail</label>
                            <asp:RequiredFieldValidator ID="SignUpEmailRequired" runat="server" ControlToValidate="SignUpEmail" ErrorMessage="O E-mail é Obrigatório!" ValidationGroup="SignUp" CssClass="field-validator">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revSignUpEmail" runat="server" ControlToValidate="SignUpEmail" ErrorMessage="E-mail Inválido!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="SignUp" CssClass="field-validator">*</asp:RegularExpressionValidator>
                            <asp:TextBox ID="SignUpEmail" runat="server" type="text" class="input"></asp:TextBox>
                        </div>
                        <div class="group">
                            <label for="pass" class="label">Senha</label>
                            <asp:RequiredFieldValidator ID="SignUpPasswordRequired" runat="server" ControlToValidate="SignUpPassword" ErrorMessage="A Senha é Obrigatório!" ValidationGroup="SignUp" CssClass="field-validator">*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvSignUp" runat="server" ControlToCompare="SignUpPassword" ControlToValidate="SignUpRepeatPassword" ErrorMessage="As Senhas não coincidem!" ValidationGroup="SignUp" CssClass="field-validator">*</asp:CompareValidator>
                            <asp:TextBox ID="SignUpPassword" runat="server" type="password" class="input" data-type="password"></asp:TextBox>
                        </div>
                        <div class="group">
                            <label for="pass" class="label">Repetir a senha</label>
                            <asp:RequiredFieldValidator ID="SignUpRepeatPasswordRequired" runat="server" ControlToValidate="SignUpRepeatPassword" ErrorMessage="A Repetição Senha é Obrigatório!" ValidationGroup="SignUp" CssClass="field-validator">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="SignUpRepeatPassword" runat="server" type="password" class="input" data-type="password"></asp:TextBox>
                        </div>
                        <div class="group">
                            <asp:ValidationSummary ID="vsSignUp" runat="server" ForeColor="#E50914" ShowMessageBox="True" ShowSummary="False" ValidationGroup="SignUp" />
                        </div>
                        <div class="group">
                            <asp:Button ID="btnSignUp" runat="server" Text="Inscrever-se" type="submit" class="button" OnClick="SignUp_Click" ValidationGroup="SignUp" />
                        </div>
                        <div class="hr"></div>
                        <div class="foot-lnk">
                            <label class="foot-member" for="tab-1">Já é membro?</label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
