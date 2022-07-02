using System.Collections.Generic;
using Xispirito.DAL;
using Xispirito.Models;

namespace Xispirito.Controller
{
    public class ViewerCertificateBAL
    {
        private ViewerCertificateDAL viewerCertificateDAL { get; set; }

        public ViewerCertificateBAL()
        {
            viewerCertificateDAL = new ViewerCertificateDAL();
        }

        public List<ViewerCertificate> GetUserCertificates(string userEmail)
        {
            return viewerCertificateDAL.GetAllUserCertificates(userEmail);
        }

        public List<ViewerCertificate> GetUserCertificates(string userEmail, string lectureName)
        {
            return viewerCertificateDAL.GetFilterUserCertificates(userEmail, lectureName); ;
        }

        public void SaveViewerCertificate(string userEmail, int certificateId)
        {
            viewerCertificateDAL.RegisterUserCertificate(userEmail, certificateId);
        }
    }
}