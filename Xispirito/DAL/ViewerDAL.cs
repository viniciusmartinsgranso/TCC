using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Xispirito.Models;

namespace Xispirito.DAL
{
    public class ViewerDAL : IDatabase<Viewer>
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["XispiritoDB"].ConnectionString;

        public void Insert(Viewer objViewer)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Viewer VALUES (@nm_viewer, @email_viewer, @pt_viewer, @pw_viwer, @isActive)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nm_viewer", objViewer.GetName());
            cmd.Parameters.AddWithValue("@email_viewer", objViewer.GetEmail());
            cmd.Parameters.AddWithValue("@pt_viewer", objViewer.GetPicture());
            cmd.Parameters.AddWithValue("@pw_viwer", objViewer.GetEncryptedPassword());
            cmd.Parameters.AddWithValue("@isActive", objViewer.GetIsActive());
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public Viewer Select(int viewerId)
        {
            Viewer objViewer = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Viewer WHERE id_viewer = @id_viewer";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id_viewer", viewerId);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objViewer = new Viewer(
                    viewerId,
                    dr["nm_viewer"].ToString(),
                    dr["email_viewer"].ToString(),
                    dr["pt_viewer"].ToString(),
                    dr["pw_viwer"].ToString(),
                    Convert.ToBoolean(dr["isActive"])
                );
            }
            conn.Close();

            return objViewer;
        }

        public Viewer SearchEmail(string viewerEmail)
        {
            Viewer objViewer = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Viewer WHERE email_viewer = @email_viewer";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_viewer", viewerEmail);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objViewer = new Viewer(
                    Convert.ToInt32(dr["id_viewer"]),
                    dr["nm_viewer"].ToString(),
                    dr["email_viewer"].ToString(),
                    dr["pt_viewer"].ToString(),
                    dr["pw_viwer"].ToString(),
                    Convert.ToBoolean(dr["isActive"])
                );
            }
            conn.Close();

            return objViewer;
        }

        public Viewer SearchEmail(string viewerEmail, string viewerEncryptedPassword)
        {
            Viewer objViewer = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Viewer WHERE email_viewer = @email_viewer AND pw_viwer = @pw_viwer";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_viewer", viewerEmail);
            cmd.Parameters.AddWithValue("@pw_viwer", viewerEncryptedPassword);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objViewer = new Viewer(
                    Convert.ToInt32(dr["id_viewer"]),
                    dr["nm_viewer"].ToString(),
                    dr["email_viewer"].ToString(),
                    dr["pt_viewer"].ToString(),
                    viewerEncryptedPassword,
                    Convert.ToBoolean(dr["isActive"])
                );
            }
            conn.Close();

            return objViewer;
        }

        public void Update(Viewer objViewer)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Viewer SET nm_viewer = @nm_viewer, email_viewer = @email_viewer, pt_viewer = @pt_viewer, pw_viwer = @pw_viwer, isActive = @isActive WHERE id_viewer = @id_viewer";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nm_viewer", objViewer.GetName());
            cmd.Parameters.AddWithValue("@email_viewer", objViewer.GetEmail());
            cmd.Parameters.AddWithValue("@pt_viewer", objViewer.GetPicture());
            cmd.Parameters.AddWithValue("@pw_viwer", objViewer.GetEncryptedPassword());
            cmd.Parameters.AddWithValue("@isActive", objViewer.GetIsActive());
            cmd.Parameters.AddWithValue("@id_viewer", objViewer.GetId());

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void Delete(int viewerId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Viewer SET isActive = @isActive WHERE id_viewer = @id_viewer";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@isActive", false);
            cmd.Parameters.AddWithValue("@id_viewer", viewerId);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public List<Viewer> List()
        {
            List<Viewer> viewerList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Viewer Where isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                viewerList = new List<Viewer>();

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
                    viewerList.Add(objViewer);
                }
            }
            conn.Close();

            return viewerList;
        }

        public bool SignIn(string viewerEmail, string viewerEncryptedPassword)
        {
            bool validViewer = false;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Viewer WHERE email_viewer = @email_viewer AND pw_viwer = @pw_viwer";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_viewer", viewerEmail);
            cmd.Parameters.AddWithValue("@pw_viwer", viewerEncryptedPassword);

            SqlDataReader dr = cmd.ExecuteReader();
            validViewer = dr.HasRows;

            return validViewer;
        }
    }
}