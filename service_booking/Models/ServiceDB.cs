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
            SqlCommand cmd = new SqlCommand("sp_GetServiceTypes", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new ServiceType
                {
                    service_type_id = (int)dr["service_type_id"],
                    service_name = dr["service_name"].ToString(),
                    description = dr["description"].ToString(),
                    cost = (decimal)dr["cost"]
                });
            }
            return list;
        }
    }
}
