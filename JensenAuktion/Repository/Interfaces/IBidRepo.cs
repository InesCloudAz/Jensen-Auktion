using JensenAuktion.Repository.Entities;

namespace JensenAuktion.Repository.Interfaces
{
    public interface IBidRepo
    {
        int CreateBid(Bid bid);
        bool DeleteBid(int bidID);
        bool HasBids(int adId);
        Bid GetBidById(int bidId);
    }
}
