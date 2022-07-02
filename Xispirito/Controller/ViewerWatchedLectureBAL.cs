using System.Collections.Generic;
using Xispirito.DAL;
using Xispirito.Models;

namespace Xispirito.Controller
{
    public class ViewerWatchedLectureBAL
    {
        private ViewerWatchedLectureDAL viewerWatchedLectureDAL { get; set; }

        private ViewerBAL viewerBAL = new ViewerBAL();
        private LectureBAL lectureBAL = new LectureBAL();


        public ViewerWatchedLectureBAL()
        {
            viewerWatchedLectureDAL = new ViewerWatchedLectureDAL();
        }

        public void RegisterUserToLecture(string userEmail, int idLecture)
        {
            ViewerWatchedLecture objViewerWatchedLecture = new ViewerWatchedLecture(viewerBAL.GetAccount(userEmail), lectureBAL.GetLecture(idLecture));
            viewerWatchedLectureDAL.RegisterUserAttendance(objViewerWatchedLecture);
        }

        public bool VerifyRegisterToLecture(string userEmail, int idLecture)
        {
            ViewerWatchedLecture objViewerWatchedLecture = new ViewerWatchedLecture(viewerBAL.GetAccount(userEmail), lectureBAL.GetLecture(idLecture));
            return viewerWatchedLectureDAL.VerifyRegisterToLecture(objViewerWatchedLecture);
        }

        public void DeleteUserAttendance(string userEmail)
        {
            viewerWatchedLectureDAL.DeleteUserAttendance(userEmail);
        }

        public List<ViewerWatchedLecture> GetUsersWhoAttended(int lectureId)
        {
            return viewerWatchedLectureDAL.GetUsersWhoAttended(lectureId);
        }
    }
}