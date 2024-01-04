using Microsoft.AspNetCore.Identity;

namespace Econference.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string UserRole {  get; set; }
        public string FullName { get; set; }
    }
}
