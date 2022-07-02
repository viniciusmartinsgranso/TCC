using System.Collections.Generic;
using Xispirito.DAL;
using Xispirito.Models;

namespace Xispirito.Controller
{
    public class SpeakerWatchedLectureBAL
    {
        private SpeakerWatchedLectureDAL speakerWatchedLectureDAL { get; set; }

        private SpeakerBAL speakerBAL = new SpeakerBAL();
        private LectureBAL lectureBAL = new LectureBAL();

        public SpeakerWatchedLectureBAL()
        {
            speakerWatchedLectureDAL = new SpeakerWatchedLectureDAL();
        }

        public void RegisterUserToLecture(string userEmail, int idLecture)
        {
            SpeakerWatchedLecture objSpeakerWatchedLecture = new SpeakerWatchedLecture(speakerBAL.GetAccount(userEmail), lectureBAL.GetLecture(idLecture));
            speakerWatchedLectureDAL.RegisterUserAttendance(objSpeakerWatchedLecture);
        }

        public bool VerifyRegisterToLecture(string userEmail, int idLecture)
        {
            SpeakerWatchedLecture objSpeakerWatchedLecture = new SpeakerWatchedLecture(speakerBAL.GetAccount(userEmail), lectureBAL.GetLecture(idLecture));
            return speakerWatchedLectureDAL.VerifyRegisterToLecture(objSpeakerWatchedLecture);
        }

        public void DeleteUserAttendance(string userEmail)
        {
            speakerWatchedLectureDAL.DeleteUserAttendance(userEmail);
        }

        public List<SpeakerWatchedLecture> GetUsersWhoAttended(int lectureId)
        {
            return speakerWatchedLectureDAL.GetUsersWhoAttended(lectureId);
        }
    }
}