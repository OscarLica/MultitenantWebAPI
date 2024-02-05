using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantWebApi.Aplication;
using MultiTenantWebApi.Domain;
using MultiTenantWebApi.Entities;

namespace MultiTenantWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        ///     Interface de usaurio
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        ///     Constructor base
        /// </summary>
        /// <param name="userRepository"></param>
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        ///     Endpoint de consulta de usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet(nameof(Users))]
        public async Task<IActionResult> Users()
        {
            var users = await _userRepository.Get();
            return Ok(users);
        }

        /// <summary>
        ///     Endpoint para generar token
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginRequest loginRequest) {

            var user = await _userRepository.Login(loginRequest);
            if (user is null) return Ok(new LoginResponse { Message = "Email or Passwors incorrect."});

            return Ok(new LoginResponse { Token = JwtHelper.GenerateToken(user), Success = true, Message = "Success" } );
        }

        /// <summary>
        ///     Endpoint para registrar un nuevo usuario
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(Users account)
        {
            var user = await _userRepository.Register(account);
            return Ok(new { success = true, user});
        }
    }
}
