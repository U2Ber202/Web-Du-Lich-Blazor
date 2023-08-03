using System.ComponentModel.DataAnnotations;

namespace Website_Du_lich.Data
{
    public class ShuttleBookingModel
    {
        [Key]
        public int SBId { get; set; }
        public int TourId { get; set; } // Liên kết với tour mà đặt xe đưa đón
        public int HotelId { get; set; } // Liên kết với hotel mà đặt xe đưa đón
        public DateTime PickupDateTime { get; set; } // Ngày giờ đón
        public string PickupLocation { get; set; } // Địa điểm đón
        public string DropOffLocation { get; set; } // Địa điểm trả
        public int NumberOfPassengers { get; set; } // Số lượng hành khách
    }
}
