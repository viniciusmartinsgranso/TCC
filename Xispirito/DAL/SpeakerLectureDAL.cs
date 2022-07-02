using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Xispirito.Models;

namespace Xispirito.DAL
{
    public class SpeakerLectureDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["XispiritoDB"].ConnectionString;

        public void RegisterUserToLecture(SpeakerLecture objSpeakerLecture)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Speaker_Lecture VALUES (@email_speaker, @id_lecture)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_speaker", objSpeakerLecture.GetSpeaker().GetEmail());
            cmd.Parameters.AddWithValue("@id_lecture", objSpeakerLecture.GetLecture().GetId());

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public bool VerifyUserAlreadyRegistered(SpeakerLecture objSpeakerLecture)
        {
            bool userAlreadyRegistered = false;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Speaker_Lecture WHERE email_speaker = @email_speaker AND id_lecture = @id_lecture";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_speaker", objSpeakerLecture.GetSpeaker().GetEmail());
            cmd.Parameters.AddWithValue("@id_lecture", objSpeakerLecture.GetLecture().GetId());

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                userAlreadyRegistered = true;
            }

            return userAlreadyRegistered;
        }

        public void DeleteUserSubscription(SpeakerLecture objSpeakerLecture)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "DELETE FROM Speaker_Lecture WHERE email_speaker = @email_speaker AND id_lecture = @id_lecture";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_speaker", objSpeakerLecture.GetSpeaker().GetEmail());
            cmd.Parameters.AddWithValue("@id_lecture", objSpeakerLecture.GetLecture().GetId());

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public int GetLectureRegistrations(int lectureId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Speaker_Lecture.email_speaker, "
                + "Speaker.*, "
                + "Lecture.* "
                + "FROM Speaker_Lecture "
                + "INNER JOIN Speaker ON Speaker_Lecture.email_speaker = Speaker.email_speaker "
                + "INNER JOIN Lecture ON Speaker_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Speaker_Lecture.id_lecture = @id_lecture AND Lecture.isActive = 1";

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

        public SpeakerLecture GetUserLectureRegistration(string speakerEmail, int lectureId)
        {
            SpeakerLecture objSpeakerLecture = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Speaker_Lecture.email_speaker, "
                + "Speaker.*, "
                + "Lecture.* "
                + "FROM Speaker_Lecture "
                + "INNER JOIN Speaker ON Speaker_Lecture.email_speaker = Speaker.email_speaker "
                + "INNER JOIN Lecture ON Speaker_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Speaker_Lecture.email_speaker = @email_speaker AND Speaker_Lecture.id_lecture = @id_lecture AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_speaker", speakerEmail);
            cmd.Parameters.AddWithValue("@id_lecture", lectureId);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                Speaker objSpeaker = new Speaker(
                        Convert.ToInt32(dr["id_speaker"]),
                        dr["nm_speaker"].ToString(),
                        speakerEmail,
                        dr["pt_speaker"].ToString(),
                        dr["pf_speaker"].ToString(),
                        dr["pw_speaker"].ToString(),
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

                objSpeakerLecture = new SpeakerLecture(
                    objSpeaker,
                    objLecture
                );
            }

            conn.Close();

            return objSpeakerLecture;
        }

        public List<SpeakerLecture> GetUserLecturesRegistration(string speakerEmail)
        {
            List<SpeakerLecture> speakerLectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Speaker_Lecture.email_speaker, "
                + "Speaker.*, "
                + "Lecture.* "
                + "FROM Speaker_Lecture "
                + "INNER JOIN Speaker ON Speaker_Lecture.email_speaker = Speaker.email_speaker "
                + "INNER JOIN Lecture ON Speaker_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Speaker_Lecture.email_speaker = @email_speaker AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_speaker", speakerEmail);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                speakerLectureList = new List<SpeakerLecture>();

                while (dr.Read())
                {
                    Speaker objSpeaker = new Speaker(
                        Convert.ToInt32(dr["id_speaker"]),
                        dr["nm_speaker"].ToString(),
                        speakerEmail,
                        dr["pt_speaker"].ToString(),
                        dr["pf_speaker"].ToString(),
                        dr["pw_speaker"].ToString(),
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

                    SpeakerLecture speakerLecture = new SpeakerLecture(
                        objSpeaker,
                        objLecture
                    );
                    speakerLectureList.Add(speakerLecture);
                }
            }

            conn.Close();

            return speakerLectureList;
        }

        public List<SpeakerLecture> GetUserLecturesRegistration(string speakerEmail, string search)
        {
            List<SpeakerLecture> speakerLectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Speaker_Lecture.email_speaker, "
                + "Speaker.*, "
                + "Lecture.* "
                + "FROM Speaker_Lecture "
                + "INNER JOIN Speaker ON Speaker_Lecture.email_speaker = Speaker.email_speaker "
                + "INNER JOIN Lecture ON Speaker_Lecture.id_lecture = Lecture.id_lecture "
                + "WHERE Speaker_Lecture.email_speaker = @email_speaker AND Lecture.nm_lecture LIKE @search AND Lecture.isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_speaker", speakerEmail);
            cmd.Parameters.AddWithValue("@search", "%" + search + "%");

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                speakerLectureList = new List<SpeakerLecture>();

                while (dr.Read())
                {
                    Speaker objSpeaker = new Speaker(
                        Convert.ToInt32(dr["id_speaker"]),
                        dr["nm_speaker"].ToString(),
                        speakerEmail,
                        dr["pt_speaker"].ToString(),
                        dr["pf_speaker"].ToString(),
                        dr["pw_speaker"].ToString(),
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

                    SpeakerLecture speakerLecture = new SpeakerLecture(
                        objSpeaker,
                        objLecture
                    );
                    speakerLectureList.Add(speakerLecture);
                }
            }

            conn.Close();

            return speakerLectureList;
        }
    }
}