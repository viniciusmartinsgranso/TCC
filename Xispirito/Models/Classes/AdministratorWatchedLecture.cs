namespace Xispirito.Models
{
    public class AdministratorWatchedLecture
    {
        private Administrator Administrator { get; set; }
        private Lecture Lecture { get; set; }

        public AdministratorWatchedLecture()
        {

        }

        public AdministratorWatchedLecture(Administrator administrator, Lecture lecture)
        {
            Administrator = administrator;
            Lecture = lecture;
        }

        public Administrator GetAdministrator()
        {
            return Administrator;
        }

        public Lecture GetLecture()
        {
            return Lecture;
        }
    }
}