﻿using System.ComponentModel.DataAnnotations;

namespace Website_Du_lich.Data
{
    public class OrdersModel
    {
        [Key]
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int TourId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public int? HotelId { get; set; }
        public int? SBId { get; set; }
        public int? CarId { get; set; }
    }
}
