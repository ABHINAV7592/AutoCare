using System.Data;
using System.Data.SqlClient;

namespace service_booking.Models
{
    public class VehicleDB
    {
        private readonly string _connectionString;

        public VehicleDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void InsertVehicle(Vehicle obj)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_InsertVehicle", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", obj.user_id);
            cmd.Parameters.AddWithValue("@vehicle_number", obj.vehicle_number);
            cmd.Parameters.AddWithValue("@model", obj.model);
            cmd.Parameters.AddWithValue("@vehicle_type", obj.brand);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Vehicle> GetVehiclesByUser(int userId)
        {
            List<Vehicle> list = new();

            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_GetVehiclesByUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_id", userId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new Vehicle
                {
                    vehicle_id = (int)dr["vehicle_id"],
                    vehicle_number = dr["vehicle_number"].ToString(),
                    model = dr["model"].ToString(),
                    brand = dr["vehicle_type"].ToString()
                });
            }
            return list;
        }
    }
}

