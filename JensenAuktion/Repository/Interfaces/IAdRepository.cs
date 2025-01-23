using JensenAuktion.Repository.DTO;
using JensenAuktion.Repository.Entities;
using Microsoft.Data.SqlClient;

namespace JensenAuktion.Repository.Interfaces
{
    public interface IAdRepository
    {
        public void CreateAd(AdsCreateDTO ad);
        public void DeleteAd(int id);
        public AdsDTO GetAdById(int adID);
        public List<Ad> GetAllAds();
        public void UpdateAd(Ad ad); 
        public void UpdateAdWithBid(Ad ad);
        public bool DeleteAdIfNoBids(int adId);
        public bool UpdateAdWithBidCheck(Ad ad);
        public Ad GetClosedAdById(int adID);
    }
            
        
    
}
   