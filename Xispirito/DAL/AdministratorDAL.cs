using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Xispirito.Models;

namespace Xispirito.DAL
{
    public class AdministratorDAL : IDatabase<Administrator>
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["XispiritoDB"].ConnectionString;

        public void Insert(Administrator objAdministrator)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Administrator VALUES (@nm_administrator, @email_administrator, @pt_administrator, @pw_administrator, @isActive)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nm_administrator", objAdministrator.GetName());
            cmd.Parameters.AddWithValue("@email_administrator", objAdministrator.GetEmail());
            cmd.Parameters.AddWithValue("@pt_administrator", objAdministrator.GetPicture());
            cmd.Parameters.AddWithValue("@pw_administrator", objAdministrator.GetEncryptedPassword());
            cmd.Parameters.AddWithValue("@isActive", objAdministrator.GetIsActive());
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public Administrator Select(int administratorId)
        {
            Administrator objAdministrator = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Viewer WHERE id_viewer = @id_viewer";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id_administrator", administratorId);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objAdministrator = new Administrator(
                    administratorId,
                    dr["nm_administrator"].ToString(),
                    dr["email_administrator"].ToString(),
                    dr["pt_administrator"].ToString(),
                    dr["pw_administrator"].ToString(),
                    Convert.ToBoolean(dr["isActive"])
                );
            }
            conn.Close();

            return objAdministrator;
        }

        public Administrator SearchEmail(string administratorEmail)
        {
            Administrator objAdministrator = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Administrator WHERE email_administrator = @email_administrator";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_administrator", administratorEmail);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objAdministrator = new Administrator(
                    Convert.ToInt32(dr["id_administrator"]),
                    dr["nm_administrator"].ToString(),
                    dr["email_administrator"].ToString(),
                    dr["pt_administrator"].ToString(),
                    dr["pw_administrator"].ToString(),
                    Convert.ToBoolean(dr["isActive"])
                );
            }
            conn.Close();

            return objAdministrator;
        }

        public Administrator SearchEmail(string administratorEmail, string administratorEncryptedPassword)
        {
            Administrator objAdministrator = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Administrator WHERE email_administrator = @email_administrator AND pw_administrator = @pw_administrator";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_administrator", administratorEmail);
            cmd.Parameters.AddWithValue("@pw_administrator", administratorEncryptedPassword);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objAdministrator = new Administrator(
                    Convert.ToInt32(dr["id_administrator"]),
                    dr["nm_administrator"].ToString(),
                    dr["email_administrator"].ToString(),
                    dr["pt_administrator"].ToString(),
                    administratorEncryptedPassword,
                    Convert.ToBoolean(dr["isActive"])
                );
            }
            conn.Close();

            return objAdministrator;
        }

        public void Update(Administrator objAdministrator)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Administrator SET nm_administrator = @nm_administrator, email_administrator = @email_administrator, pt_administrator = @pt_administrator, pw_administrator = @pw_administrator, isActive = @isActive WHERE id_administrator = @id_administrator";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nm_administrator", objAdministrator.GetName());
            cmd.Parameters.AddWithValue("@email_administrator", objAdministrator.GetEmail());
            cmd.Parameters.AddWithValue("@pt_administrator", objAdministrator.GetPicture());
            cmd.Parameters.AddWithValue("@pw_administrator", objAdministrator.GetEncryptedPassword());
            cmd.Parameters.AddWithValue("@isActive", objAdministrator.GetIsActive());
            cmd.Parameters.AddWithValue("@id_administrator", objAdministrator.GetId());

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void Delete(int administratorId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Administrator SET isActive = @isActive WHERE id_administrator = @id_administrator";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@isActive", false);
            cmd.Parameters.AddWithValue("@id_administrator", administratorId);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public List<Administrator> List()
        {
            List<Administrator> administratorList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Viewer Where isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                administratorList = new List<Administrator>();

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
                    administratorList.Add(objAdministrator);
                }
            }
            conn.Close();

            return administratorList;
        }

        public bool SignIn(string administratorEmail, string administratorEncryptedPassword)
        {
            bool validSpeaker = false;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Administrator WHERE email_administrator = @email_administrator AND pw_administrator = @pw_administrator";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@email_administrator", administratorEmail);
            cmd.Parameters.AddWithValue("@pw_administrator", administratorEncryptedPassword);

            SqlDataReader dr = cmd.ExecuteReader();
            validSpeaker = dr.HasRows;

            return validSpeaker;
        }
    }
}