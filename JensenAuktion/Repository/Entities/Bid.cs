namespace JensenAuktion.Repository.Entities
{
    public class Bid
    {
        public int BidID { get; set; }
        public float Price { get; set; }
        public int AdID { get; set; }
        public int UserID { get; set; }
    }
}
