using Econference.Models;

namespace Econference.Data
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(int id);
        Task<Booking> GetAsync(int id);
        Task<List<Booking>> GetAllAsync();
    }
}
