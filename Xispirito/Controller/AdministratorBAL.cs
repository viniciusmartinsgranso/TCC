using Xispirito.DAL;
using Xispirito.Models;

namespace Xispirito.Controller
{
    public class AdministratorBAL
    {
        private AdministratorDAL administratorDAL { get; set; }

        public AdministratorBAL()
        {
            administratorDAL = new AdministratorDAL();
        }

        public bool SignIn(string email, string encryptedPassword)
        {
            BaseUser baseUser = new BaseUser();
            baseUser.SetEmail(email);
            baseUser.SetEncryptedPassword(Cryptography.GetMD5Hash(encryptedPassword));

            bool emailFound = false;
            emailFound = administratorDAL.SignIn(baseUser.GetEmail(), baseUser.GetEncryptedPassword());

            return emailFound;
        }

        public bool VerifyAccount(string email)
        {
            Administrator objAdministrator = new Administrator();
            objAdministrator = administratorDAL.SearchEmail(email);

            bool accountFound = false;
            if (objAdministrator != null)
            {
                accountFound = true;
            }

            return accountFound;
        }

        public bool VerifyAccount(string email, string password)
        {
            Administrator objAdministrator = new Administrator();
            objAdministrator = administratorDAL.SearchEmail(email, Cryptography.GetMD5Hash(password));

            bool accountFound = false;
            if (objAdministrator != null)
            {
                accountFound = true;
            }

            return accountFound;
        }

        public Administrator GetAccount(string email)
        {
            return administratorDAL.SearchEmail(email);
        }

        public Administrator GetAccount(string email, string password)
        {
            return administratorDAL.SearchEmail(email, Cryptography.GetMD5Hash(password));
        }

        public bool VerifyAccountStatus(Administrator objAdministrator)
        {
            bool accountActive = false;
            if (objAdministrator.GetIsActive() == true)
            {
                accountActive = true;
            }
            return accountActive;
        }
    }
}