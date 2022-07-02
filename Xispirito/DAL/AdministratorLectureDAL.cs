using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Xispirito.Models;

namespace Xispirito.DAL
{
    public class AdministratorLectureDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["XispiritoDB"].ConnectionString;

        public void RegisterUserToLecture(AdministratorLecture objAdministratorLecture)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Administrator_Lecture VALUES (@email_administrator, @id_lecture)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_administrator", objAdministratorLecture.GetAdministrator().GetEmail());
            cmd.Parameters.AddWithValue("@id_lecture", objAdministratorLecture.GetLecture().GetId());

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public bool VerifyUserAlreadyRegistered(AdministratorLecture objAdministratorLecture)
        {
            bool userAlreadyRegistered = false;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Administrator_Lecture WHERE email_administrator = @email_administrator AND id_lecture = @id_lecture";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_administrator", objAdministratorLecture.GetAdministrator().GetEmail());
            cmd.Parameters.AddWithValue("@id_lecture", objAdministratorLecture.GetLecture().GetId());

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                userAlreadyRegistered = true;
            }

            return userAlreadyRegistered;
        }

        public void DeleteUserSubscription(AdministratorLecture objAdministratorLecture)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "DELETE FROM Administrator_Lecture WHERE email_administrator = @email_administrator AND id_lecture = @id_lecture";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_administrator", objAdministratorLecture.GetAdministrator().GetEmail());
            cmd.Parameters.AddWithValue("@id_lecture", objAdministratorLecture.GetLecture().GetId());

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public AdministratorLecture GetUserLectureRegistration(string administratorEmail, int lectureId)
        {
            AdministratorLecture objAdministratorLecture = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Administrator_Lecture.email_administrator, "
                + "Administrator.*, "
                + "Lecture.* "
                + "FROM Administrator_Lecture "
                + "INNER JOIN Administrator ON Administrator_Lecture.email_administrator = Administrator.email_administrator "
                + "INNER JOIN Lecture ON Administrator_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Administrator_Lecture.email_administrator = @email_administrator AND Administrator_Lecture.id_lecture = @id_lecture AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_administrator", administratorEmail);
            cmd.Parameters.AddWithValue("@id_lecture", lectureId);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                Administrator objAdministrator = new Administrator(
                        Convert.ToInt32(dr["id_administrator"]),
                        dr["nm_administrator"].ToString(),
                        administratorEmail,
                        dr["pt_administrator"].ToString(),
                        dr["pw_administrator"].ToString(),
                        Convert.ToBoolean(dr["isActive"])
                );

                Lecture objLecture = new Lecture(
                    Convert.ToInt32(dr["id_lecture"]),
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

                objAdministratorLecture = new AdministratorLecture(
                    objAdministrator,
                    objLecture
                );
            }

            conn.Close();

            return objAdministratorLecture;
        }

        public List<AdministratorLecture> GetUserLecturesRegistration(string administratorEmail)
        {
            List<AdministratorLecture> administratorLectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Administrator_Lecture.email_administrator, "
                + "Administrator.*, "
                + "Lecture.* "
                + "FROM Administrator_Lecture "
                + "INNER JOIN Administrator ON Administrator_Lecture.email_administrator = Administrator.email_administrator "
                + "INNER JOIN Lecture ON Administrator_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Administrator_Lecture.email_administrator = @email_administrator AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_administrator", administratorEmail);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                administratorLectureList = new List<AdministratorLecture>();

                while (dr.Read())
                {
                    Administrator objAdministrator = new Administrator(
                        Convert.ToInt32(dr["id_administrator"]),
                        dr["nm_administrator"].ToString(),
                        administratorEmail,
                        dr["pt_administrator"].ToString(),
                        dr["pw_administrator"].ToString(),
                        Convert.ToBoolean(dr["isActive"])
                    );

                    Lecture objLecture = new Lecture(
                        Convert.ToInt32(dr["id_lecture"]),
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

                    AdministratorLecture administratorLecture = new AdministratorLecture(
                        objAdministrator,
                        objLecture
                    );
                    administratorLectureList.Add(administratorLecture);
                }
            }
            conn.Close();

            return administratorLectureList;
        }

        public List<AdministratorLecture> GetUserLecturesRegistration(string administratorEmail, string search)
        {
            List<AdministratorLecture> administratorLectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Administrator_Lecture.email_administrator, "
                + "Administrator.*, "
                + "Lecture.* "
                + "FROM Administrator_Lecture "
                + "INNER JOIN Administrator ON Administrator_Lecture.email_administrator = Administrator.email_administrator "
                + "INNER JOIN Lecture ON Administrator_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Administrator_Lecture.email_administrator = @email_administrator AND Lecture.nm_lecture LIKE @search AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_administrator", administratorEmail);
            cmd.Parameters.AddWithValue("@search", "%" + search + "%");

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                administratorLectureList = new List<AdministratorLecture>();

                while (dr.Read())
                {
                    Administrator objAdministrator = new Administrator(
                        Convert.ToInt32(dr["id_administrator"]),
                        dr["nm_administrator"].ToString(),
                        administratorEmail,
                        dr["pt_administrator"].ToString(),
                        dr["pw_administrator"].ToString(),
                        Convert.ToBoolean(dr["isActive"])
                    );

                    Lecture objLecture = new Lecture(
                        Convert.ToInt32(dr["id_lecture"]),
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

                    AdministratorLecture administratorLecture = new AdministratorLecture(
                        objAdministrator,
                        objLecture
                    );
                    administratorLectureList.Add(administratorLecture);
                }
            }
            conn.Close();

            return administratorLectureList;
        }
    }
}