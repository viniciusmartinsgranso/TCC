using System;
using System.Web.UI.WebControls;
using Xispirito.Controller;
using Xispirito.Models;

namespace Xispirito.View.MasterPage
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        private ViewerBAL viewerBAL = new ViewerBAL();
        private SpeakerBAL speakerBAL = new SpeakerBAL();
        private AdministratorBAL administratorBAL = new AdministratorBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && Page.User.Identity.IsAuthenticated)
            {
                // Load User Information.
                GetUserInformation();
            }
        }

        private void GetUserInformation()
        {
            BaseUser user;
            string userRole;
            UserType userType;

            string accountEmail = Page.User.Identity.Name.ToString();
            if (FindViewerAccount(accountEmail))
            {
                user = viewerBAL.GetAccount(accountEmail);
                userRole = "Aluno";
                userType = UserType.Viewer;
            }
            else if (FindSpeakerAccount(accountEmail))
            {
                user = speakerBAL.GetAccount(accountEmail);
                userRole = "Palestrante";
                userType = UserType.Speaker;
            }
            else
            {
                user = administratorBAL.GetAccount(accountEmail);
                userRole = "Administrador";
                userType = UserType.Administrator;
            }
            SetUserInformation(user, userRole, userType);
        }

        private void SetUserInformation(BaseUser user, string userRole, UserType userType)
        {
            Image userPicture = (Image)MasterLoginView.FindControl("UserPicture");
            if (user.GetPicture() != "")
            {
                userPicture.ImageUrl = user.GetPicture();
            }
            else
            {
                userPicture.ImageUrl = @"~/View/Images/User.png";
            }

            Label UserName = (Label)MasterLoginView.FindControl("UserName");
            UserName.Text = user.GetName();

            Label UserRole = (Label)MasterLoginView.FindControl("UserRole");
            UserRole.Text = userRole;

            Image administrationImage = (Image)MasterLoginView.FindControl("Administrate");
            administrationImage.ImageUrl = @"~/View/Images/Administrator.png";

            LinkButton administrationMenu = (LinkButton)MasterLoginView.FindControl("AdministrationMenu");
            administrationMenu.PostBackUrl = "~/View/AdminOptions/AdminOptions.aspx";

            if (userType == UserType.Administrator)
            {
                administrationImage.Visible = true;
                administrationMenu.Visible = true;
            }

            Image image = (Image)MasterLoginView.FindControl("Profile");
            image.ImageUrl = @"~/View/Images/Profile.png";

            LinkButton UserProfile = (LinkButton)MasterLoginView.FindControl("UserProfile");
            UserProfile.PostBackUrl = @"~/View/Profiles/" + userType + "/" + userType + ".aspx";

            LinkButton RegisteredEvents = (LinkButton)MasterLoginView.FindControl("RegisteredEvents");
            RegisteredEvents.PostBackUrl = "~/View/RegisteredEvents/RegisteredEvents.aspx";

            LinkButton UserCertificates = (LinkButton)MasterLoginView.FindControl("UserCertificates");
            UserCertificates.PostBackUrl = "~/View/Certificates/Certificates.aspx";
        }

        private bool FindViewerAccount(string userEmail)
        {
            bool accountFound = viewerBAL.VerifyAccount(userEmail);

            if (accountFound == true)
            {
                accountFound = viewerBAL.VerifyAccountStatus(viewerBAL.GetAccount(userEmail));
            }
            return accountFound;
        }

        private bool FindSpeakerAccount(string userEmail)
        {
            bool accountFound = speakerBAL.VerifyAccount(userEmail);

            if (accountFound == true)
            {
                accountFound = speakerBAL.VerifyAccountStatus(speakerBAL.GetAccount(userEmail));
            }
            return accountFound;
        }

        public void DownloadDocument()
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=Ajuda.pdf");
            Response.TransmitFile(@"~\Document\Help.pdf");
            Response.End();
        }

        protected void Help_Click(object sender, EventArgs e)
        {
            DownloadDocument();
        }
    }
}