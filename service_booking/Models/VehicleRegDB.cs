using System.Data;
using System.Data.SqlClient;

namespace service_booking.Models
{
    public class VehicleRegDB
    {
        private readonly string _connectionString;

        public VehicleRegDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // INSERT VEHICLE
        public void InsertVehicle(VehicleRegister v)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_InsertVehicle", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = v.user_id;
            cmd.Parameters.Add("@vehicle_number", SqlDbType.NVarChar, 50).Value = v.vehicle_number;
            cmd.Parameters.Add("@brand", SqlDbType.NVarChar, 50).Value = v.brand;
            cmd.Parameters.Add("@model", SqlDbType.NVarChar, 50).Value = v.model;
            cmd.Parameters.Add("@manufacturing_year", SqlDbType.Int).Value = v.manufacturing_year;

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // GET VEHICLES BY USER
        public List<VehicleRegister> GetVehicles(int userId)
        {
            List<VehicleRegister> list = new();

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_GetVehiclesByUser", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new VehicleRegister
                {
                    vehicle_id = (int)dr["vehicle_id"],
                    user_id = (int)dr["user_id"],
                    vehicle_number = dr["vehicle_number"].ToString(),
                    brand = dr["brand"].ToString(),
                    model = dr["model"].ToString(),
                    manufacturing_year = (int)dr["manufacturing_year"]
                });
            }

            return list;
        }
    }
}
