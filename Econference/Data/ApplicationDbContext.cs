using Econference.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Econference.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            :base(dbContextOptions) 
        { }

        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<CateringProvider> Caterings { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
