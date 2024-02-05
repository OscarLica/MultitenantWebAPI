using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MultiTenantWebApi.Infraestructura
{
    public static class InfraestructuraServicesRegistration
    {
        /// <summary>
        ///     Ejecuta migración automatica para la base de datos master
        /// </summary>
        /// <param name="app"></param>
        public static void InitializeMigrations(this IApplicationBuilder app)
        {            
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<BaseDbContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
