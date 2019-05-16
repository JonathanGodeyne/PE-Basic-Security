using Microsoft.EntityFrameworkCore;
using Hybrid_Crypt_IRC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace Hybrid_Crypt_IRC.Data
{
    public class CryptChat_IdentityDbContext : IdentityDbContext<ChatCryptUser>
    {
        public CryptChat_IdentityDbContext(DbContextOptions<CryptChat_IdentityDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}