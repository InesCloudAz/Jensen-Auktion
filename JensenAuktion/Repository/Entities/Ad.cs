namespace JensenAuktion.Repository.Entities
{
    public class Ad
    {
        internal object Id;

        public int AdID  { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }   

       public int OwnerID { get; set; }
    }
}
