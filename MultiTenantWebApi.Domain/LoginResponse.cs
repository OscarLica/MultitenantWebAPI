namespace MultiTenantWebApi.Domain
{
    public class LoginResponse
    {
        /// <summary>
        ///     Token generado
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///     Indicador de proceso exitoso
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///     Mensaje de validación
        /// </summary>
        public string Message { get; set; }
    }
}
