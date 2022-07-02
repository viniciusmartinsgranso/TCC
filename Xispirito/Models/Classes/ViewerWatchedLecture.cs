namespace Xispirito.Models
{
    public class ViewerWatchedLecture
    {
        private Viewer Viewer { get; set; }
        private Lecture Lecture { get; set; }

        public ViewerWatchedLecture()
        {

        }

        public ViewerWatchedLecture(Viewer viewer, Lecture lecture)
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