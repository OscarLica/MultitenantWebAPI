namespace MultiTenantWebApi.Domain
{
    public class LoginRequest
    {
        /// <summary>
        ///     Email del usuario
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Password del usuario
        /// </summary>
        public string Password { get; set; }
    }
}
