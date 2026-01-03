using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace service_booking.Models
{
    public class MechanicDB
    {
        private readonly string _connectionString;

        public MechanicDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        
        public int GetMaxRegId()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_maxregid", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                object result = cmd.ExecuteScalar();
                con.Close();

                return result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
        }

       
        public int GetEmailCount(string email, string password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_countregid", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@pswd", password);

                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                return count;
            }
        }

        // 🔹 Insert Mechanic
        public void InsertMechanic(string name, string phone, string expertise)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertMechanic", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@expertise", expertise);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

       
        public void InsertLogin(int regId, string email, string password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_InsertLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@reg_id", regId);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@login_type", "Mechanic");

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
