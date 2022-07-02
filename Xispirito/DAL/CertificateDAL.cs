using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Xispirito.Models;

namespace Xispirito.DAL
{
    public class CertificateDAL : IDatabase<Certificate>
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["XispiritoDB"].ConnectionString;

        public void Insert(Certificate objCertificate)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Certified VALUES (@mdl_certificate, @id_lecture, @isActive)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("mdl_certificate", objCertificate.GetCertificateModelDirectory());
            cmd.Parameters.AddWithValue("@id_lecture", objCertificate.GetLectureId());
            cmd.Parameters.AddWithValue("@isActive", objCertificate.GetIsActive());
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public Certificate Select(int certificateId)
        {
            Certificate certificate = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Certified WHERE id_certified = @id_certified";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id_certified", certificateId);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                certificate = new Certificate(
                    certificateId,
                    dr["mdl_certificate"].ToString(),
                    Convert.ToInt32(dr["id_lecture"]),
                    Convert.ToBoolean(dr["isActive"])
                );
            }
            conn.Close();

            return certificate;
        }

        public void Update(Certificate entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int certificateId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Certified SET isActive = @isActive WHERE id_certified = @id_certified";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@isActive", false);
            cmd.Parameters.AddWithValue("@id_certified", certificateId);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public List<Certificate> List()
        {
            List<Certificate> certificateList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Certified Where isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                certificateList = new List<Certificate>();

                while (dr.Read())
                {
                    Certificate objCertificate = new Certificate(
                        Convert.ToInt32(dr["id_certified"]),
                        dr["mdl_certificate"].ToString(),
                        Convert.ToInt32(dr["id_lecture"]),
                        Convert.ToBoolean(dr["isActive"])
                    );
                    certificateList.Add(objCertificate);
                }
            }
            conn.Close();

            return certificateList;
        }
    }
}