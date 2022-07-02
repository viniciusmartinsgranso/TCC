using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xispirito.Controller;
using Xispirito.Models;

namespace Xispirito.View.Lectures.ViewLectures
{
    public partial class WatchLecture : Page
    {
        UserType userType;

        private ViewerBAL viewerBAL = new ViewerBAL();
        private ViewerLectureBAL viewerLectureBAL = new ViewerLectureBAL();
        private ViewerWatchedLectureBAL viewerWatchedLectureBAL = new ViewerWatchedLectureBAL();

        private SpeakerBAL speakerBAL = new SpeakerBAL();
        private SpeakerLectureBAL speakerLectureBAL = new SpeakerLectureBAL();
        private SpeakerWatchedLectureBAL speakerWatchedLectureBAL = new SpeakerWatchedLectureBAL();

        private AdministratorBAL administratorBAL = new AdministratorBAL();
        private AdministratorLectureBAL administratorLectureBAL = new AdministratorLectureBAL();
        private AdministratorWatchedLectureBAL administratorWatchedLectureBAL = new AdministratorWatchedLectureBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["lectureId"]))
                    {
                        int lectureId = Convert.ToInt32(Request.QueryString["lectureId"]);

                        bool userFound = FindUser(lectureId); ;
                        if (!userFound)
                        {
                            Response.Redirect("~/View/Home/Home.aspx");
                        }
                        else
                        {
                            LoadUsers(lectureId);
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
        private BaseUser GetAccount(string email)
        {
            BaseUser baseUser = new BaseUser();
            if (!viewerBAL.VerifyAccount(email))
            {
                if (!speakerBAL.VerifyAccount(email))
                {
                    if (administratorBAL.VerifyAccount(email))
                    {
                        userType = UserType.Administrator;
                        baseUser = administratorBAL.GetAccount(email);
                    }
                }
                else
                {
                    userType = UserType.Speaker;
                    baseUser = speakerBAL.GetAccount(email);
                }
            }
            else
            {
                userType = UserType.Viewer;
                baseUser = viewerBAL.GetAccount(email);
            }
            return baseUser;
        }

        private void LoadUsers(int lectureId)
        {
            List<BaseUser> baseUsers = new List<BaseUser>();

            List<AdministratorWatchedLecture> administratorWatchedLectures = new List<AdministratorWatchedLecture>();
            administratorWatchedLectures = administratorWatchedLectureBAL.GetUsersWhoAttended(lectureId);
            if (administratorWatchedLectures != null)
            {
                foreach (AdministratorWatchedLecture administrator in administratorWatchedLectures)
                {
                    BaseUser user = new BaseUser();
                    user.SetName(administrator.GetAdministrator().GetName());
                    user.SetPicture(administrator.GetAdministrator().GetPicture());
                    baseUsers.Add(user);
                }
            }

            List<SpeakerWatchedLecture> speakerWatchedLecture = new List<SpeakerWatchedLecture>();
            speakerWatchedLecture = speakerWatchedLectureBAL.GetUsersWhoAttended(lectureId);
            if (speakerWatchedLecture != null)
            {
                foreach (SpeakerWatchedLecture speaker in speakerWatchedLecture)
                {
                    BaseUser user = new BaseUser();
                    user.SetName(speaker.GetSpeaker().GetName());
                    user.SetPicture(speaker.GetSpeaker().GetPicture());
                    baseUsers.Add(user);
                }
            }

            List<ViewerWatchedLecture> viewerWatchedLecture = new List<ViewerWatchedLecture>();
            viewerWatchedLecture = viewerWatchedLectureBAL.GetUsersWhoAttended(lectureId);
            if (viewerWatchedLecture != null)
            {
                foreach (ViewerWatchedLecture viewer in viewerWatchedLecture)
                {
                    BaseUser user = new BaseUser();
                    user.SetName(viewer.GetViewer().GetName());
                    user.SetPicture(viewer.GetViewer().GetPicture());
                    baseUsers.Add(user);
                }
            }

            LoadUsersDataBound(baseUsers);
        }

        private bool FindUser(int lectureId)
        {
            GetAccount(User.Identity.Name);

            bool userFound = false;
            if (userType == UserType.Administrator)
            {
                if (administratorLectureBAL.GetUserLectureRegistration(User.Identity.Name, lectureId) != null)
                {
                    if (!administratorWatchedLectureBAL.VerifyRegisterToLecture(User.Identity.Name, lectureId))
                    {
                        administratorWatchedLectureBAL.RegisterUserToLecture(User.Identity.Name, lectureId);
                    }
                    userFound = true;
                }
            }
            else if (userType == UserType.Speaker)
            {
                if (speakerLectureBAL.GetUserLectureRegistration(User.Identity.Name, lectureId) != null)
                {
                    if (!speakerWatchedLectureBAL.VerifyRegisterToLecture(User.Identity.Name, lectureId))
                    {
                        speakerWatchedLectureBAL.RegisterUserToLecture(User.Identity.Name, lectureId);
                    }
                    userFound = true;
                }
            }
            else
            {
                if (viewerLectureBAL.GetUserLectureRegistration(User.Identity.Name, lectureId) != null)
                {
                    if (!viewerWatchedLectureBAL.VerifyRegisterToLecture(User.Identity.Name, lectureId))
                    {
                        viewerWatchedLectureBAL.RegisterUserToLecture(User.Identity.Name, lectureId);
                    }
                    userFound = true;
                }
            }
            return userFound;
        }

        protected void UsersListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                BaseUser users = (BaseUser)e.Item.DataItem;

                Image userEmail = (Image)e.Item.FindControl("UserImage");
                userEmail.ImageUrl = users.GetPicture();

                Label userName = (Label)e.Item.FindControl("UserName");
                userName.Text = users.GetName();
            }
        }

        private void LoadUsersDataBound(List<BaseUser> baseUsers)
        {
            UsersListView.Items.Clear();
            if (baseUsers != null)
            {
                UsersListView.DataSource = baseUsers;
            }
            UsersListView.DataBind();
        }
    }
}