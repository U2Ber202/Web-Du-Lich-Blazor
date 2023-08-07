using System.ComponentModel.DataAnnotations;

namespace Website_Du_lich.Data
{
    public class AccountModel
    {
        [Key]
        public int AccountId { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public string AccountStatus { get; set; }

    }
}
