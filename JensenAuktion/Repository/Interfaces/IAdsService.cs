using JensenAuktion.Repository.Entities;

namespace JensenAuktion.Repository.Interfaces
{
    public interface IAdsService
    {

        bool IsAdClosed(int adId);
        bool CheckIfAdHasBids(int adId);
        public bool IsAdClosedByBid(int id);
        public int CheckAdUserId(int id);
        public Bid GetHighestBid(int id);

    }
}
