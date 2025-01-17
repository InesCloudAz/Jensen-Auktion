using Dapper;
using JensenAuktion.Interfaces;
using JensenAuktion.Repository.Entities;
using JensenAuktion.Repository.Interfaces;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data;

namespace JensenAuktion.Repository.Repos
{
    public class AdRepository : IAdRepository
    {
        private readonly IJensenAuctionContext _context;

        public AdRepository(IJensenAuctionContext context)
        {
            _context = context;
        }
        public void CreateAd(Ad ad)
        {
            using (IDbConnection db = _context.GetConnection())
            {
                db.Open();

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Title", ad.Title);
                parameters.Add("@Description", ad.Description);
                parameters.Add("@Price", ad.Price);
                parameters.Add("@StartTime", ad.StartTime);
                parameters.Add("@EndTime", ad.EndTime);
                parameters.Add("@UserId", ad.UserId);

                db.Execute("CreateAd", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Ad> GetAllAds()
        {
            using (IDbConnection db = _context.GetConnection())
            {
                db.Open();

                var Ads = db.Query<Ad>("GetAllAds", commandType: CommandType.StoredProcedure).ToList();

                return Ads;
            }
        }

        // Updatera ad
        public void UpdateAd(Ad ad)
        {
            using (IDbConnection db = _context.GetConnection())
            {
                db.Open();

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Title", ad.Title);
                parameters.Add("@Description", ad.Description);
                parameters.Add("@Price", ad.Price);
                parameters.Add("@StartTime", ad.StartTime);
                parameters.Add("@EndTime", ad.EndTime);
                parameters.Add("@UserId", ad.UserId);
                parameters.Add("@AdId", ad.AdID);

                db.Execute("UpdateAd", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // radera ad
        public void DeleteAd(int adId)
        {
            using (IDbConnection db = _context.GetConnection())
            {
                db.Open();

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@AdId", adId);

                db.Execute("DeleteAd", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }

}
