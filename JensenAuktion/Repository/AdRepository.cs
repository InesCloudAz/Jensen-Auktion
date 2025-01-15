namespace JensenAuktion.Repository
{
    public class AdRepository
    {
        private readonly AuctionDbContext _context;

        public AdRepository(AuctionDbContext context)
        {
            _context = context;
        }
    }

}
