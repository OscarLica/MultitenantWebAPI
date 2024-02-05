using MultiTenantWebApi.Entities;

namespace MultiTenantWebApi.Domain
{
    public class RegisterAccountResponse
    {
        /// <summary>
        ///     Cuenta del usuario creada
        /// </summary>
        public Users? Account { get; set; }

        /// <summary>
        ///     Indicador de proceso exitoso
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///     Mensaje
        /// </summary>
        public string? Message { get; set; }
    }
}
