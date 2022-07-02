using System.Collections.Generic;
using Xispirito.DAL;
using Xispirito.Models;

namespace Xispirito.Controller
{
    public class ViewerLectureBAL
    {
        private ViewerLectureDAL viewerLectureDAL { get; set; }

        public ViewerLectureBAL()
        {
            viewerLectureDAL = new ViewerLectureDAL();
        }

        public void RegisterUserToLecture(ViewerLecture objViewerLecture)
        {
            viewerLectureDAL.RegisterUserToLecture(objViewerLecture);
        }

        public bool VerifyUserAlreadyRegistered(ViewerLecture objViewerLecture)
        {
            return viewerLectureDAL.VerifyUserAlreadyRegistered(objViewerLecture); ;
        }

        public void DeleteUserSubscription(ViewerLecture objViewerLecture)
        {
            viewerLectureDAL.DeleteUserSubscription(objViewerLecture);
        }

        public ViewerLecture GetUserLectureRegistration(string userEmail, int idLecture)
        {
            return viewerLectureDAL.GetUserLectureRegistration(userEmail, idLecture);
        }

        public int GetLectureRegistrationsNumber(int idLecture)
        {
            return viewerLectureDAL.GetLectureRegistrations(idLecture);
        }

        public List<ViewerLecture> GetUserLecturesRegistration(string userEmail)
        { 
            return viewerLectureDAL.GetUserLecturesRegistration(userEmail); ;
        }

        public List<ViewerLecture> GetUserLecturesRegistration(string userEmail, string search)
        {
            return viewerLectureDAL.GetUserLecturesRegistration(userEmail, search);
        }
    }
}