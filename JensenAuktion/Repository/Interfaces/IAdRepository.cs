using JensenAuktion.Repository.Entities;
using Microsoft.Data.SqlClient;

namespace JensenAuktion.Repository.Interfaces
{
    public interface IAdRepository
    {
        public void CreateAd(Ad ad);
        public void DeleteAd(int id);
        public List<Ad> GetAllAds();
        public void UpdateAd(Ad ad);
    }
}
