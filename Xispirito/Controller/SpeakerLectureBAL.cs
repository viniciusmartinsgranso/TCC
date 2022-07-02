using System.Collections.Generic;
using Xispirito.DAL;
using Xispirito.Models;

namespace Xispirito.Controller
{
    public class SpeakerLectureBAL
    {
        private SpeakerLectureDAL speakerLectureDAL { get; set; }

        public SpeakerLectureBAL()
        {
            speakerLectureDAL = new SpeakerLectureDAL();
        }

        public void RegisterUserToLecture(SpeakerLecture objSpeakerLecture)
        {
            speakerLectureDAL.RegisterUserToLecture(objSpeakerLecture);
        }

        public bool VerifyUserAlreadyRegistered(SpeakerLecture objSpeakerLecture)
        {
            return speakerLectureDAL.VerifyUserAlreadyRegistered(objSpeakerLecture); ;
        }

        public void DeleteUserSubscription(SpeakerLecture objSpeakerLecture)
        {
            speakerLectureDAL.DeleteUserSubscription(objSpeakerLecture);
        }

        public SpeakerLecture GetUserLectureRegistration(string userEmail, int idLecture)
        {
            return speakerLectureDAL.GetUserLectureRegistration(userEmail, idLecture);
        }

        public int GetLectureRegistrationsNumber(int idLecture)
        {
            return speakerLectureDAL.GetLectureRegistrations(idLecture);
        }

        public List<SpeakerLecture> GetUserLecturesRegistration(string userEmail)
        {
            return speakerLectureDAL.GetUserLecturesRegistration(userEmail);
        }

        public List<SpeakerLecture> GetUserLecturesRegistration(string userEmail, string search)
        {
            return speakerLectureDAL.GetUserLecturesRegistration(userEmail, search);
        }
    }
}