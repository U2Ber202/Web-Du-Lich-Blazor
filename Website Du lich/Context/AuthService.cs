using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Website_Du_lich.Data;

namespace Website_Du_lich.Context
{
    public class AuthService
    {
        private readonly ApplicationDBContext _dbContext;

        public AuthService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        private async Task<bool> CheckUsernameExists(string username)
        {
            using (SqlConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM Accounts WHERE Username = @Username";
                var parameters = new { Username = username };
                int count = await connection.ExecuteScalarAsync<int>(query, parameters);
                return count > 0;
            }
        }

        private async Task<bool> CheckEmailExists(string email)
        {
            using (SqlConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM Customers WHERE Email = @Email";
                var parameters = new { Email = email };
                int count = await connection.ExecuteScalarAsync<int>(query, parameters);
                return count > 0;
            }
        }

        private async Task<bool> CheckPhoneExists(string phone)
        {
            using (SqlConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM Customers WHERE Phone = @Phone";
                var parameters = new { Phone = phone };
                int count = await connection.ExecuteScalarAsync<int>(query, parameters);
                return count > 0;
            }
        }
        public async Task<bool> CheckRegister(string username, string email, string phone)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone))
            {
                return false;
            }

            bool usernameExists = await CheckUsernameExists(username);
            bool emailExists = await CheckEmailExists(email);
            bool phoneExists = await CheckPhoneExists(phone);

            if (usernameExists || emailExists || phoneExists)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> CheckUserCredentials(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            using (SqlConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                string query = "SELECT * FROM Accounts WHERE Username = @Username AND Password = @Password";
                var parameters = new { Username = username, Password = password };
                var account = await connection.QueryFirstOrDefaultAsync<AccountModel>(query, parameters);

                if (account != null)
                {
                    // Thông tin đăng nhập đúng
                    return true;
                }
                else
                {
                    // Thông tin đăng nhập sai hoặc tài khoản không tồn tại
                    return false;
                }
            }
        }
        public async Task<bool> RegisterAccount(string username, string password, string fullName, string email, string phone, string address, DateTime dateOfBirth, string gender)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) ||
                string.IsNullOrWhiteSpace(address) ||
                dateOfBirth == default(DateTime) ||
                string.IsNullOrWhiteSpace(gender))
            {
                return false;
            }

            using (SqlConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                await connection.OpenAsync();

                int newAccountId;

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Thêm tài khoản mới vào bảng Accounts
                        string insertQueryAccounts = "INSERT INTO Accounts (Username, Password, RoleID, AccountStatus) " +
                                                     "OUTPUT INSERTED.AccountId " +
                                                     "VALUES (@Username, @Password, 3, 'NOT CONFIRM')";
                        var parametersAccounts = new
                        {
                            Username = username,
                            Password = password
                        };
                        newAccountId = await connection.ExecuteScalarAsync<int>(insertQueryAccounts, parametersAccounts, transaction);

                        // Thêm thông tin khách hàng mới vào bảng Customers và gán AccountId
                        string insertQueryCustomers = "INSERT INTO Customers (FullName, Email, Phone, Address, DateOfBirth, Gender, AccountId, Balance) " +
                                                      "VALUES (@FullName, @Email, @Phone, @Address, @DateOfBirth, @Gender, @AccountId, 0.00)";
                        var parametersCustomers = new
                        {
                            FullName = fullName,
                            Email = email,
                            Phone = phone,
                            Address = address,
                            DateOfBirth = dateOfBirth,
                            Gender = gender,
                            AccountId = newAccountId
                        };
                        await connection.ExecuteAsync(insertQueryCustomers, parametersCustomers, transaction);

                        // Lấy CustomerId từ bảng Customers
                        string getCustomerIdQuery = "SELECT CustomerId FROM Customers WHERE AccountId = @AccountId";
                        var parametersGetCustomerId = new
                        {
                            AccountId = newAccountId
                        };
                        int newCustomerId = await connection.ExecuteScalarAsync<int>(getCustomerIdQuery, parametersGetCustomerId, transaction);

                        // Cập nhật CustomerId trong bảng Accounts dựa trên CustomerId từ bảng Customers
                        string updateQueryAccounts = "UPDATE Accounts SET CustomerId = @CustomerId WHERE AccountId = @AccountId";
                        var parametersUpdateAccounts = new
                        {
                            CustomerId = newCustomerId,
                            AccountId = newAccountId
                        };
                        await connection.ExecuteAsync(updateQueryAccounts, parametersUpdateAccounts, transaction);

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return true;
        }

    }
}
