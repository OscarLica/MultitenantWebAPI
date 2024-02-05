using MultiTenantWebApi.Entities;

namespace MultiTenantWebApi.Aplication
{
    public interface IOrganzationRepository
    {
        Task<List<Organization>> Get();
        Task<bool> CheckTenant(string tenant);
        Task<Organization?> GetByTenant(string tenant);
        Task<Organization> Create(Organization organization);
    }
}
