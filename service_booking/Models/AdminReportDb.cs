using System.Data;
using System.Data.SqlClient;

namespace service_booking.Models
{
    public class AdminReportDB
    {
        private readonly string _con;
        public AdminReportDB(IConfiguration c) => _con = c.GetConnectionString("DefaultConnection");

        public List<ServiceReport> GetServiceReport()
        {
            List<ServiceReport> list = new();

            using SqlConnection con = new(_con);
            SqlCommand cmd = new("sp_ServiceBookingCount", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new ServiceReport
                {
                    service_name = dr["service_name"].ToString(),
                    booking_count = (int)dr["booking_count"]
                });
            }
            con.Close();
            return list;
        }
    }
}
