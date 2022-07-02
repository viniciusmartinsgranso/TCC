using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xispirito.Controller;

namespace Xispirito.View.Login
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SignIn_Authenticate(object sender, AuthenticateEventArgs e)
        {
            string signInEmail = SignIn.UserName;
            string signInPassword = SignIn.Password;

            bool accountFound = FindAccount(signInEmail, signInPassword);
            
            if (accountFound)
            {
                bool accountActive = false;

                ViewerBAL viewerBAL = new ViewerBAL();
                accountFound = viewerBAL.VerifyAccount(signInEmail, signInPassword);

                if (accountFound == false)
                {
                    SpeakerBAL speakerBAL = new SpeakerBAL();
                    accountFound = speakerBAL.VerifyAccount(signInEmail, signInPassword);

                    if (accountFound == false)
                    {
                        AdministratorBAL administratorBAL = new AdministratorBAL();
                        accountFound = administratorBAL.VerifyAccount(signInEmail, signInPassword);

                        if (accountFound == true)
                        {
                            accountActive = administratorBAL.VerifyAccountStatus(administratorBAL.GetAccount(signInEmail, signInPassword));
                        }
                    }
                    else
                    {
                        accountActive = speakerBAL.VerifyAccountStatus(speakerBAL.GetAccount(signInEmail, signInPassword));
                    }
                }
                else
                {
                    accountActive = viewerBAL.VerifyAccountStatus(viewerBAL.GetAccount(signInEmail, signInPassword));
                }

                if (accountActive == false)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Email já Cadastrado!", "alert('Essa conta está inativa, entre em contato com administrador para reativa-lá!');", true);
                    accountFound = accountActive;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Login Inválido!", "alert('Login Inválido, verifique seu E-mail e/ou Senha e tente novamente!');", true);
            }
            e.Authenticated = accountFound;
        }

        protected void SignUp_Click(object sender, EventArgs e)
        {
            bool accountFound = FindAccount(SignUpEmail.Text);

            if (accountFound == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Email já Cadastrado!", "alert('Esse email já foi cadastrado!');", true);
                SignIn.UserName = SignUpEmail.Text;
            }
            else
            {
                ViewerBAL viewerBAL = new ViewerBAL();
                viewerBAL.SignUp(SignUpName.Text, SignUpEmail.Text, SignUpPassword.Text);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Registrado com Sucesso!", "alert('Registrado com Sucesso, agora é so efetuar o Login!');", true);
            }
            ClearSignUpFields();
        }

        private void ClearSignUpFields()
        {
            SignUpName.Text = "";
            SignUpEmail.Text = "";
            SignUpPassword.Text = "";
            SignUpRepeatPassword.Text = "";
        }

        private bool FindAccount(string email)
        {
            bool accountFound = false;
            ViewerBAL viewerBAL = new ViewerBAL();

            accountFound = viewerBAL.VerifyAccount(email);

            if (accountFound == false)
            {
                SpeakerBAL speakerBAL = new SpeakerBAL();
                accountFound = speakerBAL.VerifyAccount(email);

                if (accountFound == false)
                {
                    AdministratorBAL administratorBAL = new AdministratorBAL();
                    accountFound = administratorBAL.VerifyAccount(email);
                }
            }
            return accountFound;
        }

        private bool FindAccount(string email, string password)
        {
            bool accountFound = false;
            ViewerBAL viewerBAL = new ViewerBAL();
            accountFound = viewerBAL.VerifyAccount(email, password);

            if (accountFound == false)
            {
                SpeakerBAL speakerBAL = new SpeakerBAL();
                accountFound = speakerBAL.VerifyAccount(email, password);

                if (accountFound == false)
                {
                    AdministratorBAL administratorBAL = new AdministratorBAL();
                    accountFound = administratorBAL.VerifyAccount(email, password);
                }
            }
            return accountFound;
        }
    }
}