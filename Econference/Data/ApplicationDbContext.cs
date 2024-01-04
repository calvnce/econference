using Econference.Models;
using Microsoft.EntityFrameworkCore;

namespace Econference.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            :base(dbContextOptions) 
        { }

        public DbSet<ApplicationUser> Users { get; set; }
    }
}
