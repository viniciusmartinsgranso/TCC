using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xispirito.Models
{
    public class Certificate : BaseEntity
    {
        private int CertificateId { get; set; }
        private string CertificateModelDirectory { get; set; }
        private int LectureId { get; set; }

        public Certificate()
        {

        }

        public Certificate(int certificateId, string certificateModelDirectory, int lectureId, bool isActive)
        {
            CertificateId = certificateId;
            CertificateModelDirectory = certificateModelDirectory;
            LectureId = lectureId;
            IsActive = isActive;
        }

        public int GetId()
        {
            return CertificateId;
        }

        public void SetId(int certificateId)
        {
            CertificateId = certificateId;
        }

        public string GetCertificateModelDirectory()
        {
            return CertificateModelDirectory;
        }

        public int GetLectureId()
        {
            return LectureId;
        }
    }
}