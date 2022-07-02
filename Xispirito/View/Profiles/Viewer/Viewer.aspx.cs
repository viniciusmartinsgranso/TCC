using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using Xispirito.Controller;
using Xispirito.Models;

namespace Xispirito.View.Profile_Viewer
{
    public partial class Profile_Viewer : Page
    {
        private ViewerBAL viewerBAL = new ViewerBAL();

        private Viewer viewer = new Viewer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    viewer.SetEmail(User.Identity.Name);
                    LoadViewerProfile(viewer.GetEmail());
                }
                else
                {
                    Response.Redirect("~/View/Login/Login.aspx");
                }
            }
        }

        private void LoadViewerProfile(string viewerEmail)
        {
            GetViewerProfile(viewerEmail);
            SetViewerProfile(viewer);
        }

        private void GetViewerProfile(string viewerEmail)
        {
            Viewer objViewer = new Viewer();
            objViewer = viewerBAL.GetAccount(viewerEmail);

            viewer = objViewer;
        }

        private void SetViewerProfile(Viewer objViewer)
        {
            NameViewer.Text = objViewer.GetName();
            EmailViewer.Text = objViewer.GetEmail();

            if (objViewer.GetPicture() != "")
            {
                ImageViewer.ImageUrl = objViewer.GetPicture();
            }
            else
            {
                ImageViewer.ImageUrl = @"~/View/Images/User.png";
            }
        }

        private string SaveUploadImage()
        {
            string cryptographViewerEmail = Cryptography.GetMD5Hash(User.Identity.Name);
            string fileName = cryptographViewerEmail;

            string filePath = @"\UsersData\Viewers\" + cryptographViewerEmail + @"\Image";

            if (!File.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            HttpFileCollection hfc = null;
            HttpPostedFile hpf = null;

            string clientFile = "";
            string extension = "";
            if (ViewerPhotoUpload.HasFile)
            {
                if (ViewerPhotoUpload.PostedFile.ContentType == "image/jpeg" || ViewerPhotoUpload.PostedFile.ContentType == "image/png" || ViewerPhotoUpload.PostedFile.ContentType == "image/gif" || ViewerPhotoUpload.PostedFile.ContentType == "image/bmp")
                {
                    try
                    {
                        hfc = Request.Files;
                        hpf = hfc[0];

                        if (hpf.ContentLength > 0)
                        {
                            clientFile = Path.GetFileName(hpf.FileName);
                            extension = Path.GetExtension(hpf.FileName);

                            string folderPath = ConfigurationManager.AppSettings["XispiritoPath"] + filePath;
                            if (!File.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            string mapPath = Server.MapPath(filePath) + @"\" + fileName + extension;
                            hpf.SaveAs(mapPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error!", "alert('Um erro inesperado acorreu, tente novamente!');" + ex.Message, true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Archive Invalid!", "alert('Arquivo Inválido, É permitido carregar apenas arquivos em .JPEG .PNG .GIF .BMP');", true);
                }
            }
            return filePath + @"\" + cryptographViewerEmail + extension;
        }

        protected void SubmitUpdate_Click(Object sender, EventArgs e)
        {
            _ = new Viewer();
            Viewer updatedViewer = viewerBAL.GetAccount(User.Identity.Name);

            string updateName = NameViewer.Text;
            if (!string.IsNullOrEmpty(updateName) && updatedViewer.GetName() != updateName)
            {
                updatedViewer.SetName(updateName);
            }

            if (!string.IsNullOrEmpty(PasswordViewer.Text))
            {
                string updatePassword = Cryptography.GetMD5Hash(PasswordViewer.Text);

                if (updatedViewer.GetEncryptedPassword() != updatePassword)
                {
                    updatedViewer.SetEncryptedPassword(updatePassword);
                }
            }

            if (ViewerPhotoUpload.HasFile)
            {
                updatedViewer.SetPicture(SaveUploadImage());
            }

            if (updatedViewer != viewer)
            {
                viewerBAL.UpdateViewerAccount(updatedViewer);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Perfil Atualizado com Sucesso!", "alert('Perfil Atualizado com Sucesso!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Faça algum Alteração!", "alert('Altere alguma Informação para atualizar os dados do Perfil!');", true);
            }
        }
    }
}