using Xispirito.DAL;
using Xispirito.Models;

namespace Xispirito.Controller
{
    public class ViewerBAL
    {
        private ViewerDAL viewerDAL { get; set; }

        public ViewerBAL()
        {
            viewerDAL = new ViewerDAL();
        }

        public void SignUp(string nome, string email, string password)
        {
            password = Cryptography.GetMD5Hash(password);
            Viewer viewer = new Viewer(nome, email, "", password, true);

            viewerDAL.Insert(viewer);
        }

        public bool VerifyAccount(string email)
        {
            Viewer objViewer = new Viewer();
            objViewer = viewerDAL.SearchEmail(email);

            bool accountFound = false;
            if (objViewer != null)
            {
                accountFound = true;
            }

            return accountFound;
        }

        public bool VerifyAccount(string email, string password)
        {
            Viewer objViewer = new Viewer();
            objViewer = viewerDAL.SearchEmail(email, Cryptography.GetMD5Hash(password));

            bool accountFound = false;
            if (objViewer != null)
            {
                accountFound = true;
            }

            return accountFound;
        }

        public Viewer GetAccount(string email)
        {
            return viewerDAL.SearchEmail(email);
        }

        public Viewer GetAccount(string email, string password)
        {
            return viewerDAL.SearchEmail(email, Cryptography.GetMD5Hash(password));
        }

        public bool VerifyAccountStatus(Viewer objViewer)
        {
            bool accountActive = false;
            if (objViewer.GetIsActive() == true)
            {
                accountActive = true;
            }
            return accountActive;
        }

        public void UpdateViewerAccount(Viewer objViewer)
        {
            viewerDAL.Update(objViewer);
        }
    }
}