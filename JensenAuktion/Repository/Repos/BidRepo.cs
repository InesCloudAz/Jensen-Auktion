﻿using Dapper;
using JensenAuktion.Repository.Entities;
using System.Data;
using JensenAuktion.Repository.Interfaces;
using JensenAuktion.Interfaces;

namespace JensenAuktion.Repository.Repos
{
    public class BidRepo : IBidRepo
    {
        private readonly IJensenAuctionContext _context;

        public BidRepo(IJensenAuctionContext context)
        {
            _context = context;
        }

        public int CreateBid(Bid bid)
        {
            using (var db = _context.GetConnection())
            {
                db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Price", bid.Price);
                parameters.Add("@AdID", bid.AdID);
                parameters.Add("@UserID", bid.UserID);
                parameters.Add("@NewBidID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                db.Execute("CreateBid", parameters, commandType: CommandType.StoredProcedure);

                return parameters.Get<int>("@NewBidID");
            }
        }

        public bool DeleteBid(int bidID)
        {
            using (var db = _context.GetConnection())
            {
                db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BidID", bidID);
                parameters.Add("@IsDeleted", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                db.Execute("DeleteBid", parameters, commandType: CommandType.StoredProcedure);

                return parameters.Get<bool>("@IsDeleted");
            }
        }

        public Bid GetBidById(int bidId)
        {
            using (var db = _context.GetConnection())
            {
                db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BidId", bidId);

                return db.Query<Bid>("GetBidById", parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();

            }
        }

        public bool HasBids(int adId)
        {
            using (var db = _context.GetConnection())
            {
                db.Open();

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AdID", adId);
                parameters.Add("@HasBids", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                db.Execute("HasBids", parameters, commandType: CommandType.StoredProcedure);

                return parameters.Get<bool>("@HasBids");
            }
        }
    }
}
