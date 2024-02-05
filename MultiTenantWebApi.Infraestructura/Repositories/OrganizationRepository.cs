using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiTenantWebApi.Aplication;
using MultiTenantWebApi.Entities;

namespace MultiTenantWebApi.Infraestructura.Repositories
{
    public class OrganizationRepository : IOrganzationRepository
    {
        /// <summary>
        ///     Contexto de la base de datos principal
        /// </summary>
        private readonly BaseDbContext _dbContext;

        /// <summary>
        ///     interface de iconfiguration
        /// </summary>
        private readonly IConfiguration _configuration;


        /// <summary>
        ///     Constructor base, inicializa dependencias
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="configuration"></param>
        public OrganizationRepository(BaseDbContext dbContext, IConfiguration  configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        /// <summary>
        ///     Consulta listado de organizaciones
        /// </summary>
        /// <returns></returns>
        public async Task<List<Organization>> Get()
            => await _dbContext.Organization.ToListAsync();

        /// <summary>
        ///     Consulta si existe el tenant
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public async Task<bool> CheckTenant(string tenant)
           => await _dbContext.Organization.AnyAsync(org => org.Tenant == tenant);

        /// <summary>
        ///     Consulta el organización por tenant 
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public async Task<Organization?> GetByTenant(string tenant)
          => await _dbContext.Organization.FirstOrDefaultAsync(org => org.Tenant == tenant);
        
        /// <summary>
        ///     Crea una nueva organización
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public async Task<Organization> Create(Organization organization) {
            await _dbContext.Organization.AddAsync(organization);
            await _dbContext.SaveChangesAsync();
            await CreateDbContext(organization.Tenant);
            return organization;
        }

        /// <summary>
        ///     Crea la migración para la base de datos del tenant
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        private async Task CreateDbContext(string tenant) {
            var factory = new AppDbContextFactory(null, _configuration, tenant);
            using (var context = factory.CreateDbContext(new string[0]))
            {
                await context.Database.MigrateAsync();
            }
        }

    }
}
