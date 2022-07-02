using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Xispirito.Models;

namespace Xispirito.DAL
{
    public class ViewerWatchedLectureDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["XispiritoDB"].ConnectionString;

        public void RegisterUserAttendance(ViewerWatchedLecture objViewerWatchedLecture)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Viewer_Watched_Lecture VALUES (@email_viewer, @id_lecture)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_viewer", objViewerWatchedLecture.GetViewer().GetEmail());
            cmd.Parameters.AddWithValue("@id_lecture", objViewerWatchedLecture.GetLecture().GetId());

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public bool VerifyRegisterToLecture(ViewerWatchedLecture objViewerWatchedLecture)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Viewer_Watched_Lecture.email_viewer, "
                + "Viewer.*, "
                + "Lecture.* "
                + "FROM Viewer_Watched_Lecture "
                + "INNER JOIN Viewer ON Viewer_Watched_Lecture.email_viewer = Viewer.email_viewer "
                + "INNER JOIN Lecture ON Viewer_Watched_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Viewer_Watched_Lecture.email_viewer = @email_viewer     AND Viewer_Watched_Lecture.id_lecture = @id_lecture AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_viewer", objViewerWatchedLecture.GetViewer().GetEmail());
            cmd.Parameters.AddWithValue("@id_lecture", objViewerWatchedLecture.GetLecture().GetId());

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

            string sql = "DELETE FROM Viewer_Watched_Lecture WHERE email_viewer = @email_viewer";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_viewer", userEmail);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<ViewerWatchedLecture> GetUsersWhoAttended(int lectureId)
        {
            List<ViewerWatchedLecture> viewerWatchedLectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Viewer_Watched_Lecture.email_viewer, "
                + "Viewer.*, "
                + "Lecture.* "
                + "FROM Viewer_Watched_Lecture "
                + "INNER JOIN Viewer ON Viewer_Watched_Lecture.email_viewer = Viewer.email_viewer "
                + "INNER JOIN Lecture ON Viewer_Watched_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Viewer_Watched_Lecture.id_lecture = @id_lecture AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id_lecture", lectureId);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                viewerWatchedLectureList = new List<ViewerWatchedLecture>();

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

                    ViewerWatchedLecture viewerWatchedLecture = new ViewerWatchedLecture(
                        objViewer,
                        objLecture
                    );
                    viewerWatchedLectureList.Add(viewerWatchedLecture);
                }
            }
            conn.Close();

            return viewerWatchedLectureList;
        }
    }
}