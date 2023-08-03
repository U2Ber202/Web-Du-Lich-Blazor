using Microsoft.EntityFrameworkCore;
using Website_Du_lich.Data;

namespace Website_Du_lich.Context
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        
        }
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<OrdersModel> Orders { get; set; }
        public DbSet<TourModel> Tours { get; set; }
        public DbSet<PaymentModel> Payments { get; set; }
        public DbSet<HotelModel> Hotel { get; set; }
        public DbSet<ShuttleBookingModel> Shuttles { get; set; }
    }
}
