using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xispirito.Controller;
using Xispirito.Models;

namespace Xispirito.View.Certificates.Viewers
{
    public partial class ViewerCertificates : Page
    {
        UserType userType;

        private ViewerBAL viewerBAL = new ViewerBAL();
        private ViewerCertificateBAL viewerCertificateBAL = new ViewerCertificateBAL();

        private SpeakerBAL speakerBAL = new SpeakerBAL();
        private SpeakerCertificateBAL speakerCertificateBAL = new SpeakerCertificateBAL();

        private AdministratorBAL administratorBAL = new AdministratorBAL();
        private AdministratorCertificateBAL administratorCertificateBAL = new AdministratorCertificateBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    GetAccountType(User.Identity.Name);
                    if (userType == UserType.Administrator)
                    {
                        LoadAdministratorCertificatesDataBound(administratorCertificateBAL.GetUserCertificates(User.Identity.Name));
                    }
                    else if (userType == UserType.Speaker)
                    {
                        LoadSpeakerCertificatesDataBound(speakerCertificateBAL.GetUserCertificates(User.Identity.Name));
                    }
                    else
                    {
                        LoadViewerCertificatesDataBound(viewerCertificateBAL.GetUserCertificates(User.Identity.Name));
                    }
                }
                else
                {
                    Response.Redirect("~/View/Login/Login.aspx");
                }
            }
        }

        private void LoadViewerCertificatesDataBound(List<ViewerCertificate> viewerCertificates)
        {
            string title = "Meus Certificados ";

            ListViewCertificates.Items.Clear();
            if (viewerCertificates != null)
            {
                MyCertificates.Text = title + "(" + viewerCertificates.Count + ")";
                ListViewCertificates.DataSource = viewerCertificates;
            }
            else
            {
                MyCertificates.Text = title + "(0)";
            }
            ListViewCertificates.DataBind();
        }

        private void LoadAdministratorCertificatesDataBound(List<AdministratorCertificate> administratorCertificates)
        {
            string title = "Meus Certificados ";

            ListViewCertificates.Items.Clear();
            if (administratorCertificates != null)
            {
                MyCertificates.Text = title + "(" + administratorCertificates.Count + ")";
                ListViewCertificates.DataSource = administratorCertificates;
            }
            else
            {
                MyCertificates.Text = title + "(0)";
            }
            ListViewCertificates.DataBind();
        }

        private void LoadSpeakerCertificatesDataBound(List<SpeakerCertificate> speakerCertificates)
        {
            string title = "Meus Certificados ";

            ListViewCertificates.Items.Clear();
            if (speakerCertificates != null)
            {
                MyCertificates.Text = title + "(" + speakerCertificates.Count + ")";
                ListViewCertificates.DataSource = speakerCertificates;
            }
            else
            {
                MyCertificates.Text = title + "(0)";
            }
            ListViewCertificates.DataBind();
        }

        private void GetAccountType(string email)
        {
            if (!viewerBAL.VerifyAccount(email))
            {
                if (!speakerBAL.VerifyAccount(email))
                {
                    if (administratorBAL.VerifyAccount(email))
                    {
                        userType = UserType.Administrator;
                    }
                }
                else
                {
                    userType = UserType.Speaker;
                }
            }
            else
            {
                userType = UserType.Viewer;
            }
        }

        protected void SearchCertificate_Click(object sender, EventArgs e)
        {
            string search = FilterCertificate.Text;

            if (search != null && User.Identity.IsAuthenticated)
            {
                GetAccountType(User.Identity.Name);
                if (search.ToLower() == "gerar")
                {
                    int limit = 3;

                    FilterCertificate.Text = "";

                    LectureBAL lectureBAL = new LectureBAL();
                    CertificateBAL certificateBAL = new CertificateBAL();
                    if (userType == UserType.Administrator)
                    {
                        _ = new Administrator();
                        AdministratorBAL administratorBAL = new AdministratorBAL();
                        Administrator administrator = administratorBAL.GetAccount(User.Identity.Name);

                        for (int i = 1; i <= limit; i++)
                        {
                            _ = new Lecture();
                            Lecture lecture = lectureBAL.GetLecture(i);

                            _ = new Certificate();
                            Certificate certificate = certificateBAL.GetCertificateById(i);

                            CertificateGenerator.GenerateAdministratorCertificate(administrator, lecture, certificate);
                        }
                    }
                    else if (userType == UserType.Speaker)
                    {
                        _ = new Speaker();
                        SpeakerBAL speakerBAL = new SpeakerBAL();
                        Speaker speaker = speakerBAL.GetAccount(User.Identity.Name);

                        for (int i = 1; i <= limit; i++)
                        {
                            _ = new Lecture();
                            Lecture lecture = lectureBAL.GetLecture(i);

                            _ = new Certificate();
                            Certificate certificate = certificateBAL.GetCertificateById(i);

                            CertificateGenerator.GenerateSpeakerCertificate(speaker, lecture, certificate);
                        }
                    }
                    else
                    {
                        _ = new Viewer();
                        ViewerBAL viewerBAL = new ViewerBAL();
                        Viewer viewer = viewerBAL.GetAccount(User.Identity.Name);

                        for (int i = 1; i <= limit; i++)
                        {
                            _ = new Lecture();
                            Lecture lecture = lectureBAL.GetLecture(i);

                            _ = new Certificate();
                            Certificate certificate = certificateBAL.GetCertificateById(i);

                            CertificateGenerator.GenerateViewerCertificate(viewer, lecture, certificate);
                        }
                    }

                    if (userType == UserType.Administrator)
                    {
                        LoadAdministratorCertificatesDataBound(administratorCertificateBAL.GetUserCertificates(User.Identity.Name));
                    }
                    else if (userType == UserType.Speaker)
                    {
                        LoadSpeakerCertificatesDataBound(speakerCertificateBAL.GetUserCertificates(User.Identity.Name));
                    }
                    else
                    {
                        LoadViewerCertificatesDataBound(viewerCertificateBAL.GetUserCertificates(User.Identity.Name));
                    }
                }
                else
                {
                    if (userType == UserType.Administrator)
                    {
                        LoadAdministratorCertificatesDataBound(administratorCertificateBAL.GetUserCertificates(User.Identity.Name, search));
                    }
                    else if (userType == UserType.Speaker)
                    {
                        LoadSpeakerCertificatesDataBound(speakerCertificateBAL.GetUserCertificates(User.Identity.Name, search));
                    }
                    else
                    {
                        LoadViewerCertificatesDataBound(viewerCertificateBAL.GetUserCertificates(User.Identity.Name, search));
                    }
                }
            }
        }

        protected void ListViewCertificates_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                if (User.Identity.IsAuthenticated)
                {
                    GetAccountType(User.Identity.Name);

                    Image certificateImage = (Image)e.Item.FindControl("CertificateImage");
                    Label titleLabel = (Label)e.Item.FindControl("CertificateTitle");
                    Label dateLabel = (Label)e.Item.FindControl("CertificateDate");
                    Button downloadButton = (Button)e.Item.FindControl("DownloadCertificate");

                    string certificateKey;
                    string path;

                    if (userType == UserType.Administrator)
                    {
                        AdministratorCertificate administratorCertificate = (AdministratorCertificate)e.Item.DataItem;

                        certificateKey = Cryptography.GetMD5Hash(administratorCertificate.GetAdministrator().GetEmail() + administratorCertificate.GetCertificate().GetId().ToString());
                        path = @"\UsersData\Administrators\" + Cryptography.GetMD5Hash(User.Identity.Name) + @"\Certificates\" + certificateKey;

                        certificateImage.ImageUrl = path + ".png";
                        titleLabel.Text = administratorCertificate.GetLecture().GetName();
                        dateLabel.Text = administratorCertificate.GetLecture().GetDate().ToString("dd/MM/yyyy HH:mm");
                    }
                    else if (userType == UserType.Speaker)
                    {
                        SpeakerCertificate speakerCertificate = (SpeakerCertificate)e.Item.DataItem;

                        certificateKey = Cryptography.GetMD5Hash(speakerCertificate.GetSpeaker().GetEmail() + speakerCertificate.GetCertificate().GetId().ToString());
                        path = @"\UsersData\Speakers\" + Cryptography.GetMD5Hash(User.Identity.Name) + @"\Certificates\" + certificateKey;

                        certificateImage.ImageUrl = path + ".png";
                        titleLabel.Text = speakerCertificate.GetLecture().GetName();
                        dateLabel.Text = speakerCertificate.GetLecture().GetDate().ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    {
                        ViewerCertificate viewerCertificate = (ViewerCertificate)e.Item.DataItem;

                        certificateKey = Cryptography.GetMD5Hash(viewerCertificate.GetViewer().GetEmail() + viewerCertificate.GetCertificate().GetId().ToString());
                        path = @"\UsersData\Viewers\" + Cryptography.GetMD5Hash(User.Identity.Name) + @"\Certificates\" + certificateKey;

                        certificateImage.ImageUrl = path + ".png";
                        titleLabel.Text = viewerCertificate.GetLecture().GetName();
                        dateLabel.Text = viewerCertificate.GetLecture().GetDate().ToString("dd/MM/yyyy HH:mm");
                    }
                    downloadButton.CommandArgument = certificateKey;
                }
            }
        }

        protected void DownloadCertificate_Click(Object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string certificateKey = clickedButton.CommandArgument;

            GetAccountType(User.Identity.Name);

            string userPath = ConfigurationManager.AppSettings["XispiritoPath"];
            if (userType == UserType.Administrator)
            {
                userPath += @"\UsersData\Administrators\";
            }
            else if (userType == UserType.Speaker)
            {
                userPath += @"\UsersData\Speakers\";
            }
            else
            {
                userPath += @"\UsersData\Viewers\";
            }
            userPath += Cryptography.GetMD5Hash(User.Identity.Name) + @"\Certificates\" + certificateKey + ".pdf";

            DownloadCertificate(userPath, certificateKey);
        }

        public void DownloadCertificate(string path, string certificateKey)
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=" + certificateKey + ".pdf");
            Response.TransmitFile(path);
            Response.End();
        }
    }
}