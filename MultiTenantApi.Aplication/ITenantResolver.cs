using MultiTenantWebApi.Domain;

namespace MultiTenantApi.Aplication
{
    public interface ITenantResolver
    {
        Tenant GetCurrentTenant();
    }
}
