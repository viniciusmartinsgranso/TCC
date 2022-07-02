namespace Xispirito.Models
{
    public class AdministratorCertificate
    {
        private int CertificateKey { get; set; }
        private Administrator Administrator { get; set; }
        private Lecture Lecture { get; set; }
        private Certificate Certificate { get; set; }

        public AdministratorCertificate()
        {

        }

        public AdministratorCertificate(int certificateKey, Administrator administrator, Lecture lecture, Certificate certificate)
        {
            CertificateKey = certificateKey;
            Administrator = administrator;
            Lecture = lecture;
            Certificate = certificate;
        }

        public int GetCertificateKey()
        {
            return CertificateKey;
        }

        public Administrator GetAdministrator()
        {
            return Administrator;
        }

        public Lecture GetLecture()
        {
            return Lecture;
        }

        public Certificate GetCertificate()
        {
            return Certificate;
        }
    }
}