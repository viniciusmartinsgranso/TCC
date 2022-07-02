namespace Xispirito.Models
{
    public class SpeakerCertificate
    {
        private int CertificateKey { get; set; }
        private Speaker Speaker { get; set; }
        private Lecture Lecture { get; set; }
        private Certificate Certificate { get; set; }

        public SpeakerCertificate()
        {

        }

        public SpeakerCertificate(int certificateKey, Speaker speaker, Lecture lecture, Certificate certificate)
        {
            CertificateKey = certificateKey;
            Speaker = speaker;
            Lecture = lecture;
            Certificate = certificate;
        }

        public int GetCertificateKey()
        {
            return CertificateKey;
        }

        public Speaker GetSpeaker()
        {
            return Speaker;
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