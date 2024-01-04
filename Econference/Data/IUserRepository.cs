using Econference.Models;

namespace Econference.Data
{
    public interface IUserRepository
    {
        Task RegisterAsync(ApplicationUser applicationUser);
        Task<ApplicationUser> GetUserAsync(String username);
    }
}
