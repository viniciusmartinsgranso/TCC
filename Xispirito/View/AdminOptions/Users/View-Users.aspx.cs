using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xispirito.Controller;
using Xispirito.Models;

namespace Xispirito.View.AdminOptions
{
    public partial class Admin_Options : Page
    {
        private Administrator administrator = new Administrator();
        private AdministratorBAL administratorBAL = new AdministratorBAL();

        private SpeakerBAL speakerBAL = new SpeakerBAL();

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
                        LoadUsersDataBound(speakerBAL.GetAdministratorSpeakerList());
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

        protected void ListViewUsers_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                BaseUser users = (BaseUser)e.Item.DataItem;

                Label userName = (Label)e.Item.FindControl("UserName");
                userName.Text = users.GetName();

                Label userEmail = (Label)e.Item.FindControl("UserEmail");
                userEmail.Text = users.GetEmail();

                Button editLecture = (Button)e.Item.FindControl("EditUser");
                //editLecture.PostBackUrl = "~/View/Lectures/CRUD/Lecture-CRUD.aspx?lectureId=" + users.GetId();

                Button deleteLecture = (Button)e.Item.FindControl("DeleteUser");
                deleteLecture.CommandArgument = users.GetId().ToString();
            }
        }

        private void LoadUsersDataBound(List<Speaker> speakers)
        {
            ListViewUsers.Items.Clear();
            if (speakers != null)
            {
                ListViewUsers.DataSource = speakers;
            }
            ListViewUsers.DataBind();
        }

        protected void SearchUsers_Click(object sender, EventArgs e)
        {
            string search = FilterUsers.Text;

            if (search != null)
            {
                LoadUsersDataBound(speakerBAL.GetAdministratorSpeakerList(search));
            }
        }

        protected void DeleteUser_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            int speakerId = Convert.ToInt32(clickedButton.CommandArgument);
            speakerBAL.DeleteSpeaker(speakerId);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Usuário Excluido!", "alert('Usuário Excluido com Sucesso!');", true);

            LoadUsersDataBound(speakerBAL.GetAdministratorSpeakerList());
            FilterUsers.Text = "";
        }
    }
}