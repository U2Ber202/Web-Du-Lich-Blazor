using System.ComponentModel.DataAnnotations;

namespace Website_Du_lich.Data
{
    public class CustomerModel
    {
        [Key]
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int AccountId { get; set; }
        public string Gender { get; set; }
    }
}
