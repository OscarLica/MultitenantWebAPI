namespace MultiTenantWebApi.Entities
{
    public class Organization
    {
        /// <summary>
        ///     primary key de la entidad
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Nombre de la organización
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Nombre del tenant
        /// </summary>
        public string Tenant { get; set; }
    }
}
