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
                    slot_id = (int)dr["slot_id"],
                    slot_date = (DateTime)dr["slot_date"],
                    start_time = (TimeSpan)dr["start_time"],
                    end_time = (TimeSpan)dr["end_time"],
                    max_bookings = (int)dr["max_bookings"]
                });
            }
            return list;
        }
    }
}
