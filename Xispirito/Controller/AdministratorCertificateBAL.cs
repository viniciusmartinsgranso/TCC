using System.Collections.Generic;
using Xispirito.DAL;
using Xispirito.Models;

namespace Xispirito.Controller
{
    public class AdministratorCertificateBAL
    {
        private AdministratorCertificateDAL administratorCertificateDAL { get; set; }

        public AdministratorCertificateBAL()
        {
            administratorCertificateDAL = new AdministratorCertificateDAL();
        }

        public List<AdministratorCertificate> GetUserCertificates(string userEmail)
        {
            return administratorCertificateDAL.GetAllUserCertificates(userEmail);
        }

        public List<AdministratorCertificate> GetUserCertificates(string userEmail, string lectureName)
        {
            return administratorCertificateDAL.GetFilterUserCertificates(userEmail, lectureName);
        }

        public void SaveViewerCertificate(string userEmail, int certificateId)
        {
            administratorCertificateDAL.RegisterUserCertificate(userEmail, certificateId);
        }
    }
}