using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xispirito.Controller;
using Xispirito.Models;

namespace Xispirito.View.AdminOptions.Users.Create_User
{
    public partial class Create_Speaker : Page
    {
        private Administrator administrator = new Administrator();
        private AdministratorBAL administratorBAL = new AdministratorBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    administrator.SetEmail(User.Identity.Name);
                    administrator = administratorBAL.GetAccount(administrator.GetEmail());
                    if (administrator == null)
                    {
                        Response.Redirect("~/View/Home/Home.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/View/Login/Login.aspx");
                }
            }
        }

        protected void GenerateSpeaker_Click(object sender, EventArgs e)
        {
            SpeakerBAL speakerBAL = new SpeakerBAL();

            Speaker speaker = new Speaker();
            speaker.SetEmail(SpeakerEmail.Text);
            speaker.SetName(SpeakerName.Text);

            string password = Cryptography.GetMD5Hash(SpeakerEmail.Text + (speakerBAL.GetLastIndexSpeaker() + 1));
            speaker.SetEncryptedPassword(Cryptography.GetMD5Hash(password));
            speakerBAL.InsertGenerateSpeaker(speaker);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Acesso do Palestrante Gerado!", "alert('Acesso do Palestrante Gerado com Sucesso! Senha de acesso gerada: " + password + " ');", true);

            ClearFields();
        }

        private void ClearFields()
        {
            SpeakerName.Text = "";
            SpeakerEmail.Text = "";
        }
    }
}