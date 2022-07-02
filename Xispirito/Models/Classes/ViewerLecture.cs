namespace Xispirito.Models
{
    public class ViewerLecture : BaseEntity
    {
        private Viewer Viewer { get; set; }
        private Lecture Lecture { get; set; }

        public ViewerLecture()
        {

        }

        public ViewerLecture(Viewer viewer, Lecture lecture)
        {
            Viewer = viewer;
            Lecture = lecture;
        }

        public Viewer GetViewer()
        {
            return Viewer;
        }

        public Lecture GetLecture()
        {
            return Lecture;
        }
    }
}