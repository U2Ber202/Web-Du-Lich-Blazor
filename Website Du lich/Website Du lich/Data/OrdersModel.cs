namespace Website_Du_lich.Data
{
    public class OrdersModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int TourId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }
}
