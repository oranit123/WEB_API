using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DamoFullPrj.Models
{
    public class DBServices
    {
        static string conParent = @"Data Source=ORANITGERBI\SQLEXPRESS;Initial Catalog=MiddleProject;Integrated Security=True";
        public static User Login(string email, string pass)
        {
            User user = null;
            using (SqlConnection con = new SqlConnection(conParent))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Parents WHERE email = @Email AND passwordUserd = @Password", con);

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", pass);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    user = new User
                    {
                        id = rdr["idParent"].ToString(),
                        firstName = rdr["firstName"].ToString(),
                        lastName = rdr["lastName"].ToString(),
                        email = rdr["email"].ToString(),
                        password = rdr["passwordUserd"].ToString()
                    };
                }
                con.Close();
            }

            return user;
        }


        public static bool AddParent(User user)
        {
            using (SqlConnection con = new SqlConnection(conParent))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Parents (idParent, firstName, lastName, email, passwordUserd) " +
                    "VALUES (@Id, @FirstName, @LastName, @Email, @Password)", con);

                cmd.Parameters.AddWithValue("@Id", user.id);
                cmd.Parameters.AddWithValue("@FirstName", user.firstName);
                cmd.Parameters.AddWithValue("@LastName", user.lastName);
                cmd.Parameters.AddWithValue("@Email", user.email);
                cmd.Parameters.AddWithValue("@Password", user.password);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }


        public static bool EditParent(User user)
        {
            using (SqlConnection con = new SqlConnection(conParent))
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Parents SET firstName = @FirstName, lastName = @LastName, email = @Email, passwordUserd = @Password " +
                    "WHERE idParent = @Id", con);

                cmd.Parameters.AddWithValue("@FirstName", user.firstName);
                cmd.Parameters.AddWithValue("@LastName", user.lastName);
                cmd.Parameters.AddWithValue("@Email", user.email);
                cmd.Parameters.AddWithValue("@Password", user.password);
                cmd.Parameters.AddWithValue("@Id", user.id);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }

        public static bool DeleteParent(string parentId)
        {
            using (SqlConnection con = new SqlConnection(conParent))
            {
                SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Parents WHERE idParent = @Id", con);

                cmd.Parameters.AddWithValue("@Id", parentId);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }
        public static List<User> GetAllParents()
        {
            List<User> parents = new List<User>();

            using (SqlConnection con = new SqlConnection(conParent))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Parents", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    User parent = new User
                    {
                        id = rdr["idParent"].ToString(),
                        firstName = rdr["firstName"].ToString(),
                        lastName = rdr["lastName"].ToString(),
                        email = rdr["email"].ToString(),
                        password = rdr["passwordUserd"].ToString()
                    };

                    parents.Add(parent);
                }

                con.Close();
            }

            return parents;
        }
    }



}

