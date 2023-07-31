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
        //Get All Tours
        public async Task<List<TourModel>> GetAllTours()
        {
            return await _dbContext.Tours.ToListAsync();
        }
        //Add New Tour Record
        public async Task<bool> AddNewTour(TourModel tour)
        {
            await _dbContext.Tours.AddAsync(tour);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        //Get Tour Record By Id
        public async Task<TourModel> GetTourById(int id)
        {
            TourModel tour = await _dbContext.Tours.FirstOrDefaultAsync(x => x.TourId == id);
            return tour;
        }
        //Update Tour Record
        public async Task<bool> UpdateTour(TourModel tour)
        {
            _dbContext.Tours.Update(tour);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        //Delete Tour Record
        public async Task<bool> DeleteTour(TourModel tour)
        {
            _dbContext.Tours.Remove(tour);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
