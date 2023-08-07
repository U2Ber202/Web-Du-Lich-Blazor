using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Website_Du_lich.Data;


namespace Website_Du_lich.Context
{
    public class DataService
    {
        private readonly ApplicationDBContext _dbContext;
        public DataService(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        //Get All Tours/Hotel/CarRent/Shuttle
        public async Task<List<TourModel>> GetAllTours()
        {
            return await _dbContext.Tours.ToListAsync();
        }

        public async Task<List<HotelModel>> GetAllHotel()
        {
            return await _dbContext.Hotel.ToListAsync();
        }
        public async Task<List<CarRentModel>> GetAllCarRent()
        {
            return await _dbContext.CarRent.ToListAsync();
        }
        public async Task<List<ShuttleBookingModel>> GetAllShuttles()
        {
            return await _dbContext.Shuttles.ToListAsync();
        }
        //Add New Tours/Hotel/CarRent/Shuttle Record
        public async Task<bool> AddNewTour(TourModel tour)
        {
            await _dbContext.Tours.AddAsync(tour);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddNewHotel(HotelModel hotel)
        {
            await _dbContext.Hotel.AddAsync(hotel);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddNewCarRent(CarRentModel carRent)
        {
            await _dbContext.CarRent.AddAsync(carRent);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddNewShuttle(ShuttleBookingModel shuttle)
        {
            await _dbContext.Shuttles.AddAsync(shuttle);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        //Get Tours/Hotel/CarRent/Shuttle Record By Id
        public async Task<TourModel> GetTourById(int id)
        {
            TourModel tour = await _dbContext.Tours.FirstOrDefaultAsync(x => x.TourId == id);
            return tour;
        }
        public async Task<HotelModel> GetHotelById(int id)
        {
            HotelModel hotel = await _dbContext.Hotel.FirstOrDefaultAsync(x => x.HotelId == id);
            return hotel;
        }
        public async Task<CarRentModel> GetCarRentById(int id)
        {
            CarRentModel carRent = await _dbContext.CarRent.FirstOrDefaultAsync(x => x.CarRentId == id);
            return carRent;
        }
        public async Task<ShuttleBookingModel> GetShuttleById(int id)
        {
            ShuttleBookingModel shuttle = await _dbContext.Shuttles.FirstOrDefaultAsync(x => x.SBId == id);
            return shuttle;
        }
        //Update Tours/Hotel/CarRent/Shuttle Record
        public async Task<bool> UpdateTour(TourModel tour)
        {
            _dbContext.Tours.Update(tour);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateHotel(HotelModel hotel)
        {
            _dbContext.Hotel.Update(hotel);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCarRent(CarRentModel carRent)
        {
            _dbContext.CarRent.Update(carRent);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateShuttle(ShuttleBookingModel shuttle)
        {
            _dbContext.Shuttles.Update(shuttle);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        //Delete Tours/Hotel/CarRent/Shuttle Record
        public async Task<bool> DeleteTour(TourModel tour)
        {
            _dbContext.Tours.Remove(tour);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteHotel(HotelModel hotel)
        {
            _dbContext.Hotel.Remove(hotel);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteCarRent(CarRentModel carRent)
        {
            _dbContext.CarRent.Remove(carRent);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteShuttle(ShuttleBookingModel shuttle)
        {
            _dbContext.Shuttles.Remove(shuttle);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        //Get All History Orders
        public async Task<List<dynamic>> GetOrderDetailsAsync()
        {
            using (SqlConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                string query = @"
            SELECT
                o.OrderId, o.TotalAmount, o.Status,
                c.CustomerId, c.FullName,
                cr.CarModel, cr.CarType,
                s.PickupLocation, s.DropOffLocation,
                h.HotelName, h.Address
            FROM Orders o
            INNER JOIN Customers c ON o.CustomerId = c.CustomerId
            LEFT JOIN CarRent cr ON o.CarId = cr.CarId
            LEFT JOIN Shuttles s ON o.SBId = s.SBId
            LEFT JOIN Hotel h ON o.HotelId = h.HotelId";

                await connection.OpenAsync();

                // Sử dụng Dapper để thực hiện truy vấn và ánh xạ vào danh sách đối tượng tạm thời
                List<dynamic> results = (await connection.QueryAsync(query)).ToList();

                return results;
            }
        }
        public async Task<string> GetFullNameByUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {
                string query = @"
            SELECT c.FullName
            FROM Customers c
            INNER JOIN Accounts a ON c.CustomerId = a.CustomerId
            WHERE a.Username = @Username";

                await connection.OpenAsync();

                string fullName = await connection.ExecuteScalarAsync<string>(query, new { Username = username });

                return fullName;
            }
        }
    }
}
