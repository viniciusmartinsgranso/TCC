using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xispirito.Controller;
using Xispirito.Models;

namespace Xispirito.View.AdminOptions.Lectures
{
    public partial class Lectures_List : Page
    {
        private Administrator administrator = new Administrator();
        private AdministratorBAL administratorBAL = new AdministratorBAL();

        private LectureBAL lectureBAL = new LectureBAL();

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
                        LoadLecturesDataBound(lectureBAL.GetAdministratorLectureList());
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

        private void LoadLecturesDataBound(List<Lecture> lectures)
        {
            ListViewLectures.Items.Clear();
            if (lectures != null)
            {
                ListViewLectures.DataSource = lectures;
            }
            ListViewLectures.DataBind();
        }

        protected void ListViewLectures_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Lecture lecture = (Lecture)e.Item.DataItem;

                Label lectureName = (Label)e.Item.FindControl("LectureName");
                lectureName.Text = lecture.GetName();

                Label lectureDate = (Label)e.Item.FindControl("LectureDate");
                lectureDate.Text = lecture.GetDate().ToString("dd/MM/yyyy HH:mm");

                Label lectureModality = (Label)e.Item.FindControl("LectureModality");
                lectureModality.Text = lecture.GetModality();
                lectureModality.ForeColor = ModalityColor.GetModalityColor(lecture.GetModality());

                Button editLecture = (Button)e.Item.FindControl("EditLecture");
                editLecture.PostBackUrl = "~/View/Lectures/CRUD/Lecture-CRUD.aspx?lectureId=" + lecture.GetId();

                Button deleteLecture = (Button)e.Item.FindControl("DeleteLecture");
                deleteLecture.CommandArgument = lecture.GetId().ToString();
            }
        }

        protected void SearchEvents_Click(object sender, EventArgs e)
        {
            string search = FilterEvents.Text;

            if (search != null)
            {
                LoadLecturesDataBound(lectureBAL.GetAdministratorLectureList(search));
            }
        }

        protected void DeleteLecture_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            int lectureId = Convert.ToInt32(clickedButton.CommandArgument);
            lectureBAL.DeleteLecture(lectureId);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Palestra Excluida!", "alert('Palestra Excluida com Sucesso!');", true);

            LoadLecturesDataBound(lectureBAL.GetAdministratorLectureList());
        }
    }
}