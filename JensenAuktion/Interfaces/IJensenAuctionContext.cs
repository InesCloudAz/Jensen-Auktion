using JensenAuktion.Repository.Entities;
using Microsoft.Data.SqlClient;

namespace JensenAuktion.Interfaces
{
    public interface IJensenAuctionContext
    {
        SqlConnection GetConnection();
    }
}
