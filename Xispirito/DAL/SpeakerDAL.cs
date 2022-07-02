using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Xispirito.Models;

namespace Xispirito.DAL
{
    public class SpeakerDAL : IDatabase<Speaker>
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["XispiritoDB"].ConnectionString;

        public void Insert(Speaker objSpeaker)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Speaker VALUES (@nm_speaker, @email_speaker, @pt_speaker, @pf_speaker, @pw_speaker, @isActive)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nm_speaker", objSpeaker.GetName());
            cmd.Parameters.AddWithValue("@email_speaker", objSpeaker.GetEmail());
            cmd.Parameters.AddWithValue("@pt_speaker", objSpeaker.GetPicture());
            cmd.Parameters.AddWithValue("@pf_speaker", objSpeaker.GetSpeakerProfession());
            cmd.Parameters.AddWithValue("@pw_speaker", objSpeaker.GetEncryptedPassword());
            cmd.Parameters.AddWithValue("@isActive", objSpeaker.GetIsActive());
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void InsertGenerateSpeaker(Speaker objSpeaker)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Speaker (nm_speaker, email_speaker, pw_speaker, isActive) VALUES (@nm_speaker, @email_speaker, @pw_speaker, @isActive)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nm_speaker", objSpeaker.GetName());
            cmd.Parameters.AddWithValue("@email_speaker", objSpeaker.GetEmail());
            cmd.Parameters.AddWithValue("@pw_speaker", objSpeaker.GetEncryptedPassword());
            cmd.Parameters.AddWithValue("@isActive", true);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public Speaker Select(int speakerId)
        {
            Speaker objSpeaker = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Speaker WHERE id_speaker = @id_speaker AND isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id_speaker", speakerId);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objSpeaker = new Speaker(
                    speakerId,
                    dr["nm_speaker"].ToString(),
                    dr["email_speaker"].ToString(),
                    dr["pt_speaker"].ToString(),
                    dr["pf_speaker"].ToString(),
                    dr["pw_speaker"].ToString(),
                    Convert.ToBoolean(dr["isActive"])
                );
            }
            conn.Close();

            return objSpeaker;
        }

        public Speaker SearchEmail(string speakerEmail)
        {
            Speaker objSpeaker = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Speaker WHERE email_speaker = @email_speaker";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_speaker", speakerEmail);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objSpeaker = new Speaker(
                    Convert.ToInt32(dr["id_speaker"]),
                    dr["nm_speaker"].ToString(),
                    dr["email_speaker"].ToString(),
                    dr["pt_speaker"].ToString(),
                    dr["pf_speaker"].ToString(),
                    dr["pw_speaker"].ToString(),
                    Convert.ToBoolean(dr["isActive"])
                );
            }
            conn.Close();

            return objSpeaker;
        }

        public Speaker SearchEmail(string speakerEmail, string speakerEncryptedPassword)
        {
            Speaker objSpeaker = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Speaker WHERE email_speaker = @email_speaker AND pw_speaker = @pw_speaker";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_speaker", speakerEmail);
            cmd.Parameters.AddWithValue("@pw_speaker", speakerEncryptedPassword);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objSpeaker = new Speaker(
                    Convert.ToInt32(dr["id_speaker"]),
                    dr["nm_speaker"].ToString(),
                    dr["email_speaker"].ToString(),
                    dr["pt_speaker"].ToString(),
                    dr["pf_speaker"].ToString(),
                    speakerEncryptedPassword,
                    Convert.ToBoolean(dr["isActive"])
                );
            }
            conn.Close();

            return objSpeaker;
        }

        public void Update(Speaker objSpeaker)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Speaker SET nm_speaker = @nm_speaker, email_speaker = @email_speaker, pt_speaker = @pt_speaker, pw_viwer = @pw_speaker, isActive = @isActive WHERE id_viewer = @id_viewer";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nm_speaker", objSpeaker.GetName());
            cmd.Parameters.AddWithValue("@email_speaker", objSpeaker.GetEmail());
            cmd.Parameters.AddWithValue("@pt_speaker", objSpeaker.GetPicture());
            cmd.Parameters.AddWithValue("@pw_speaker", objSpeaker.GetEncryptedPassword());
            cmd.Parameters.AddWithValue("@isActive", objSpeaker.GetIsActive());
            cmd.Parameters.AddWithValue("@id_speaker", objSpeaker.GetId());

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void Delete(int speakerId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Speaker SET isActive = @isActive WHERE id_speaker = @id_speaker";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@isActive", false);
            cmd.Parameters.AddWithValue("@id_speaker", speakerId);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public List<Speaker> List()
        {
            List<Speaker> speakerList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Speaker WHERE isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                speakerList = new List<Speaker>();

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
                    speakerList.Add(objSpeaker);
                }
            }
            conn.Close();

            return speakerList;
        }

        public List<Speaker> List(string search)
        {
            List<Speaker> speakerList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Speaker WHERE nm_speaker LIKE @nm_speaker AND isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nm_speaker", "%" + search + "%");

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                speakerList = new List<Speaker>();

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
                    speakerList.Add(objSpeaker);
                }
            }
            conn.Close();

            return speakerList;
        }

        public int GetLastIndexSpeaker()
        {
            int index = 0;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT MAX(id_speaker) AS LastIndex FROM Speaker";

            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                index = Convert.ToInt32(dr["LastIndex"]);
            }
            conn.Close();

            return index;
        }

        public bool SignIn(string speakerEmail, string speakerEncryptedPassword)
        {
            bool validSpeaker = false;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Speaker WHERE email_speaker = @email_speaker AND pw_speaker = @pw_speaker";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_speaker", speakerEmail);
            cmd.Parameters.AddWithValue("@pw_speaker", speakerEncryptedPassword);

            SqlDataReader dr = cmd.ExecuteReader();
            validSpeaker = dr.HasRows;

            return validSpeaker;
        }
    }
}