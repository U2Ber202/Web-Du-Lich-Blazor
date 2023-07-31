using System.ComponentModel.DataAnnotations;

namespace Website_Du_lich.Data
{
    public class PaymentModel
    {
        [Key]
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentMethod { get; set; }
    }
}
