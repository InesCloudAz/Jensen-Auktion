
namespace JensenAuktion.Repository
{
    public class AuctionDbContext
    {
        public object Ads { get; internal set; }

        internal async Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}