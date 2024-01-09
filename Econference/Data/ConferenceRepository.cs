using Econference.Models;

namespace Econference.Data
{
    public class ConferenceRepository : IConferenceRepository
    {
        private readonly ApplicationDbContext _context;
        public ConferenceRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(Conference conference)
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Conference>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Conference> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Conference conference)
        {
            throw new NotImplementedException();
        }
    }
}
