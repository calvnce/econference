using Econference.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Econference.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<ApplicationUser> GetUserAsync(string username)
        {
            return await _context.Users.FindAsync(username);
        }

        public async Task RegisterAsync(ApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
