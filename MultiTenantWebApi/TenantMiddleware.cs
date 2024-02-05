using Microsoft.Extensions.Configuration;
using MultiTenantWebApi.Aplication;
using MultiTenantWebApi.Domain;
using System.Net;

namespace MultiTenantWebApi
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate next;

        private readonly IServiceScopeFactory _serviceScopeFactory;
        public TenantMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
        {
            this.next = next;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task Invoke(HttpContext context, IConfiguration configuration)
        {
            context.Items["TenantConnection"] = null;
            context.Items["Tenant"] = null;
            var excludePath = context.Request.Path.Value.Contains(UrlExcludes.Login) || context.Request.Path.Value.Contains(UrlExcludes.Organization);
            var tenant = context.Request.Path.Value.Split("/").Where(wh => !string.IsNullOrEmpty(wh)).FirstOrDefault();

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var organizationRepository = scope.ServiceProvider.GetRequiredService<IOrganzationRepository>();
                    
                if (excludePath)
                {
                    UseTenantConnection(context, configuration, tenant, "MasterDBSecurity");
                    await next.Invoke(context);
                    return;
                }

                if (!await CheckTenant(organizationRepository, tenant))
                {

                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "text/plain";

                    await context.Response.WriteAsync($"Tenant {tenant} is not configured");

                    return;
                }

                UseTenantConnection(context, configuration, tenant);
                await next.Invoke(context);
            }
            
        }

        /// <summary>
        ///     indica la conexión a usar en base al tenant
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        /// <param name="tenant"></param>
        /// <param name="tenantName"></param>
        private void UseTenantConnection(HttpContext context, IConfiguration configuration, string tenant, string tenantName = "TenantConection") {

            var connection = configuration.GetConnectionString(tenantName);

            context.Items["TenantConnection"] = string.Format(connection, tenant);
            context.Items["Tenant"] = tenant;
        }

        /// <summary>
        ///     verifica que el tenand existe
        /// </summary>
        /// <param name="organzationRepository"></param>
        /// <param name="tenant"></param>
        /// <returns></returns>
        private async Task<bool> CheckTenant(IOrganzationRepository organzationRepository,string tenant)
        {
            if (string.IsNullOrEmpty(tenant)) return false;

            return await organzationRepository.CheckTenant(tenant);
        }
    }

    /// <summary>
    ///     Clase para injectar el midleware
    /// </summary>
    public static class TenantMiddlewareExtension
    {
        public static IApplicationBuilder UseTenant(this IApplicationBuilder app)
        {
            app.UseMiddleware<TenantMiddleware>();
            return app;
        }
    }
}
