using Microsoft.Data.SqlClient;

namespace JensenAuktion.Interfaces
{
    public interface IJensenAuctionContext
    {
        public SqlConnection GetConnection();
    }
}
