using JensenAuktion.Repository.Entities;
using Microsoft.Data.SqlClient;

namespace JensenAuktion.Repository.Interfaces
{
    public interface IAdRepository
    {
        void CreateAd(Ad ad);
        void DeleteAd(int id);
        Ad GetAdById(int adID);
        List<Ad> GetAllAds();
        void UpdateAd(Ad ad); // Added method definition

        bool DeleteAdIfNoBids(int adId);
        bool UpdateAdWithBidCheck(Ad ad);
    }
            
        
    
}
   