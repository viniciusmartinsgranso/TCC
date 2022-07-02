using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Xispirito.Models;

namespace Xispirito.DAL
{
    public class SpeakerCertificateDAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["XispiritoDB"].ConnectionString;

        public void RegisterUserCertificate(string userEmail, int certificateId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Speaker_Certificate VALUES (@email_speaker, @id_certified)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_speaker", userEmail);
            cmd.Parameters.AddWithValue("@id_certified", certificateId);

            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<SpeakerCertificate> GetAllUserCertificates(string userEmail)
        {
            List<SpeakerCertificate> userCertificates = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Speaker_Certificate.key_certificate,"
               + "Speaker.*,"
               + "Certified.*,"
               + "Lecture.*"
               + "FROM Speaker_Certificate "
               + "INNER JOIN Speaker ON Speaker_Certificate.email_speaker = Speaker.email_speaker "
               + "INNER JOIN Certified ON Speaker_Certificate.id_certified = Certified.id_certified "
               + "INNER JOIN Lecture ON Certified.id_lecture = Lecture.id_lecture "
               + "WHERE Speaker_Certificate.email_speaker = @email_speaker";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_speaker", userEmail);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                userCertificates = new List<SpeakerCertificate>();

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

                    SpeakerCertificate speakerCertificate = new SpeakerCertificate(
                        Convert.ToInt32(dr["key_certificate"]),
                        objSpeaker,
                        objLecture,
                        objCertificate
                    );
                    userCertificates.Add(speakerCertificate);
                }
            }
            conn.Close();

            return userCertificates;
        }

        public List<SpeakerCertificate> GetFilterUserCertificates(string userEmail, string lectureName)
        {
            List<SpeakerCertificate> userCertificates = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT Speaker_Certificate.key_certificate,"
               + "Speaker.*,"
               + "Certified.*,"
               + "Lecture.*"
               + "FROM Speaker_Certificate "
               + "INNER JOIN Speaker ON Speaker_Certificate.email_speaker = Speaker.email_speaker "
               + "INNER JOIN Certified ON Speaker_Certificate.id_certified = Certified.id_certified "
               + "INNER JOIN Lecture ON Certified.id_lecture = Lecture.id_lecture "
               + "WHERE Speaker_Certificate.email_speaker = @email_speaker AND Lecture.nm_lecture LIKE @lectureName";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_speaker", userEmail);
            cmd.Parameters.AddWithValue("@lectureName", "%" + lectureName + "%");

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                userCertificates = new List<SpeakerCertificate>();

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

                    SpeakerCertificate speakerCertificate = new SpeakerCertificate(
                        Convert.ToInt32(dr["key_certificate"]),
                        objSpeaker,
                        objLecture,
                        objCertificate
                    );
                    userCertificates.Add(speakerCertificate);
                }
            }
            conn.Close();

            return userCertificates;
        }
    }
}