using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xispirito.Controller;
using Xispirito.Models;

namespace Xispirito.View.EventsSearch
{
    public partial class EventsSearch : Page
    {
        private LectureBAL lectureBAL = new LectureBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Loading Events.
                LoadEventsDataBound(lectureBAL.GetLecturesList());
            }
        }

        private void LoadEventsDataBound(List<Lecture> lecturesList)
        {
            ListViewAllEvents.Items.Clear();
            ListViewAllEvents.DataSource = lecturesList;
            ListViewAllEvents.DataBind();
        }

        protected void EventSearchImage_Click(object sender, ImageClickEventArgs e)
        {
            string search = EventSearch.Text;

            if (search != null)
            {
                LoadEventsDataBound(lectureBAL.SearchLecturesByName(search));
            }
        }

        protected void ListViewAllEvents_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Lecture lecture = (Lecture)e.Item.DataItem;

                ImageButton imageButton = (ImageButton)e.Item.FindControl("AllEventsImage");
                imageButton.ImageUrl = lecture.GetPicture();
                imageButton.PostBackUrl = "~/View/Registry/Registry.aspx?event=" + lecture.GetId();

                Label titleLabel = (Label)e.Item.FindControl("TitleAllEvents");
                titleLabel.Text = lecture.GetName();

                Label typeLabel = (Label)e.Item.FindControl("TypeAllEvents");
                typeLabel.Text = lecture.GetModality();
                typeLabel.BackColor = ModalityColor.GetModalityColor(lecture.GetModality());

                Label timeLabel = (Label)e.Item.FindControl("TimeAllEvents");
                timeLabel.Text = lecture.GetTime().ToString() + " Min";
            }
        }
    }
}