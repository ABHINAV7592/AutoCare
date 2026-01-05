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

        public int InsertMechanic(string name, string phone, string expertise)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_InsertMechanic", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@expertise", expertise);

            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void InsertLogin(int mechanicId, string email, string password)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_InsertLogin", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@reg_id", mechanicId);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@login_type", "Mechanic");

            con.Open();
            cmd.ExecuteNonQuery();
        }

        public int CheckEmailExists(string email)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_CheckEmailExists", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@email", email);

            con.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public string GetExpertiseByMechanicId(int mechanicId)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_GetMechanicExpertise", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@mechanic_id", mechanicId);

            con.Open();
            return cmd.ExecuteScalar()?.ToString() ?? "";
        }

        public string GetExpertiseByLoginId(int mechanicId)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(
                "SELECT expertise FROM Mechanic WHERE mechanic_id = @id", con);

            cmd.Parameters.AddWithValue("@id", mechanicId);

            con.Open();
            return cmd.ExecuteScalar()?.ToString() ?? "";
        }

    }
}
