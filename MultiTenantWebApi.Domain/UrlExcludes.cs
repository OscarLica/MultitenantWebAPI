namespace MultiTenantWebApi.Domain
{
    /// <summary>
    ///     Clase para excluir url que no requieren tenant
    /// </summary>
    public class UrlExcludes
    {
        /// <summary>
        ///     url del login
        /// </summary>
        public const string Login = "/api/Account";

        /// <summary>
        ///     url de la organización
        /// </summary>
        public const string Organization = "/api/Organization";
    }
}
