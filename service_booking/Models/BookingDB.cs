using System.Data;
using System.Data.SqlClient;

namespace service_booking.Models
{
    public class BookingDB
    {
        private readonly string _connectionString;

        public BookingDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void InsertBooking(int userId, BookServiceView model)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_InsertBooking", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@user_id", userId);
            cmd.Parameters.AddWithValue("@vehicle_id", model.vehicle_id);
            cmd.Parameters.AddWithValue("@service_type_id", model.service_type_id);
            cmd.Parameters.AddWithValue("@slot_id", model.slot_id);
            cmd.Parameters.AddWithValue("@status", "Pending");

            con.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Bookings> GetBookingHistory(int userId)
        {
            List<Bookings> list = new();

            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_GetBookingsByUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user_id", userId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new Bookings
                {
                    booking_id = (int)dr["booking_id"],
                    booking_status = dr["booking_status"].ToString(),
                    date = (DateTime)dr["date"]
                });
            }
            return list;
        }
    }
}
