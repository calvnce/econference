
using Econference.Models;
using Microsoft.EntityFrameworkCore;

namespace Econference.Data
{
    public class CateringProviderRepository : ICateringProviderRepository
    {
        private readonly ApplicationDbContext _context;
        public CateringProviderRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(CateringProvider cateringProvider)
        {
            try
            {
                await _context.Caterings.AddAsync( cateringProvider );
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
                var cater = await _context.Caterings.FindAsync(id);
                if ( cater != null )
                {
                    _context.Caterings.Remove( cater );
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<List<CateringProvider>> GetAllAsync()
        {
            try
            {
                return await _context.Caterings.ToListAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<CateringProvider> GetAsync(int id)
        {
            try
            {
                return await _context.Caterings.FindAsync(id);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task UpdateAsync(CateringProvider cateringProvider)
        {
            try
            {
                _context.Caterings.Update(cateringProvider);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
