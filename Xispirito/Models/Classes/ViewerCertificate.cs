namespace Xispirito.Models
{
    public class ViewerCertificate
    {
        private int CertificateKey { get; set; }
        private Viewer Viewer { get; set; }
        private Lecture Lecture { get; set; }
        private Certificate Certificate { get; set; }

        public ViewerCertificate()
        {

        }

        public ViewerCertificate(int certificateKey, Viewer viewer, Lecture lecture, Certificate certificate)
        {
            CertificateKey = certificateKey;
            Viewer = viewer;
            Lecture = lecture;
            Certificate = certificate;
        }

        public int GetCertificateKey()
        {
            return CertificateKey;
        }

        public Viewer GetViewer()
        {
            return Viewer;
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