using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xispirito.Controller;
using Xispirito.Models;

namespace Xispirito.View.RegisteredEvents
{
    public partial class RegisteredEvents : Page
    {
        UserType userType;

        private ViewerBAL viewerBAL = new ViewerBAL();
        private ViewerLectureBAL viewerLectureBAL = new ViewerLectureBAL();

        private SpeakerBAL speakerBAL = new SpeakerBAL();
        private SpeakerLectureBAL speakerLectureBAL = new SpeakerLectureBAL();

        private AdministratorBAL administratorBAL = new AdministratorBAL();
        private AdministratorLectureBAL administratorLectureBAL = new AdministratorLectureBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {
                    GetAccount(User.Identity.Name);
                    if (userType == UserType.Administrator)
                    {
                        LoadAdministratorEventsDataBound(administratorLectureBAL.GetUserLecturesRegistration(User.Identity.Name));
                    }
                    else if (userType == UserType.Speaker)
                    {
                        LoadSpeakerEventsDataBound(speakerLectureBAL.GetUserLecturesRegistration(User.Identity.Name));
                    }
                    else
                    {
                        LoadViewerEventsDataBound(viewerLectureBAL.GetUserLecturesRegistration(User.Identity.Name));
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

        private void LoadViewerEventsDataBound(List<ViewerLecture> viewerLectures)
        {
            string title = "Meus Eventos ";

            ListViewEvents.Items.Clear();
            if (viewerLectures != null)
            {
                MyEvents.Text = title + "(" + viewerLectures.Count + ")";
                ListViewEvents.DataSource = viewerLectures;
            }
            else
            {
                MyEvents.Text = title + "(0)";
            }
            ListViewEvents.DataBind();
        }

        private void LoadSpeakerEventsDataBound(List<SpeakerLecture> speakerLectures)
        {
            string title = "Meus Eventos ";

            ListViewEvents.Items.Clear();
            if (speakerLectures != null)
            {
                MyEvents.Text = title + "(" + speakerLectures.Count + ")";
                ListViewEvents.DataSource = speakerLectures;
            }
            else
            {
                MyEvents.Text = title + "(0)";
            }
            ListViewEvents.DataBind();
        }

        private void LoadAdministratorEventsDataBound(List<AdministratorLecture> administratorLectures)
        {
            string title = "Meus Eventos ";

            ListViewEvents.Items.Clear();
            if (administratorLectures != null)
            {
                MyEvents.Text = title + "(" + administratorLectures.Count + ")";
                ListViewEvents.DataSource = administratorLectures;
            }
            else
            {
                MyEvents.Text = title + "(0)";
            }
            ListViewEvents.DataBind();
        }

        protected void SearchEvents_Click(object sender, EventArgs e)
        {
            string search = FilterEvents.Text;

            if (search != null && User.Identity.IsAuthenticated)
            {
                GetAccount(User.Identity.Name);
                if (userType == UserType.Administrator)
                {
                    LoadAdministratorEventsDataBound(administratorLectureBAL.GetUserLecturesRegistration(User.Identity.Name, search));
                }
                else if (userType == UserType.Speaker)
                {
                    LoadSpeakerEventsDataBound(speakerLectureBAL.GetUserLecturesRegistration(User.Identity.Name, search));
                }
                else
                {
                    LoadViewerEventsDataBound(viewerLectureBAL.GetUserLecturesRegistration(User.Identity.Name, search));
                }
            }
        }

        protected void ListViewEvents_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                ImageButton eventImage = (ImageButton)e.Item.FindControl("EventImage");

                Label titleLabel = (Label)e.Item.FindControl("EventTitle");

                Label modalityLabel = (Label)e.Item.FindControl("EventModality");

                Image addressImage = (Image)e.Item.FindControl("AddressIcon");
                Label addressLabel = (Label)e.Item.FindControl("EventAddress");

                Label dateLabel = (Label)e.Item.FindControl("EventDate");

                Button watchButton = (Button)e.Item.FindControl("WatchLecture");

                Button cancelButton = (Button)e.Item.FindControl("CancelSubscription");

                if (userType == UserType.Administrator)
                {
                    AdministratorLecture administratorLecture = (AdministratorLecture)e.Item.DataItem;

                    eventImage.ImageUrl = administratorLecture.GetLecture().GetPicture();
                    eventImage.PostBackUrl = "~/View/Registry/Registry.aspx?event=" + administratorLecture.GetLecture().GetId().ToString();

                    titleLabel.Text = administratorLecture.GetLecture().GetName();

                    modalityLabel.Text = administratorLecture.GetLecture().GetModality();
                    modalityLabel.ForeColor = ModalityColor.GetModalityColor(administratorLecture.GetLecture().GetModality());

                    if (administratorLecture.GetLecture().GetModality() == Enum.GetName(typeof(Modality), 0))
                    {
                        addressImage.Visible = false;
                        addressLabel.Visible = false;
                    }
                    else
                    {
                        addressLabel.Text = administratorLecture.GetLecture().GetAddress();
                    }

                    dateLabel.Text = administratorLecture.GetLecture().GetDate().ToString("dd/MM/yyyy HH:mm");

                    DateTime endDateTime = administratorLecture.GetLecture().GetDate();
                    endDateTime = endDateTime.AddMinutes(administratorLecture.GetLecture().GetTime());
                    dateLabel.Text += " - " + endDateTime.ToString("dd/MM/yyyy HH:mm");

                    watchButton.PostBackUrl = "~/View/Lectures/WatchLectures/WatchLecture.aspx?lectureId=" + administratorLecture.GetLecture().GetId().ToString();
                    watchButton.BackColor = ModalityColor.GetModalityColor(administratorLecture.GetLecture().GetModality());

                    if (administratorLecture.GetLecture().GetModality() != Enum.GetName(typeof(Modality), 1))
                    {
                        if (administratorLecture.GetLecture().GetDate() <= DateTime.Now && administratorLecture.GetLecture().GetDate() < endDateTime)
                        {
                            watchButton.Visible = true;
                        }
                    }

                    cancelButton.CommandArgument = administratorLecture.GetLecture().GetId().ToString();
                }
                else if (userType == UserType.Speaker)
                {
                    SpeakerLecture speakerLecture = (SpeakerLecture)e.Item.DataItem;

                    eventImage.ImageUrl = speakerLecture.GetLecture().GetPicture();
                    eventImage.PostBackUrl = "~/View/Registry/Registry.aspx?event=" + speakerLecture.GetLecture().GetId().ToString();

                    titleLabel.Text = speakerLecture.GetLecture().GetName();

                    modalityLabel.Text = speakerLecture.GetLecture().GetModality();
                    modalityLabel.ForeColor = ModalityColor.GetModalityColor(speakerLecture.GetLecture().GetModality());

                    if (speakerLecture.GetLecture().GetModality() == Enum.GetName(typeof(Modality), 0))
                    {
                        addressImage.Visible = false;
                        addressLabel.Visible = false;
                    }
                    else
                    {
                        addressLabel.Text = speakerLecture.GetLecture().GetAddress();
                    }

                    dateLabel.Text = speakerLecture.GetLecture().GetDate().ToString("dd/MM/yyyy HH:mm");

                    DateTime endDateTime = speakerLecture.GetLecture().GetDate();
                    endDateTime = endDateTime.AddMinutes(speakerLecture.GetLecture().GetTime());
                    dateLabel.Text += " - " + endDateTime.ToString("dd/MM/yyyy HH:mm");

                    watchButton.PostBackUrl = "~/View/Lectures/WatchLectures/WatchLecture.aspx?lectureId=" + speakerLecture.GetLecture().GetId().ToString();
                    watchButton.BackColor = ModalityColor.GetModalityColor(speakerLecture.GetLecture().GetModality());

                    if (speakerLecture.GetLecture().GetModality() != Enum.GetName(typeof(Modality), 1))
                    {
                        if (speakerLecture.GetLecture().GetDate() <= DateTime.Now && speakerLecture.GetLecture().GetDate() < endDateTime)
                        {
                            watchButton.Visible = true;
                        }
                    }

                    cancelButton.CommandArgument = speakerLecture.GetLecture().GetId().ToString();
                }
                else
                {
                    ViewerLecture viewerLecture = (ViewerLecture)e.Item.DataItem;

                    eventImage.ImageUrl = viewerLecture.GetLecture().GetPicture();
                    eventImage.PostBackUrl = "~/View/Registry/Registry.aspx?event=" + viewerLecture.GetLecture().GetId().ToString();

                    titleLabel.Text = viewerLecture.GetLecture().GetName();

                    modalityLabel.Text = viewerLecture.GetLecture().GetModality();
                    modalityLabel.ForeColor = ModalityColor.GetModalityColor(viewerLecture.GetLecture().GetModality());

                    if (viewerLecture.GetLecture().GetModality() == Enum.GetName(typeof(Modality), 0))
                    {
                        addressImage.Visible = false;
                        addressLabel.Visible = false;
                    }
                    else
                    {
                        addressLabel.Text = viewerLecture.GetLecture().GetAddress();
                    }

                    dateLabel.Text = viewerLecture.GetLecture().GetDate().ToString("dd/MM/yyyy HH:mm");

                    DateTime endDateTime = viewerLecture.GetLecture().GetDate();
                    endDateTime = endDateTime.AddMinutes(viewerLecture.GetLecture().GetTime());
                    dateLabel.Text += " - " + endDateTime.ToString("dd/MM/yyyy HH:mm");

                    watchButton.PostBackUrl = "~/View/Lectures/WatchLectures/WatchLecture.aspx?lectureId=" + viewerLecture.GetLecture().GetId().ToString();
                    watchButton.BackColor = ModalityColor.GetModalityColor(viewerLecture.GetLecture().GetModality());

                    if (viewerLecture.GetLecture().GetModality() != Enum.GetName(typeof(Modality), 1))
                    {
                        if (viewerLecture.GetLecture().GetDate() <= DateTime.Now && viewerLecture.GetLecture().GetDate() < endDateTime)
                        {
                            watchButton.Visible = true;
                        }
                    }

                    cancelButton.CommandArgument = viewerLecture.GetLecture().GetId().ToString();
                }
            }
        }

        protected void CancelSubscription_Click(Object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                Button clickedButton = (Button)sender;
                int lectureId = Convert.ToInt32(clickedButton.CommandArgument);

                GetAccount(User.Identity.Name);

                if (userType == UserType.Administrator)
                {
                    _ = new AdministratorLecture();
                    AdministratorLecture objAdministratorLecture = administratorLectureBAL.GetUserLectureRegistration(User.Identity.Name, lectureId);
                    administratorLectureBAL.DeleteUserSubscription(objAdministratorLecture);

                    LoadAdministratorEventsDataBound(administratorLectureBAL.GetUserLecturesRegistration(User.Identity.Name));
                }
                else if (userType == UserType.Speaker)
                {
                    _ = new SpeakerLecture();
                    SpeakerLecture objSpeakerLecture = speakerLectureBAL.GetUserLectureRegistration(User.Identity.Name, lectureId);
                    speakerLectureBAL.DeleteUserSubscription(objSpeakerLecture);

                    LoadSpeakerEventsDataBound(speakerLectureBAL.GetUserLecturesRegistration(User.Identity.Name));
                }
                else
                {
                    _ = new ViewerLecture();
                    ViewerLecture objViewerLecture = viewerLectureBAL.GetUserLectureRegistration(User.Identity.Name, lectureId);
                    viewerLectureBAL.DeleteUserSubscription(objViewerLecture);

                    LoadViewerEventsDataBound(viewerLectureBAL.GetUserLecturesRegistration(User.Identity.Name));
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Inscrição Cancelada!", "alert('Sua Inscrição a Palestra foi Cancelada!');", true);
                FilterEvents.Text = "";
            }
        }
    }
}