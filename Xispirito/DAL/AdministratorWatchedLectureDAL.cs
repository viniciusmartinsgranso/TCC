using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Xispirito.Models;

namespace Xispirito.DAL
{
    public class AdministratorWatchedLectureDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["XispiritoDB"].ConnectionString;

        public void RegisterUserAttendance(AdministratorWatchedLecture objAdministratorWatchedLecture)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Administrator_Watched_Lecture VALUES (@email_administrator, @id_lecture)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_administrator", objAdministratorWatchedLecture.GetAdministrator().GetEmail());
            cmd.Parameters.AddWithValue("@id_lecture", objAdministratorWatchedLecture.GetLecture().GetId());

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public bool VerifyRegisterToLecture(AdministratorWatchedLecture objAdministratorWatchedLecture)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Administrator_Watched_Lecture.email_administrator, "
                + "Administrator.*, "
                + "Lecture.* "
                + "FROM Administrator_Watched_Lecture "
                + "INNER JOIN Administrator ON Administrator_Watched_Lecture.email_administrator = Administrator.email_administrator "
                + "INNER JOIN Lecture ON Administrator_Watched_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Administrator_Watched_Lecture.email_administrator = @email_administrator AND Administrator_Watched_Lecture.id_lecture = @id_lecture AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_administrator", objAdministratorWatchedLecture.GetAdministrator().GetEmail());
            cmd.Parameters.AddWithValue("@id_lecture", objAdministratorWatchedLecture.GetLecture().GetId());

            SqlDataReader dr = cmd.ExecuteReader();

            bool userAlreadyRegisteredToLecture = false;
            if (dr.HasRows)
            {
                userAlreadyRegisteredToLecture = true;
            }
            conn.Close();

            return userAlreadyRegisteredToLecture;
        }

        public void DeleteUserAttendance(string userEmail)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "DELETE FROM Administrator_Watched_Lecture WHERE email_administrator = @email_administrator";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_administrator", userEmail);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<AdministratorWatchedLecture> GetUsersWhoAttended(int lectureId)
        {
            List<AdministratorWatchedLecture> administratorWatchedLectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Administrator_Watched_Lecture.email_administrator, "
                + "Administrator.*, "
                + "Lecture.* "
                + "FROM Administrator_Watched_Lecture "
                + "INNER JOIN Administrator ON Administrator_Watched_Lecture.email_administrator = Administrator.email_administrator "
                + "INNER JOIN Lecture ON Administrator_Watched_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Administrator_Watched_Lecture.id_lecture = @id_lecture AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id_lecture", lectureId);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                administratorWatchedLectureList = new List<AdministratorWatchedLecture>();

                while (dr.Read())
                {
                    Administrator objAdministrator = new Administrator(
                        Convert.ToInt32(dr["id_administrator"]),
                        dr["nm_administrator"].ToString(),
                        dr["email_administrator"].ToString(),
                        dr["pt_administrator"].ToString(),
                        dr["pw_administrator"].ToString(),
                        Convert.ToBoolean(dr["isActive"])
                    );

                    Lecture objLecture = new Lecture(
                        lectureId,
                        dr["nm_lecture"].ToString(),
                        dr["pt_lecture"].ToString(),
                        Convert.ToInt32(dr["tm_lecture"]),
                        Convert.ToDateTime(dr["dt_lecture"]),
                        dr["dc_lecture"].ToString(),
                        Enum.GetName(typeof(Modality), Convert.ToInt32(dr["mod_lecture"])),
                        dr["adr_lecture"].ToString(),
                        Convert.ToInt32(dr["lt_lecture"]),
                        Convert.ToBoolean(dr["isActive"])
                    );

                    AdministratorWatchedLecture administratorWatchedLecture = new AdministratorWatchedLecture(
                        objAdministrator,
                        objLecture
                    );
                    administratorWatchedLectureList.Add(administratorWatchedLecture);
                }
            }
            conn.Close();

            return administratorWatchedLectureList;
        }
    }
}