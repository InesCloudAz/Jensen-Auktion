using JensenAuktion.Repository.Entities;

namespace JensenAuktion.Repository.Interfaces
{
    public interface IBid
    {
        Bid CreateBid(Bid bid);
        Bid DeleteBid(Bid bid);

    }
}
