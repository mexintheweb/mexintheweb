using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace mexintheweb.Models.Identity
{
    public class AuthIdentityDbContext : IdentityDbContext
    {
        public AuthIdentityDbContext(DbContextOptions<AuthIdentityDbContext> options) : base(options)
        {
            
        }
    }
}
