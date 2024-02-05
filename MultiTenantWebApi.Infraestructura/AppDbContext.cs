using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MultiTenantApi.Aplication;
using MultiTenantWebApi.Domain;
using MultiTenantWebApi.Entities;

namespace MultiTenantWebApi.Infraestructura
{
    public class AppDbContext : DbContext
    {
        private readonly Tenant _tenant;
        private readonly ITenantResolver _tenantResolver;
        public AppDbContext(DbContextOptions<AppDbContext> options, ITenantResolver tenantResolver)
            : base(options)
        {
            _tenantResolver = tenantResolver;
        }
        public DbSet<Products> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               
                    var connection = _tenantResolver.GetCurrentTenant();

                    if (connection.ConnectionString is { } connectionString)
                            optionsBuilder.UseSqlServer(connection.ConnectionString);

            }
            base.OnConfiguring(optionsBuilder);
        }
    }
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        private readonly ITenantResolver _tenantResolver;
        public AppDbContextFactory(ITenantResolver tenantResolver, IConfiguration configuration, string tenant = "baseTenant")
        {
            _tenantResolver = tenantResolver;
            if (_tenantResolver is null)
                _tenantResolver = new DefaultTenantResolver(configuration, tenant);
        }
        public AppDbContext CreateDbContext(string[] args)
        {
            // Aquí deberías obtener el tenant dinámicamente, por ejemplo, desde un parámetro o archivo de configuración.
            var tenant = _tenantResolver.GetCurrentTenant(); // Aquí reemplaza con la lógica para obtener el tenant dinámicamente

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(tenant.ConnectionString);

            return new AppDbContext(optionsBuilder.Options, null);
        }

    }
    public class DefaultTenantResolver : ITenantResolver
    {
        private readonly string _tenant;
        private readonly IConfiguration _configuration;
        public DefaultTenantResolver(IConfiguration configuration, string tenant)
        {
            _tenant = tenant;
            _configuration = configuration;
        }
        public Tenant GetCurrentTenant()
        {
            var connectionString = _configuration.GetConnectionString("TenantConection");
            // Devuelve un tenant de ejemplo durante el diseño de tiempo (migraciones)
            return new Tenant { ConnectionString = string.Format(connectionString, _tenant) };
        }
    }
}
