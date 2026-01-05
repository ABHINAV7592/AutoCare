using System.Data;
using System.Data.SqlClient;

namespace service_booking.Models
{
    public class ServiceDB
    {
        private readonly string _connectionString;

        public ServiceDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ServiceType> GetServices()
        {
            List<ServiceType> list = new();

            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_GetAllServiceTypes", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new ServiceType
                {
                    service_type_id = Convert.ToInt32(dr["service_type_id"]),
                    service_name = dr["service_name"].ToString(),
                    description = dr["description"].ToString(),
                    cost = Convert.ToDecimal(dr["cost"])
                });
            }
            con.Close();
            return list;
        }

        public void InsertService(ServiceType service)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_InsertServiceType", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@service_name", service.service_name);
            cmd.Parameters.AddWithValue("@description", service.description);
            cmd.Parameters.AddWithValue("@cost", service.cost);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
