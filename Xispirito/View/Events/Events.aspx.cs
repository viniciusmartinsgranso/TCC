using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xispirito.Controller;
using Xispirito.Models;
using AspImageButton = System.Web.UI.WebControls.ImageButton;

namespace Xispirito.View.Events
{
    public partial class Events : System.Web.UI.Page
    {
        private LectureBAL lectureBAL = new LectureBAL();
        private List<Lecture> upcomingLectures;

        private AreaBAL areaBAL = new AreaBAL();
        private List<Area> areaLectures;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int maxQuantityCards = 10;

                // Loading Upcoming Events.
                upcomingLectures = new List<Lecture>();
                upcomingLectures = lectureBAL.GetUpcomingLecturesList(maxQuantityCards);

                ListViewUpcomingEvents.DataSource = upcomingLectures;
                ListViewUpcomingEvents.DataBind();

                // Loading Area Events.
                areaLectures = new List<Area>();
                areaLectures = areaBAL.GetAreaList(maxQuantityCards);

                ListViewAreaEvents.DataSource = areaLectures;
                ListViewAreaEvents.DataBind();
            }
        }

        protected void ListViewEvents_ItemDataBound(object sender, ListViewItemEventArgs e)
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

        protected void ListViewAreaEvents_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Area area = (Area)e.Item.DataItem;

                ImageButton imageButton = (ImageButton)e.Item.FindControl("AreaEventImage");
                imageButton.ImageUrl = area.GetPicture();
                imageButton.PostBackUrl = "~/View/Registry/Registry.aspx?event=" + area.GetId();

                Label titleLabel = (Label)e.Item.FindControl("TitleArea");
                titleLabel.Text = area.GetName();
            }
        }

        protected void EventSearchImage_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/View/EventsSearch/EventsSearch.aspx?search=" + EventSearch.Text);
        }
    }
}