using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xispirito.Controller;
using Xispirito.Models;
using AspImageButton = System.Web.UI.WebControls.ImageButton;

namespace Xispirito.View.HomeWithMaster
{

    public partial class Home : Page
    {
        private LectureBAL lectureBAL = new LectureBAL();

        private List<Lecture> upcomingLectures;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int maxQuantityCards = 6;

                // Loading Upcoming Events.
                upcomingLectures = new List<Lecture>();
                upcomingLectures = lectureBAL.GetLecturesList(maxQuantityCards);

                ListViewUpcomingEvents.DataSource = upcomingLectures;
                ListViewUpcomingEvents.DataBind();
            }
        }
        protected void ListViewUpcomingEvents_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Lecture lecture = (Lecture)e.Item.DataItem;

                ImageButton imageButton = (ImageButton)e.Item.FindControl("UpcomingEventImage");
                imageButton.ImageUrl = lecture.GetPicture();
                imageButton.PostBackUrl = "~/View/Registry/Registry.aspx?event=" + lecture.GetId();

                Label titleLabel = (Label)e.Item.FindControl("TitleUpcomingEvent");
                titleLabel.Text = lecture.GetName();

                Label typeLabel = (Label)e.Item.FindControl("TypeUpcomingEvent");
                typeLabel.Text = lecture.GetModality();
                typeLabel.BackColor = ModalityColor.GetModalityColor(lecture.GetModality());

                Label timeLabel = (Label)e.Item.FindControl("TimeUpcomingEvent");
                timeLabel.Text = lecture.GetTime().ToString() + " Min";
            }
        }
    }
}