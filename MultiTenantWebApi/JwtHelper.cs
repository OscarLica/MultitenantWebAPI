using Microsoft.IdentityModel.Tokens;
using MultiTenantWebApi.Domain;
using MultiTenantWebApi.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MultiTenantWebApi
{
    public class JwtHelper
    {
        public const string ISSUER = "http://localhost:7153/";
        public const string AUDIENCE = "http://localhost:7153/";
        public const string SECURITY_KEY = "tokenSecurityKeyMultiTenant@#%123.";

        /// <summary>
        ///     Parametros para el token
        /// </summary>
        /// <returns></returns>
        public static TokenValidationParameters GetTokenParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = ISSUER,
                ValidAudience = AUDIENCE,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECURITY_KEY))
            };
        }

        /// <summary>
        ///     Contruye el token del usuario
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        public static string GenerateToken(Users users)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECURITY_KEY));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: ISSUER,
                audience: AUDIENCE,
                claims: new List<Claim>() { new("https://schemas.microsoft.com/identity/claims/tenantid", users.OrganizationId.ToString()?? string.Empty), new ("https://schemas.microsoft.com/identity/claims/email", users.Email) },
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
