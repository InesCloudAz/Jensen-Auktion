namespace JensenAuktion.Repository.Entities
{
    public class Ad
    {
        public int AdID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int UserId { get; set; }
        public Bid? Bid { get; set; }
    }
}
