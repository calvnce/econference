using Econference.Models;

namespace Econference.Data
{
    public interface IConferenceRepository
    {
        Task AddAsync(Conference conference);
        Task UpdateAsync(Conference conference);
        Task DeleteAsync(int id);
        Task<Conference> GetAsync(int id);
        Task<List<Conference>> GetAllAsync();
    }
}
