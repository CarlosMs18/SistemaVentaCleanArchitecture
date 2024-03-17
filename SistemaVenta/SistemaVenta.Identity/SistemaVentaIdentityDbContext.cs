using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaVenta.Identity.Models;

namespace SistemaVenta.Identity
{
    public class SistemaVentaIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public SistemaVentaIdentityDbContext(DbContextOptions<SistemaVentaIdentityDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);  
        }
    }
}
