using Econference.Models;
using Microsoft.EntityFrameworkCore;

namespace Econference.Data
{
    public class HallRepository : IHallRepository
    {
        private readonly ApplicationDbContext _context;
        public HallRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(Hall hall)
        {
            try
            {
                await _context.Halls.AddAsync(hall);
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
                var hall = await _context.Halls.FindAsync(id);
                if (hall != null)
                {
                    _context.Halls.Remove(hall);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<List<Hall>> GetAllAsync()
        {
            try
            {
                return await _context.Halls.ToListAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<Hall> GetAsync(int id)
        {
            try
            {
                return await _context.Halls.FindAsync(id);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task UpdateAsync(Hall hall)
        {
            try
            {
                 _context.Halls.Update(hall);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
