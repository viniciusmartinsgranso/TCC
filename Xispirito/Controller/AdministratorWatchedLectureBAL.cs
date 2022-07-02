using System.Collections.Generic;
using Xispirito.DAL;
using Xispirito.Models;

namespace Xispirito.Controller
{
    public class AdministratorWatchedLectureBAL
    {
        private AdministratorWatchedLectureDAL administratorWatchedLectureDAL { get; set; }

        private AdministratorBAL administratorBAL = new AdministratorBAL();
        private LectureBAL lectureBAL = new LectureBAL();

        public AdministratorWatchedLectureBAL()
        {
            administratorWatchedLectureDAL = new AdministratorWatchedLectureDAL();
        }

        public void RegisterUserToLecture(string userEmail, int idLecture)
        {
            AdministratorWatchedLecture objAdministratorWatchedLecture = new AdministratorWatchedLecture(administratorBAL.GetAccount(userEmail), lectureBAL.GetLecture(idLecture));
            administratorWatchedLectureDAL.RegisterUserAttendance(objAdministratorWatchedLecture);
        }

        public bool VerifyRegisterToLecture(string userEmail, int idLecture)
        {
            AdministratorWatchedLecture objAdministratorWatchedLecture = new AdministratorWatchedLecture(administratorBAL.GetAccount(userEmail), lectureBAL.GetLecture(idLecture));
            return administratorWatchedLectureDAL.VerifyRegisterToLecture(objAdministratorWatchedLecture);
        }

        public void DeleteUserAttendance(string userEmail)
        {
            administratorWatchedLectureDAL.DeleteUserAttendance(userEmail);
        }

        public List<AdministratorWatchedLecture> GetUsersWhoAttended(int lectureId)
        {
            return administratorWatchedLectureDAL.GetUsersWhoAttended(lectureId);
        }
    }
}