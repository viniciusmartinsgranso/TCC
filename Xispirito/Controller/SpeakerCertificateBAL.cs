using System.Collections.Generic;
using Xispirito.Controller;
using Xispirito.DAL;
using Xispirito.Models;

namespace Xispirito.Controller
{
    public class SpeakerCertificateBAL
    {
        private SpeakerCertificateDAL speakerCertificateDAL { get; set; }

        public SpeakerCertificateBAL()
        {
            speakerCertificateDAL = new SpeakerCertificateDAL();
        }

        public List<SpeakerCertificate> GetUserCertificates(string userEmail)
        {
            return speakerCertificateDAL.GetAllUserCertificates(userEmail);
        }

        public List<SpeakerCertificate> GetUserCertificates(string userEmail, string lectureName)
        {
            return speakerCertificateDAL.GetFilterUserCertificates(userEmail, lectureName);
        }

        public void SaveViewerCertificate(string userEmail, int certificateId)
        {
            speakerCertificateDAL.RegisterUserCertificate(userEmail, certificateId);
        }
    }
}