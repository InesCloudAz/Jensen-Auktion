using JensenAuktion.Interfaces;
using Microsoft.Data.SqlClient;

namespace JensenAuktion
{
    public class JensenAuctionContext : IJensenAuctionContext
    {
        private readonly string _connString;

        public JensenAuctionContext(IConfiguration config)
        {
            _connString = config.GetConnectionString("JensenAuktion");
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connString);
        }
    }
}
