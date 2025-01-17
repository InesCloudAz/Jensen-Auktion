using Dapper;
using JensenAuktion.Interfaces;
using JensenAuktion.Repository.Entities;
using JensenAuktion.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace JensenAuktion.Repository.Repos
{
    public class UserRepo : IUserRepo
    {

        private readonly IJensenAuctionContext _context;

        public  UserRepo(IJensenAuctionContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            using (IDbConnection db = _context.GetConnection())
            {
                db.Open();

                return db.Query<User>("GetAllUsers"
                    , commandType: CommandType.StoredProcedure)
                    .ToList();
            }

        }

        public void InsertUser(User user)
        {
            using (IDbConnection db = _context.GetConnection())
            {
                db.Open();
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@UserName", user.UserName);
                parameters.Add("@Password", user.Password);


                db.Execute("InsertUser"
                        , parameters
                        , commandType: CommandType.StoredProcedure);

            }

        }

        public void UpdateUser(User user)
        {
            using (IDbConnection db = _context.GetConnection())
            {
                db.Open();
                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@UserID", user.UserID);
                parameters.Add("@UserName", user.UserName);
                parameters.Add("@Password", user.Password);


                db.Execute("UpdateUser"
                        , parameters
                        , commandType: CommandType.StoredProcedure);

            }
        }

        public int? LoginUser(string userName, string password)
        {
            using (SqlConnection connection = _context.GetConnection())
            {
                using (SqlCommand command = new SqlCommand("LoginUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    return result == null ? (int?)null : Convert.ToInt32(result);
                }
            }
        }

    }
}
