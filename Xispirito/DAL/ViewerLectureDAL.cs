using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Xispirito.Models;

namespace Xispirito.DAL
{
    public class ViewerLectureDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["XispiritoDB"].ConnectionString;

        public void RegisterUserToLecture(ViewerLecture objViewerLecture)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Viewer_Lecture VALUES (@email_viewer, @id_lecture)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_viewer", objViewerLecture.GetViewer().GetEmail());
            cmd.Parameters.AddWithValue("@id_lecture", objViewerLecture.GetLecture().GetId());

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public bool VerifyUserAlreadyRegistered(ViewerLecture objViewerLecture)
        {
            bool userAlreadyRegistered = false;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Viewer_Lecture WHERE email_viewer = @email_viewer AND id_lecture = @id_lecture";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_viewer", objViewerLecture.GetViewer().GetEmail());
            cmd.Parameters.AddWithValue("@id_lecture", objViewerLecture.GetLecture().GetId());

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                userAlreadyRegistered = true;
            }

            return userAlreadyRegistered;
        }

        public void DeleteUserSubscription(ViewerLecture objViewerLecture)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "DELETE FROM Viewer_Lecture WHERE email_viewer = @email_viewer AND id_lecture = @id_lecture";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_viewer", objViewerLecture.GetViewer().GetEmail());
            cmd.Parameters.AddWithValue("@id_lecture", objViewerLecture.GetLecture().GetId());

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public int GetLectureRegistrations(int lectureId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Viewer_Lecture.email_viewer, "
                + "Viewer.*, "
                + "Lecture.* "
                + "FROM Viewer_Lecture "
                + "INNER JOIN Viewer ON Viewer_Lecture.email_viewer = Viewer.email_viewer "
                + "INNER JOIN Lecture ON Viewer_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Viewer_Lecture.id_lecture = @id_lecture AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id_lecture", lectureId);

            SqlDataReader dr = cmd.ExecuteReader();

            int registrationNumber = 0;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    registrationNumber++;
                }
            }
            conn.Close();

            return registrationNumber;
        }

        public ViewerLecture GetUserLectureRegistration(string viewerEmail, int lectureId)
        {
            ViewerLecture objViewerLecture = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Viewer_Lecture.email_viewer, "
                + "Viewer.*, "
                + "Lecture.* "
                + "FROM Viewer_Lecture "
                + "INNER JOIN Viewer ON Viewer_Lecture.email_viewer = Viewer.email_viewer "
                + "INNER JOIN Lecture ON Viewer_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Viewer_Lecture.email_viewer = @email_viewer AND Viewer_Lecture.id_lecture = @id_lecture AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_viewer", viewerEmail);
            cmd.Parameters.AddWithValue("@id_lecture", lectureId);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                Viewer objViewer = new Viewer(
                        Convert.ToInt32(dr["id_viewer"]),
                        dr["nm_viewer"].ToString(),
                        viewerEmail,
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

                objViewerLecture = new ViewerLecture(
                    objViewer,
                    objLecture
                );
            }

            conn.Close();

            return objViewerLecture;
        }

        public List<ViewerLecture> GetUserLecturesRegistration(string viewerEmail)
        {
            List<ViewerLecture> viewerLectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Viewer_Lecture.email_viewer, "
                + "Viewer.*, "
                + "Lecture.* "
                + "FROM Viewer_Lecture "
                + "INNER JOIN Viewer ON Viewer_Lecture.email_viewer = Viewer.email_viewer "
                + "INNER JOIN Lecture ON Viewer_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Viewer_Lecture.email_viewer = @email_viewer AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_viewer", viewerEmail);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                viewerLectureList = new List<ViewerLecture>();

                while (dr.Read())
                {
                    Viewer objViewer = new Viewer(
                        Convert.ToInt32(dr["id_viewer"]),
                        dr["nm_viewer"].ToString(),
                        viewerEmail,
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

                    ViewerLecture viewerLecture = new ViewerLecture(
                        objViewer,
                        objLecture
                    );
                    viewerLectureList.Add(viewerLecture);
                }
            }

            conn.Close();

            return viewerLectureList;
        }

        public List<ViewerLecture> GetUserLecturesRegistration(string viewerEmail, string search)
        {
            List<ViewerLecture> viewerLectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Viewer_Lecture.email_viewer, "
                + "Viewer.*, "
                + "Lecture.* "
                + "FROM Viewer_Lecture "
                + "INNER JOIN Viewer ON Viewer_Lecture.email_viewer = Viewer.email_viewer "
                + "INNER JOIN Lecture ON Viewer_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Viewer_Lecture.email_viewer = @email_viewer AND Lecture.nm_lecture LIKE @search AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_viewer", viewerEmail);
            cmd.Parameters.AddWithValue("@search", "%" + search + "%");

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                viewerLectureList = new List<ViewerLecture>();

                while (dr.Read())
                {
                    Viewer objViewer = new Viewer(
                        Convert.ToInt32(dr["id_viewer"]),
                        dr["nm_viewer"].ToString(),
                        viewerEmail,
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

                    ViewerLecture viewerLecture = new ViewerLecture(
                        objViewer,
                        objLecture
                    );
                    viewerLectureList.Add(viewerLecture);
                }
            }

            conn.Close();

            return viewerLectureList;
        }
    }
}