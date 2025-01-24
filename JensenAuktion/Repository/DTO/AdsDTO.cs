using JensenAuktion.Repository.Entities;

namespace JensenAuktion.Repository.DTO
{
    public class AdsDTO
    {        
        public int AdID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int UserId { get; set; }
        public List<Bid> Bid { get; set; }
    }

    public class AdsCreateDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
        public int UserId { get; set; }
    }
}
