using Econference.Models;
using Microsoft.EntityFrameworkCore;

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
                _context.Conferences.Add(conference);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
               var conference =  await _context.Conferences.FindAsync(id);
                if (conference != null)
                {
                    _context.Conferences.Remove(conference);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<List<Conference>> GetAllAsync()
        {
            try
            {
                return await _context.Conferences.ToListAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<Conference> GetAsync(int id)
        {
            try
            {
                return await (_context.Conferences.FindAsync(id));
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task UpdateAsync(Conference conference)
        {
            try
            {
                _context.Conferences.Update(conference);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
