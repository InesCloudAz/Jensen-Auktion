using Dapper;
using JensenAuktion.Interfaces;
using JensenAuktion.Repository.DTO;
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
        public void CreateAd(AdsCreateDTO ad)
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

                var Ads = db.Query<Ad, Bid, Ad>("GetAllAds",(ad, bid) =>
                {
                    ad.Bid = bid;
                    return ad;                    

                }, splitOn: "BidId", commandType: CommandType.StoredProcedure).ToList();

                return Ads;
            }
        }

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

        public void UpdateAdWithBid(Ad ad)
        {
            using (IDbConnection db = _context.GetConnection())
            {
                db.Open();

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@Title", ad.Title);
                parameters.Add("@Description", ad.Description);
                parameters.Add("@StartTime", ad.StartTime);
                parameters.Add("@EndTime", ad.EndTime);
                parameters.Add("@UserId", ad.UserId);
                parameters.Add("@AdId", ad.AdID);

                db.Execute("UpdateAdWithBid", parameters, commandType: CommandType.StoredProcedure);
            }
        }

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
        public AdsDTO GetAdById(int adID)
        {
            using (IDbConnection db = _context.GetConnection())
            {
                db.Open();

                var adDictionary = new Dictionary<int, AdsDTO>();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AdId", adID);

                var searchedAd = db.Query<AdsDTO, Bid, AdsDTO>("GetAdById", (ad, bid) =>
                {
                    // Check if the ad already exists in the dictionary
                    if (!adDictionary.TryGetValue(ad.AdID, out var currentAd))
                    {
                        currentAd = ad;
                        currentAd.Bid = new List<Bid>();
                        adDictionary.Add(currentAd.AdID, currentAd);
                    }

                    // Add bid to the ad's bid list if it's not null
                    if (bid != null)
                    {
                        currentAd.Bid.Add(bid);
                    }

                    return currentAd;
                }, parameters, splitOn: "BidId", commandType: CommandType.StoredProcedure).FirstOrDefault();

                return searchedAd;
            }
        }

        public Ad GetClosedAdById(int adID)
        {
            using (IDbConnection db = _context.GetConnection())
            {
                db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AdId", adID);

                var searchedAd = db.Query<Ad, Bid, Ad>("GetAdById",(ad, bid) =>
                {
                    ad.Bid = bid;
                    return ad;
                }, parameters, splitOn:("BidId"), commandType: CommandType.StoredProcedure).FirstOrDefault();

                return searchedAd;
            }
        }

        public bool DeleteAdIfNoBids(int adId)
        {
            using (IDbConnection db = _context.GetConnection())
            {
                db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AdId", adId);

                bool hasBids = db.QuerySingleOrDefault<bool>("CheckIfAdHasBids", parameters, commandType: CommandType.StoredProcedure);

                if (!hasBids)
                {
                    db.Execute("DeleteAd", parameters, commandType: CommandType.StoredProcedure);
                    return true; // Successfully deleted
                }
            }

            return false; // Not deleted because bids exist or ad not found
        }

        public bool UpdateAdWithBidCheck(Ad ad)
        {
            using (IDbConnection db = _context.GetConnection())
            {
                db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AdId", ad.AdID);

                bool hasBids = db.QuerySingleOrDefault<bool>("CheckIfAdHasBids", parameters, commandType: CommandType.StoredProcedure);

                // Retrieve the existing ad from the database
                var existingAd = db.QuerySingleOrDefault<Ad>("GetAdById", parameters, commandType: CommandType.StoredProcedure);
                if (existingAd == null)
                {
                    return false; // Ad not found
                }

                // Update the non-price fields (always allowed)
                existingAd.Title = ad.Title;
                existingAd.Description = ad.Description;
                existingAd.StartTime = ad.StartTime;
                existingAd.EndTime = ad.EndTime;
                existingAd.UserId = ad.UserId;

                // Update the price only if no bids exist
                if (!hasBids)
                {
                    existingAd.Price = ad.Price;
                }

                // Save the changes to the database
                parameters = new DynamicParameters();
                parameters.Add("@AdId", existingAd.AdID);
                parameters.Add("@Title", existingAd.Title);
                parameters.Add("@Description", existingAd.Description);
                parameters.Add("@Price", existingAd.Price);
                parameters.Add("@StartTime", existingAd.StartTime);
                parameters.Add("@EndTime", existingAd.EndTime);
                parameters.Add("@UserId", existingAd.UserId);

                db.Execute("UpdateAd", parameters, commandType: CommandType.StoredProcedure);
                return true; // Successfully updated
            }
        }

    }

}
