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

        //public void InsertVehicle(Vehicle obj)
        //{
        //    using SqlConnection con = new SqlConnection(_connectionString);
        //    using SqlCommand cmd = new SqlCommand("sp_InsertVehicle", con);

        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.AddWithValue("@user_id", obj.user_id);
        //    cmd.Parameters.AddWithValue("@vehicle_number", obj.vehicle_number);
        //    cmd.Parameters.AddWithValue("@brand", obj.brand);
        //    cmd.Parameters.AddWithValue("@model", obj.model);
        //    cmd.Parameters.AddWithValue("@manufacturing_year", obj.manufacturing_year);

        //    con.Open();
        //    int rows = cmd.ExecuteNonQuery();
        //    con.Close();

        //    if (rows == 0)
        //    {
        //        throw new Exception("Vehicle insertion failed.");
        //    }
        //}

        public void InsertVehicle(Vehicle obj)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_InsertVehicle", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", obj.user_id);
            cmd.Parameters.AddWithValue("@vehicle_number", obj.vehicle_number);
            cmd.Parameters.AddWithValue("@brand", obj.brand);
            cmd.Parameters.AddWithValue("@model", obj.model);
            cmd.Parameters.AddWithValue("@manufacturing_year", obj.manufacturing_year);

            con.Open();

            try
            {
                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                    throw new Exception("INSERT returned 0 rows");
            }
            catch (SqlException ex)
            {
                throw new Exception("SQL ERROR: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        public List<Vehicle> GetVehiclesByUser(int userId)
        {
            List<Vehicle> vehicles = new();

            using SqlConnection con = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_GetVehiclesByUser", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_id", userId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                vehicles.Add(new Vehicle
                {
                    vehicle_id = Convert.ToInt32(dr["vehicle_id"]),
                    user_id = Convert.ToInt32(dr["user_id"]),
                    vehicle_number = dr["vehicle_number"].ToString(),
                    brand = dr["brand"].ToString(),
                    model = dr["model"].ToString(),
                    manufacturing_year = Convert.ToInt32(dr["manufacturing_year"])
                });
            }

            con.Close();
            return vehicles;
        }
    }
}

