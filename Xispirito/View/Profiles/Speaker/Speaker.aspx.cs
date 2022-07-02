using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xispirito.Controller;
using Xispirito.Models;

namespace Xispirito.View.Profiles.Profile_Speaker
{
    public partial class Profile_Speaker : System.Web.UI.Page
    {
        private SpeakerBAL speakerBAL = new SpeakerBAL();

        private Speaker speaker = new Speaker();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && User.Identity.IsAuthenticated)
            {
                if (Request.QueryString["user"] != null && User.Identity.Name == Request.QueryString["user"])
                {
                    speaker.SetEmail(Request.QueryString["user"]);
                    LoadSpeakerProfile(speaker.GetEmail());
                }
                else
                {
                    Response.Redirect("~/View/Home/Home.aspx");
                }
            }
        }

        private void LoadSpeakerProfile(string speakerEmail)
        {
            speaker = GetSpeakerProfile(speakerEmail);

            SetSpeakerProfile(speaker);
        }

        private Speaker GetSpeakerProfile(string speakerEmail)
        {
            speaker = speakerBAL.GetAccount(speakerEmail);

            return speaker;
        }

        private void SetSpeakerProfile(Speaker objSpeaker)
        {
            NameSpeaker.Text = objSpeaker.GetName();
            EmailSpeaker.Text = objSpeaker.GetEmail();
            ProfissionSpeaker.Text = objSpeaker.GetSpeakerProfession();
            ImageSpeaker.ImageUrl = objSpeaker.GetPicture();
        }
    }
}