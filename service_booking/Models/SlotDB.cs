using System.Data;
using System.Data.SqlClient;

namespace service_booking.Models
{
    public class SlotDB
    {
        private readonly string _connectionString;

        public SlotDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<TimeSlot> GetAllSlots()
        {
            List<TimeSlot> list = new();

            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_GetAllSlots", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new TimeSlot
                {
                    slot_id = Convert.ToInt32(dr["slot_id"]),
                    slot_date = Convert.ToDateTime(dr["slot_date"]),
                    start_time = (TimeSpan)dr["start_time"],
                    end_time = (TimeSpan)dr["end_time"],
                    max_bookings = Convert.ToInt32(dr["max_bookings"])
                });
            }
            con.Close();
            return list;
        }

        public List<TimeSlot> GetAvailableSlots()
        {
            List<TimeSlot> list = new();

            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_GetAvailableSlots", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new TimeSlot
                {
                    slot_id = Convert.ToInt32(dr["slot_id"]),
                    slot_date = Convert.ToDateTime(dr["slot_date"]),
                    start_time = (TimeSpan)dr["start_time"],
                    end_time = (TimeSpan)dr["end_time"],
                    max_bookings = Convert.ToInt32(dr["max_bookings"])
                });
            }
            con.Close();
            return list;
        }

        public void InsertSlot(TimeSlot slot)
        {
            using SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("sp_InsertSlot", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@slot_date", slot.slot_date);
            cmd.Parameters.AddWithValue("@start_time", slot.start_time);
            cmd.Parameters.AddWithValue("@end_time", slot.end_time);
            cmd.Parameters.AddWithValue("@max_bookings", slot.max_bookings);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
