using Econference.Models;

namespace Econference.Data
{
    public interface ICateringProviderRepository
    {
        Task AddAsync(CateringProvider cateringProvider);
        Task UpdateAsync(CateringProvider cateringProvider);
        Task DeleteAsync(int id);
        Task<CateringProvider> GetAsync(int id);
        Task<List<CateringProvider>> GetAllAsync();
    }
}
