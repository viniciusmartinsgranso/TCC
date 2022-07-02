using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using Xispirito.Controller;
using Xispirito.Models;

namespace Xispirito.View.Lectures.CRUD
{
    public partial class Lecture_CRUD : Page
    {
        private AdministratorBAL administratorBAL = new AdministratorBAL();
        private LectureBAL lectureBAL = new LectureBAL();

        private Administrator administrator = new Administrator();
        private Lecture lecture = new Lecture();

        private bool insertMode = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    administrator.SetEmail(User.Identity.Name);
                    administrator = administratorBAL.GetAccount(administrator.GetEmail());

                    if (administrator != null)
                    {
                        ModalityLecture.DataSource = Enum.GetNames(typeof(Modality));
                        ModalityLecture.DataBind();
                        ModalityLecture.BackColor = ModalityColor.GetModalityColor(ModalityLecture.SelectedValue);

                        if (!string.IsNullOrEmpty(Request.QueryString["lectureId"]))
                        {
                            insertMode = false;
                            
                            LoadLectureInfo();
                        }
                    }
                    else
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

        private void LoadLectureInfo()
        {
            GetLectureInfo();
            SetLectureInfo();
        }

        private void GetLectureInfo()
        {
            lecture = lectureBAL.GetLecture(Convert.ToInt32(Request.QueryString["lectureId"]));
        }

        private void SetLectureInfo()
        {
            ActionLecture.Text = "Editar Palestra";
            NameLecture.Text = lecture.GetName();
            DescriptionLecture.Text = lecture.GetDescription();
            DateLecture.Text = lecture.GetDate().ToString("dd/MM/yyyy");

            StartTime.Value = lecture.GetDate().ToString("HH:mm");

            DateTime dateTime = lecture.GetDate();
            dateTime = dateTime.AddMinutes(lecture.GetTime());

            EndTime.Value = dateTime.ToString("HH:mm");

            ModalityLecture.SelectedValue = lecture.GetModality();
            ModalityLecture.BackColor = ModalityColor.GetModalityColor(ModalityLecture.SelectedValue);

            if (lecture.GetModality() != Enum.GetName(typeof(Modality), 0))
            {
                AddressLecture.Visible = true;
                AddressLecture.Text = lecture.GetAddress();
            }
            else
            {
                AddressLecture.Visible = false;
            }

            LimitLecture.Text = lecture.GetLimit().ToString();
            ImageLecture.ImageUrl = lecture.GetPicture();

            ChangeModalityBackColor();
            ChangeAddressVisibility();
        }

        private void ChangeModalityBackColor()
        {
            ModalityLecture.BackColor = ModalityColor.GetModalityColor(ModalityLecture.SelectedValue);
        }

        private void ChangeAddressVisibility()
        {
            if (ModalityLecture.SelectedValue != Enum.GetName(typeof(Modality), 0))
            {
                AddressLecture.Visible = true;
                AddressLecture.Text = lecture.GetAddress();
            }
            else
            {
                AddressLecture.Visible = false;
            }
        }

        protected void ModalityLecture_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeModalityBackColor();
            ChangeAddressVisibility();
        }

        protected void SubmitUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["lectureId"]))
            {
                insertMode = false;
            }

            if (insertMode == false)
            {
                // UPDATE
                GetLectureInfo();
                lecture = GetSubmitInformation(lecture.GetId());

                lectureBAL.UpdateLecture(lecture);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Palestra Editada!", "alert('Palestra Editada com Sucesso!');", true);
            }
            else
            {
                if (administrator != null)
                {
                    // INSERT
                    lecture.SetId(lectureBAL.GetNextId());
                    lecture = GetSubmitInformation(lecture.GetId());

                    lectureBAL.InsertLecture(lecture);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Palestra Cadastrada!", "alert('Palestra Cadastrada com Sucesso!');", true);

                    Response.Redirect("~/View/Lectures/CRUD/Lecture-CRUD.aspx?lectureId=" + lecture.GetId());
                }
            }
        }

        private Lecture GetSubmitInformation(int lectureId)
        {
            string address = "";
            if (AddressLecture.Visible)
            {
                address = AddressLecture.Text;
            }

            string picturePath = SaveUploadImage(lectureId);
            if (string.IsNullOrEmpty(picturePath))
            {
                picturePath = lecture.GetPicture();
            }

            int limit = Convert.ToInt32(LimitLecture.Text);

            Lecture objLecture = new Lecture(
                lectureId,
                NameLecture.Text,
                picturePath,
                CalculateLectureTime(),
                Convert.ToDateTime(DateLecture.Text + " " + StartTime.Value),
                DescriptionLecture.Text,
                ModalityLecture.SelectedValue,
                address,
                limit,
                true
            );
            return objLecture;
        }

        private int CalculateLectureTime()
        {
            int minutes;

            string date = DateLecture.Text;
            DateTime startDateTime = Convert.ToDateTime(date + " " + StartTime.Value);
            DateTime endDateTime = Convert.ToDateTime(date + " " + EndTime.Value);

            TimeSpan timeSpan;
            if (startDateTime.Hour <= endDateTime.Hour)
            {
                timeSpan = startDateTime - endDateTime;
                minutes = Convert.ToInt32((timeSpan.TotalHours / 60) + timeSpan.TotalMinutes);
            }
            else
            {
                double dayHours;
                dayHours = ((startDateTime.Hour - endDateTime.Hour) - 24) * -1;

                double dayMinutes;
                if (startDateTime.Minute > endDateTime.Minute)
                {
                    dayMinutes = (startDateTime.Minute - endDateTime.Minute);
                }
                else
                {
                    dayMinutes = (endDateTime.Minute - startDateTime.Minute);
                }
                minutes = Convert.ToInt32((dayHours * 60) + dayMinutes);
            }

            if (minutes < 0)
            {
                minutes = minutes * -1;
            }

            return minutes;
        }

        private string SaveUploadImage(int lectureId)
        {
            string cryptographLectureId = "";
            string fileName = "";

            string filePath = "";
            string fullPath = "";

            HttpFileCollection hfc = null;
            HttpPostedFile hpf = null;

            string clientFile = "";
            string extension = "";
            if (LecturePhotoUpload.HasFile)
            {
                cryptographLectureId = Cryptography.GetMD5Hash(lectureId.ToString());
                fileName = cryptographLectureId;

                filePath = @"\View\Images\Lectures\" + cryptographLectureId;

                if (LecturePhotoUpload.PostedFile.ContentType == "image/jpeg" || LecturePhotoUpload.PostedFile.ContentType == "image/png" || LecturePhotoUpload.PostedFile.ContentType == "image/gif" || LecturePhotoUpload.PostedFile.ContentType == "image/bmp")
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
                            fullPath = filePath + @"\" + fileName + extension;
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
            return fullPath;
        }
    }
}