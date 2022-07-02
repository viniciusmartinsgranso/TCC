using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using Xispirito.Controller;

namespace Xispirito.Models
{

    public static class CertificateGenerator
    {
        private static string outsideProjectPath = ConfigurationManager.AppSettings["XispiritoPath"];
        private static string insideProjectPath = @"UsersData\";
        private static string certificateFolder = @"\Certificates\";
        private static string extension = ".pdf";

        public static void GenerateViewerCertificate(Viewer viewer, Lecture lecture, Certificate certificate)
        {
            string path = @"Viewers\";
            string userEmail = Cryptography.GetMD5Hash(viewer.GetEmail());
            string fileName = Cryptography.GetMD5Hash(viewer.GetEmail() + certificate.GetId().ToString());
            string fullPath = outsideProjectPath + insideProjectPath + path + userEmail + certificateFolder + fileName + extension;

            string folderPath = outsideProjectPath + insideProjectPath + path + userEmail;
            if (!File.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!File.Exists(folderPath + certificateFolder))
            {
                Directory.CreateDirectory(folderPath + certificateFolder);
            }

            GeneratePDF(fileName, fullPath, viewer, lecture, certificate);

            SaveViewerCertificate(viewer.GetEmail(), certificate.GetId());

            string inputPath = outsideProjectPath + insideProjectPath + path + userEmail + certificateFolder + fileName;
            string outputPath = inputPath;

            ConvertPdfToPng(inputPath, outputPath);
        }

        public static void GenerateSpeakerCertificate(Speaker speaker, Lecture lecture, Certificate certificate)
        {
            string path = @"Speakers\";
            string userEmail = Cryptography.GetMD5Hash(speaker.GetEmail());
            string fileName = Cryptography.GetMD5Hash(speaker.GetEmail() + certificate.GetId().ToString());
            string fullPath = outsideProjectPath + insideProjectPath + path + userEmail + certificateFolder + fileName + extension;

            string folderPath = outsideProjectPath + insideProjectPath + path + userEmail;
            if (!File.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!File.Exists(folderPath + certificateFolder))
            {
                Directory.CreateDirectory(folderPath + certificateFolder);
            }

            GeneratePDF(fileName, fullPath, speaker, lecture, certificate);

            SaveSpeakerCertificate(speaker.GetEmail(), certificate.GetId());

            string inputPath = outsideProjectPath + insideProjectPath + path + userEmail + certificateFolder + fileName;
            string outputPath = inputPath;

            ConvertPdfToPng(inputPath, outputPath);
        }

        public static void GenerateAdministratorCertificate(Administrator administrator, Lecture lecture, Certificate certificate)
        {
            string path = @"Administrators\";
            string userEmail = Cryptography.GetMD5Hash(administrator.GetEmail());
            string fileName = Cryptography.GetMD5Hash(administrator.GetEmail() + certificate.GetId().ToString());
            string fullPath = outsideProjectPath + insideProjectPath + path + userEmail + certificateFolder + fileName + extension;

            string folderPath = outsideProjectPath + insideProjectPath + path + userEmail;
            if (!File.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            if (!File.Exists(folderPath + certificateFolder))
            {
                Directory.CreateDirectory(folderPath + certificateFolder);
            }

            GeneratePDF(fileName, fullPath, administrator, lecture, certificate);

            SaveAdministratorCertificate(administrator.GetEmail(), certificate.GetId());

            string inputPath = outsideProjectPath + insideProjectPath + path + userEmail + certificateFolder + fileName;
            string outputPath = inputPath;

            ConvertPdfToPng(inputPath, outputPath);
        }

        private static void GeneratePDF(string fileName, string fullPath, BaseUser baseUser, Lecture lecture, Certificate certificate)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            page.Size = PdfSharp.PageSize.Statement;
            page.Height = 600;
            page.Width = 800;

            XGraphics gfx = XGraphics.FromPdfPage(page);

            gfx.DrawImage(XImage.FromFile(outsideProjectPath + certificate.GetCertificateModelDirectory()), 0, 0, page.Width, page.Height);

            XFont font = new XFont("Arial", 14, XFontStyle.Regular);
            XFont boldFont = new XFont("Arial", 12, XFontStyle.Bold);

            double textPositionY = 0;
            string text = "Certificamos que " + baseUser.GetName() + ", participou da atividade:";
            gfx.DrawString(text,
                font, XBrushes.Black,
                new XRect(0, textPositionY, page.Width, page.Height),
                XStringFormats.Center
            );

            XSize nextText = gfx.MeasureString(text, font);
            textPositionY += nextText.Height;

            text = lecture.GetName() + ",";
            gfx.DrawString(text,
                boldFont, XBrushes.Black,
                new XRect(0, textPositionY, page.Width, page.Height),
                XStringFormats.Center
            );

            nextText = gfx.MeasureString(text, boldFont);
            textPositionY += nextText.Height;

            text = "realizada em " + lecture.GetDate().ToString("dd/MM/yyyy") + ", contabilizando a carga horária total de " + lecture.GetTime() + " Minutos.";
            gfx.DrawString(text,
                font, XBrushes.Black,
                new XRect(0, textPositionY, page.Width, page.Height),
                XStringFormats.Center
            );

            nextText = gfx.MeasureString(text, font);
            textPositionY += nextText.Height * 3;

            boldFont = new XFont("Arial", 16, XFontStyle.Bold);

            text = "Sorocaba, " + lecture.GetDate().ToString("dd") + " de " + lecture.GetDate().ToString("MMMM") + " de " + lecture.GetDate().ToString("yyyy") + ".";
            gfx.DrawString(text,
                boldFont, XBrushes.Black,
                new XRect(0, textPositionY, page.Width, page.Height),
                XStringFormats.Center
            );

            // Certificate Key.
            gfx.DrawString(fileName,
                boldFont, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormats.BottomLeft
            );
            document.Save(fullPath);
            document.Close();
        }

        private static void SaveViewerCertificate(string userEmail, int certificateId)
        {
            ViewerCertificateBAL viewerCertificateBAL = new ViewerCertificateBAL();
            viewerCertificateBAL.SaveViewerCertificate(userEmail, certificateId);
        }

        private static void SaveSpeakerCertificate(string userEmail, int certificateId)
        {
            SpeakerCertificateBAL speakerCertificateBAL = new SpeakerCertificateBAL();
            speakerCertificateBAL.SaveViewerCertificate(userEmail, certificateId);
        }

        private static void SaveAdministratorCertificate(string userEmail, int certificateId)
        {
            AdministratorCertificateBAL administratorCertificateBAL = new AdministratorCertificateBAL();
            administratorCertificateBAL.SaveViewerCertificate(userEmail, certificateId);
        }

        private static void ConvertPdfToPng(string inputFilePath, string outputFilePath)
        {
            string arguments = string.Format(@"-density 300 {0}.pdf {1}.png", inputFilePath, outputFilePath);
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                Arguments = arguments,
                FileName = ConfigurationManager.AppSettings["ImageMagickPath"]
            };
            startInfo.UseShellExecute = false;

            Process.Start(startInfo).WaitForExit();
        }
    }
}