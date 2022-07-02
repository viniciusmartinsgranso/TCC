using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Xispirito.Models;

namespace Xispirito.DAL
{
    public class SpeakerWatchedLectureDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["XispiritoDB"].ConnectionString;

        public void RegisterUserAttendance(SpeakerWatchedLecture objSpeakerWatchedLecture)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Speaker_Watched_Lecture VALUES (@email_speaker, @id_lecture)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_speaker", objSpeakerWatchedLecture.GetSpeaker().GetEmail());
            cmd.Parameters.AddWithValue("@id_lecture", objSpeakerWatchedLecture.GetLecture().GetId());

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public bool VerifyRegisterToLecture(SpeakerWatchedLecture objSpeakerWatchedLecture)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Speaker_Watched_Lecture.email_speaker, "
                + "Speaker.*, "
                + "Lecture.* "
                + "FROM Speaker_Watched_Lecture "
                + "INNER JOIN Speaker ON Speaker_Watched_Lecture.email_speaker = Speaker.email_speaker "
                + "INNER JOIN Lecture ON Speaker_Watched_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Speaker_Watched_Lecture.email_speaker = @email_speaker AND Speaker_Watched_Lecture.id_lecture = @id_lecture AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_speaker", objSpeakerWatchedLecture.GetSpeaker().GetEmail());
            cmd.Parameters.AddWithValue("@id_lecture", objSpeakerWatchedLecture.GetLecture().GetId());

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

            string sql = "DELETE FROM Speaker_Watched_Lecture WHERE email_speaker = @email_speaker";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_speaker", userEmail);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<SpeakerWatchedLecture> GetUsersWhoAttended(int lectureId)
        {
            List<SpeakerWatchedLecture> speakerWatchedLectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Speaker_Watched_Lecture.email_speaker, "
                + "Speaker.*, "
                + "Lecture.* "
                + "FROM Speaker_Watched_Lecture "
                + "INNER JOIN Speaker ON Speaker_Watched_Lecture.email_speaker = Speaker.email_speaker "
                + "INNER JOIN Lecture ON Speaker_Watched_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Speaker_Watched_Lecture.id_lecture = @id_lecture AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id_lecture", lectureId);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                speakerWatchedLectureList = new List<SpeakerWatchedLecture>();

                while (dr.Read())
                {
                    Speaker objSpeaker = new Speaker(
                        Convert.ToInt32(dr["id_speaker"]),
                        dr["nm_speaker"].ToString(),
                        dr["email_speaker"].ToString(),
                        dr["pt_speaker"].ToString(),
                        dr["pf_speaker"].ToString(),
                        dr["pw_speaker"].ToString(),
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

                    SpeakerWatchedLecture speakerWatchedLecture = new SpeakerWatchedLecture(
                        objSpeaker,
                        objLecture
                    );
                    speakerWatchedLectureList.Add(speakerWatchedLecture);
                }
            }
            conn.Close();

            return speakerWatchedLectureList;
        }
    }
}