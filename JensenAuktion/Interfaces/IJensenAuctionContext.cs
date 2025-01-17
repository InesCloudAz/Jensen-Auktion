using JensenAuktion.Repository.Entities;
using Microsoft.Data.SqlClient;

namespace JensenAuktion.Interfaces
{
    public interface IJensenAuctionContext
    {
        object Ads { get; }
        object Ad { get; set; }
        void AddAd(Ad newAd);
        void CreateAd(Ad newAd);
        void DeleteAd(Ad ad);
        object GetAdById(int id);
        object GetAdById(object id);
        object GetAllAds();
        SqlConnection GetConnection();
        void SaveChanges();
        void UpdateAd(Ad updatedAd);
        void UpdateAd(object ad);
    }
}
