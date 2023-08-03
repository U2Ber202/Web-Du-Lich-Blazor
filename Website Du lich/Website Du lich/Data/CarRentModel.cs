using System.ComponentModel.DataAnnotations;

namespace Website_Du_lich.Data
{
    public class CarRentModel
    {
        [Key]
        public int CarRentId { get; set; }
        public int TourId { get; set; } // Liên kết với tour mà thuê xe đi
        public string CarModel { get; set; } // Hãng xe
        public string CarType { get; set; } // Loại xe
        public DateTime StartDate { get; set; } // Ngày bắt đầu thuê
        public DateTime EndDate { get; set; } // Ngày kết thúc thuê
        public string PickupLocation { get; set; } // Địa điểm nhận xe
        public string DropOffLocation { get; set; } // Địa điểm trả xe
        public int NumberOfCars { get; set; } // Số lượng xe cần thuê
    }
}
