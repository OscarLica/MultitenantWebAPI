using Microsoft.AspNetCore.Http;
using MultiTenantApi.Aplication;
using MultiTenantWebApi.Domain;

namespace MultiTenantWebApi.Infraestructura
{
    public class TenantResolver : ITenantResolver
    {
        /// <summary>
        ///     interface del IHttpContextAccessor
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        ///     constructor base
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        public TenantResolver(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        ///     Consulta el tenant actual
        /// </summary>
        /// <returns></returns>
        public Tenant GetCurrentTenant()
        {
            var tenantConnection = _httpContextAccessor.HttpContext.Items["TenantConnection"];
            var tenant = _httpContextAccessor.HttpContext.Items["Tenant"];
            return new Tenant { ConnectionString = tenantConnection.ToString(), Prefix = tenant.ToString() };
        }
    }
}