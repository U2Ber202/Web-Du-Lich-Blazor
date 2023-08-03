using System.ComponentModel.DataAnnotations;

namespace Website_Du_lich.Data
{
    public class HotelModel
    {
        [Key]
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Price { get; set; }
        public string RoomType { get; set; }
        public int AvailableRooms { get; set; }
        public string Amenities { get; set; }
        public double Rating { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
