using Xispirito.DAL;
using Xispirito.Models;

namespace Xispirito.Controller
{
    public class CertificateBAL
    {
        private CertificateDAL certificateDAL { get; set; }

        public CertificateBAL()
        {
            certificateDAL = new CertificateDAL();
        }

        public Certificate GetCertificateById(int certificateId)
        {
            Certificate certificate = new Certificate();
            certificate.SetId(certificateId);
            certificate = certificateDAL.Select(certificate.GetId());

            return certificate;
        }
    }
}