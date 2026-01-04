using System.Data;
using System.Data.SqlClient;

namespace service_booking.Models
{
    public class AdminBookingDB
    {
        private readonly string _con;
        public AdminBookingDB(IConfiguration c) => _con = c.GetConnectionString("DefaultConnection");

        public List<AdminBookingView> GetAllBookings()
        {
            List<AdminBookingView> list = new();

            using SqlConnection con = new(_con);
            SqlCommand cmd = new("sp_AdminGetAllBookings", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new AdminBookingView
                {
                    booking_id = (int)dr["booking_id"],
                    user_name = dr["user_name"].ToString(),
                    vehicle_number = dr["vehicle_number"].ToString(),
                    service_name = dr["service_name"].ToString(),
                    booking_status = dr["booking_status"].ToString()
                });
            }


            return list;
        }

        public void UpdateBookingStatus(int bookingId, string status)
        {
            using SqlConnection con = new(_con);
            SqlCommand cmd = new("sp_UpdateBookingStatus", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@booking_id", bookingId);
            cmd.Parameters.AddWithValue("@status", status);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
