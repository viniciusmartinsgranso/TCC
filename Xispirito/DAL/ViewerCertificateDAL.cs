using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Xispirito.Models;

namespace Xispirito.DAL
{
    public class ViewerCertificateDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["XispiritoDB"].ConnectionString;

        public void RegisterUserCertificate(string userEmail, int certificateId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Viewer_Certificate VALUES (@email_viewer, @id_certified)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_viewer", userEmail);
            cmd.Parameters.AddWithValue("@id_certified", certificateId);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<ViewerCertificate> GetAllUserCertificates(string userEmail)
        {
            List<ViewerCertificate> userCertificates = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Viewer_Certificate.key_certificate,"
               + "Viewer.*,"
               + "Certified.*,"
               + "Lecture.*"
               + "FROM Viewer_Certificate "
               + "INNER JOIN Viewer ON Viewer_Certificate.email_viewer = Viewer.email_viewer "
               + "INNER JOIN Certified ON Viewer_Certificate.id_certified = Certified.id_certified "
               + "INNER JOIN Lecture ON Certified.id_lecture = Lecture.id_lecture "
               + "WHERE Viewer_Certificate.email_viewer = @email_viewer";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_viewer", userEmail);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                userCertificates = new List<ViewerCertificate>();

                while (dr.Read())
                {
                    Viewer objViewer = new Viewer(
                        Convert.ToInt32(dr["id_viewer"]),
                        dr["nm_viewer"].ToString(),
                        dr["email_viewer"].ToString(),
                        dr["pt_viewer"].ToString(),
                        dr["pw_viwer"].ToString(),
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

                    ViewerCertificate viewerCertificate = new ViewerCertificate(
                        Convert.ToInt32(dr["key_certificate"]),
                        objViewer,
                        objLecture,
                        objCertificate
                    );
                    userCertificates.Add(viewerCertificate);
                }
            }
            conn.Close();

            return userCertificates;
        }

        public List<ViewerCertificate> GetFilterUserCertificates(string userEmail, string lectureName)
        {
            List<ViewerCertificate> userCertificates = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Viewer_Certificate.key_certificate,"
               + "Viewer.*,"
               + "Certified.*,"
               + "Lecture.*"
               + "FROM Viewer_Certificate "
               + "INNER JOIN Viewer ON Viewer_Certificate.email_viewer = Viewer.email_viewer "
               + "INNER JOIN Certified ON Viewer_Certificate.id_certified = Certified.id_certified "
               + "INNER JOIN Lecture ON Certified.id_lecture = Lecture.id_lecture "
               + "WHERE Viewer_Certificate.email_viewer = @email_viewer AND Lecture.nm_lecture LIKE @lectureName";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_viewer", userEmail);
            cmd.Parameters.AddWithValue("@lectureName", "%" + lectureName + "%");

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                userCertificates = new List<ViewerCertificate>();

                while (dr.Read())
                {
                    Viewer objViewer = new Viewer(
                        Convert.ToInt32(dr["id_viewer"]),
                        dr["nm_viewer"].ToString(),
                        dr["email_viewer"].ToString(),
                        dr["pt_viewer"].ToString(),
                        dr["pw_viwer"].ToString(),
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

                    ViewerCertificate viewerCertificate = new ViewerCertificate(
                        Convert.ToInt32(dr["key_certificate"]),
                        objViewer,
                        objLecture,
                        objCertificate
                    );
                    userCertificates.Add(viewerCertificate);
                }
            }
            conn.Close();

            return userCertificates;
        }
    }
}