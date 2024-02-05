namespace MultiTenantWebApi.Entities.Dto
{
    public class UsersDto
    {
        /// <summary>
        ///     Primahry key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Email del usuario
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Contraseña
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///  id de la entidad <see cref="Organization"/>
        /// </summary>
        public int OrganizationId { get; set; }

        /// <summary>
        ///     Nombre de la organzación
        /// </summary>
        public string Organization { get; set; }
    }
}
