namespace Website_Du_lich.Data
{
    public class TourModel
    {
        public int TourId { get; set; }
        public string TourName { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
