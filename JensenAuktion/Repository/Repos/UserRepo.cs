using Dapper;
using JensenAuktion.Repository.Entities;
using JensenAuktion.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace JensenAuktion.Repository.Repos
{
    public class UserRepo : IUserRepo
    {

        private readonly string _connString;

        public  UserRepo()
        {
            _connString = "Data Source=localhost;Initial Catalog=JensenAuktion;Integrated Security=SSPI; TrustServerCertificate=True;";
        }

        public void InsertUser(User user)
        {
            using (IDbConnection db = new SqlConnection(_connString))
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
            using (IDbConnection db = new SqlConnection(_connString))
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

    }
}
