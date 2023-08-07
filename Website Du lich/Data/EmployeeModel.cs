using System.ComponentModel.DataAnnotations;

namespace Website_Du_lich.Data
{
    public class EmployeeModel
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public int AccountId { get; set; }
    }
}
