namespace Xispirito.Models
{
    public class AdministratorLecture : BaseEntity
    {
        private Administrator Administrator { get; set; }
        private Lecture Lecture { get; set; }

        public AdministratorLecture()
        {

        }

        public AdministratorLecture(Administrator administrator, Lecture lecture)
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