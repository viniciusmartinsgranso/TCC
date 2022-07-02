using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Xispirito.Models;

namespace Xispirito.DAL
{
    public class LectureDAL : IDatabase<Lecture>
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["XispiritoDB"].ConnectionString;

        public void Insert(Lecture objLecture)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "INSERT INTO Lecture VALUES (@nm_lecture, @pt_lecture, @tm_lecture, @dt_lecture, @dc_lecture, @mod_lecture, @adr_lecture, @lt_lecture, @isActive)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nm_lecture", objLecture.GetName());
            cmd.Parameters.AddWithValue("@pt_lecture", objLecture.GetPicture());
            cmd.Parameters.AddWithValue("@tm_lecture", objLecture.GetTime());
            cmd.Parameters.AddWithValue("@dt_lecture", objLecture.GetDate());
            cmd.Parameters.AddWithValue("@dc_lecture", objLecture.GetDescription());

            int modality = 0;
            foreach (string enumModality in Enum.GetNames(typeof(Modality)))
            {
                if (objLecture.GetModality() == enumModality)
                {
                    break;
                }
                modality++;
            }
            cmd.Parameters.AddWithValue("@mod_lecture", modality);

            cmd.Parameters.AddWithValue("@adr_lecture", objLecture.GetAddress());
            cmd.Parameters.AddWithValue("@lt_lecture", objLecture.GetLimit());
            cmd.Parameters.AddWithValue("@isActive", objLecture.GetIsActive());
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public Lecture Select(int lectureId)
        {
            Lecture objLecture = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Lecture WHERE id_lecture = @id_lecture";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id_lecture", lectureId);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows && dr.Read())
            {
                objLecture = new Lecture(
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
            }
            conn.Close();

            return objLecture;
        }

        public int GetLastId()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT MAX(id_lecture) AS LastId FROM Lecture";

            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            int lastId = 0;
            if (dr.HasRows && dr.Read())
            {
                lastId = Convert.ToInt32(dr["LastId"]);
            }
            conn.Close();

            return lastId;
        }

        public List<Lecture> UpcomingLecturesList()
        {
            List<Lecture> lectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Lecture WHERE isActive = 1 AND dt_lecture > GETDATE() ORDER BY dt_lecture ASC";

            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                lectureList = new List<Lecture>();

                while (dr.Read())
                {
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
                    lectureList.Add(objLecture);
                }
            }
            conn.Close();

            return lectureList;
        }

        public List<Lecture> UpcomingLecturesList(int maxQuantity)
        {
            List<Lecture> lectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Lecture WHERE isActive = 1 AND dt_lecture > GETDATE() ORDER BY dt_lecture ASC";

            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                lectureList = new List<Lecture>();

                for (int i = 0; i < maxQuantity; i++)
                {
                    if (dr.Read())
                    {
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
                        lectureList.Add(objLecture);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            conn.Close();

            return lectureList;
        }

        public List<Lecture> LecturesList()
        {
            List<Lecture> lectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Lecture WHERE isActive = 1 AND dt_lecture > GETDATE() ORDER BY NEWID();";

            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                lectureList = new List<Lecture>();

                while (dr.Read())
                {
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
                    lectureList.Add(objLecture);
                }
            }
            conn.Close();

            return lectureList;
        }

        public List<Lecture> LecturesList(int maxQuantity)
        {
            List<Lecture> lectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Lecture WHERE isActive = 1 AND dt_lecture > GETDATE() ORDER BY NEWID();";

            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                lectureList = new List<Lecture>();

                for (int i = 0; i < maxQuantity; i++)
                {
                    if (dr.Read())
                    {
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
                        lectureList.Add(objLecture);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            conn.Close();

            return lectureList;
        }

        public List<Lecture> SearchLecturesByName(string search)
        {
            List<Lecture> searchLectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Lecture WHERE isActive = 1 AND nm_lecture LIKE @search AND dt_lecture > GETDATE()";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@search", "%" + search + "%");

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                searchLectureList = new List<Lecture>();

                while (dr.Read())
                {
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
                    searchLectureList.Add(objLecture);
                }
            }
            conn.Close();

            return searchLectureList;
        }

        public void Update(Lecture objLecture)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Lecture SET nm_lecture = @nm_lecture, pt_lecture = @pt_lecture, tm_lecture = @tm_lecture, dt_lecture = @dt_lecture, dc_lecture = @dc_lecture, mod_lecture = @mod_lecture, adr_lecture = @adr_lecture, lt_lecture = @lt_lecture, isActive = @isActive WHERE id_lecture = @id_lecture";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@nm_lecture", objLecture.GetName());
            cmd.Parameters.AddWithValue("@pt_lecture", objLecture.GetPicture());
            cmd.Parameters.AddWithValue("@tm_lecture", objLecture.GetTime());
            cmd.Parameters.AddWithValue("@dt_lecture", objLecture.GetDate());
            cmd.Parameters.AddWithValue("@dc_lecture", objLecture.GetDescription());

            int modality = 0;
            foreach (string enumModality in Enum.GetNames(typeof(Modality)))
            {
                if (objLecture.GetModality() == enumModality)
                {
                    break;
                }
                modality++;
            }
            cmd.Parameters.AddWithValue("@mod_lecture", modality);

            cmd.Parameters.AddWithValue("@adr_lecture", objLecture.GetAddress());
            cmd.Parameters.AddWithValue("@lt_lecture", objLecture.GetLimit());
            cmd.Parameters.AddWithValue("@isActive", objLecture.GetIsActive());
            cmd.Parameters.AddWithValue("@id_lecture", objLecture.GetId());

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void Delete(int lectureId)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "UPDATE Lecture SET isActive = @isActive WHERE id_lecture = @id_lecture";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@isActive", false);
            cmd.Parameters.AddWithValue("@id_lecture", lectureId);

            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public List<Lecture> List()
        {
            List<Lecture> lectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Lecture WHERE isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                lectureList = new List<Lecture>();

                while (dr.Read())
                {
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
                    lectureList.Add(objLecture);
                }
            }
            conn.Close();

            return lectureList;
        }

        public List<Lecture> List(string search)
        {
            List<Lecture> lectureList = null;

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sql = "SELECT * FROM Lecture WHERE nm_lecture LIKE @nm_lecture AND isActive = 1";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nm_lecture", "%" + search + "%");

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                lectureList = new List<Lecture>();

                while (dr.Read())
                {
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
                    lectureList.Add(objLecture);
                }
            }
            conn.Close();

            return lectureList;
        }
    }
}