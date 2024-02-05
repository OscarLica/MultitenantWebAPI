using Microsoft.EntityFrameworkCore;
using MultiTenantWebApi.Entities;

namespace MultiTenantWebApi.Infraestructura
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions<BaseDbContext> options)
           : base(options)
        {

        }

        public DbSet<Organization> Organization { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
