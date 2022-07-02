using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Xispirito.Models;

namespace Xispirito.DAL
{
    public class AdministratorCertificateDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["XispiritoDB"].ConnectionString;

        public void RegisterUserCertificate(string userEmail, int certificateId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Administrator_Certificate VALUES (@email_administrator, @id_certified)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_administrator", userEmail);
            cmd.Parameters.AddWithValue("@id_certified", certificateId);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<AdministratorCertificate> GetAllUserCertificates(string userEmail)
        {
            List<AdministratorCertificate> userCertificates = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Administrator_Certificate.key_certificate,"
               + "Administrator.*,"
               + "Certified.*,"
               + "Lecture.*"
               + "FROM Administrator_Certificate "
               + "INNER JOIN Administrator ON Administrator_Certificate.email_administrator = Administrator.email_administrator "
               + "INNER JOIN Certified ON Administrator_Certificate.id_certified = Certified.id_certified "
               + "INNER JOIN Lecture ON Certified.id_lecture = Lecture.id_lecture "
               + "WHERE Administrator_Certificate.email_administrator = @email_administrator";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_administrator", userEmail);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                userCertificates = new List<AdministratorCertificate>();

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

                    Certificate objCertificate = new Certificate(
                        Convert.ToInt32(dr["id_certified"]),
                        dr["mdl_certificate"].ToString(),
                        Convert.ToInt32(dr["id_lecture"]),
                        Convert.ToBoolean(dr["isActive"])
                    );

                    AdministratorCertificate administratorCertificate = new AdministratorCertificate(
                        Convert.ToInt32(dr["key_certificate"]),
                        objAdministrator,
                        objLecture,
                        objCertificate
                    );
                    userCertificates.Add(administratorCertificate);
                }
            }
            conn.Close();

            return userCertificates;
        }

        public List<AdministratorCertificate> GetFilterUserCertificates(string userEmail, string lectureName)
        {
            List<AdministratorCertificate> userCertificates = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Administrator_Certificate.key_certificate,"
               + "Administrator.*,"
               + "Certified.*,"
               + "Lecture.*"
               + "FROM Administrator_Certificate "
               + "INNER JOIN Administrator ON Administrator_Certificate.email_administrator = Administrator.email_administrator "
               + "INNER JOIN Certified ON Administrator_Certificate.id_certified = Certified.id_certified "
               + "INNER JOIN Lecture ON Certified.id_lecture = Lecture.id_lecture "
               + "WHERE Administrator_Certificate.email_administrator = @email_administrator AND Lecture.nm_lecture LIKE @lectureName";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_administrator", userEmail);
            cmd.Parameters.AddWithValue("@lectureName", "%" + lectureName + "%");

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                userCertificates = new List<AdministratorCertificate>();

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

                    Certificate objCertificate = new Certificate(
                        Convert.ToInt32(dr["id_certified"]),
                        dr["mdl_certificate"].ToString(),
                        Convert.ToInt32(dr["id_lecture"]),
                        Convert.ToBoolean(dr["isActive"])
                    );

                    AdministratorCertificate administratorCertificate = new AdministratorCertificate(
                        Convert.ToInt32(dr["key_certificate"]),
                        objAdministrator,
                        objLecture,
                        objCertificate
                    );
                    userCertificates.Add(administratorCertificate);
                }
            }
            conn.Close();

            return userCertificates;
        }
    }
}