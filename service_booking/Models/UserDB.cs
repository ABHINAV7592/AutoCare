using System.Data;
using System.Data.SqlClient;

namespace service_booking.Models
{
    public class UserDB
    {
            private readonly string _connectionString;

            public UserDB(IConfiguration configuration)
            {
                _connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            SqlConnection con => new SqlConnection(_connectionString);

          
            public int GetMaxRegId()
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_maxregid", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    var result = cmd.ExecuteScalar();
                    con.Close();

                    return result == DBNull.Value ? 0 : Convert.ToInt32(result); //checking null value, ternary operator thst is condition ? true_value : false_value.

            }
        }

            
            public int GetEmailCount(string email, string password)
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_countregid", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@pswd", password);

                    con.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();

                    return count;
                }
            }

            
            public void InsertUser(string name, string phone)
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@phone", phone);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            
            public void InsertLogin(int regId, string email, string password, string loginType)
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertLogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@reg_id", regId);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@login_type", loginType);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        public LoginResult? Login(string email, string password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Login", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new LoginResult
                    {
                        reg_id = Convert.ToInt32(dr["reg_id"]),
                        login_type = dr["login_type"].ToString()
                    };
                }

                con.Close();
                return null; 
            }
        }

    }
}

