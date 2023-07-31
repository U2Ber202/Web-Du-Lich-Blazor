using System.ComponentModel.DataAnnotations;

namespace Website_Du_lich.Data
{
    public class AccountModel
    {
        [Key]
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
    }
}
