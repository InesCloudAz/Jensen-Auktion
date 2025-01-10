using Dapper;
using JensenAuktion.Repository.Entities;
using Microsoft.Data.SqlClient;
using System.Data;

namespace JensenAuktion.Repository.Repos
{
    public class UserRepo
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

    }
}
