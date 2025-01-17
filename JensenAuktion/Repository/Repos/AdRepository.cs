using JensenAuktion.Interfaces;
using JensenAuktion.Repository.Entities;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace JensenAuktion.Repository.Repos
{
    public class AdRepository
    {
        private readonly IJensenAuctionContext _context;

        public AdRepository(IJensenAuctionContext context)
        {
            _context = context;
        }

        // skapa ad
        public void CreateAd(Ad ad)
        {
            if (_context.Ads is List<Ad> ads)
            {
                ads.Add(ad);
                _context.SaveChanges();
            }
        }

        public List<Ad> GetAllAds()
        {
            return (_context.Ads as IEnumerable<Ad>)?.ToList() ?? new List<Ad>();
        }

        // Get ad by ID
        public Ad GetAdById(int id)
        {
            return (_context.Ads as IEnumerable<Ad>)?.FirstOrDefault(ad => ad.AdID == id);
        }

        // Updatera ad
        public void UpdateAd(Ad ad)
        {
            var ads = _context.Ads as List<Ad>;
            var existingAd = ads?.FirstOrDefault(a => a.AdID == ad.AdID);
            if (existingAd != null)
            {
                ads.Remove(existingAd);
                ads.Add(ad);
                _context.SaveChanges();
            }
        }

        // radera ad
        public void DeleteAd(Ad ad)
        {
            if (_context.Ads is List<Ad> ads)
            {
                ads.Remove(ad);
                _context.SaveChanges();
            }
        }
    }

}
