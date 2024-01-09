using Econference.Models;

namespace Econference.Data
{
    public interface IHallRepository
    {
        Task AddAsync(Hall hall);
        Task UpdateAsync(Hall hall);
        Task DeleteAsync(int id);
        Task<Hall> GetAsync(int id);
        Task<List<Hall>> GetAllAsync();
    }
}
