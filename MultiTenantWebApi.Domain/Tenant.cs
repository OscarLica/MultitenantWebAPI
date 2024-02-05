namespace MultiTenantWebApi.Domain
{
    public class Tenant
    {
        /// <summary>
        ///     Prefijo del tenant
        /// </summary>
        public string Prefix { get; set; } = null!;

        /// <summary>
        ///     string de conexión
        /// </summary>
        public string? ConnectionString { get; set; }
    }
}
