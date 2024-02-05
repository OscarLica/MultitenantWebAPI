using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using MultiTenantWebApi.Aplication;
using MultiTenantWebApi.Domain;
using MultiTenantWebApi.Entities;
using MultiTenantWebApi.Entities.Dto;

namespace MultiTenantWebApi.Infraestructura.Repositories
{
    public class UserRepository : IUserRepository
    {
        /// <summary>
        ///     Contexto de base de datos principal
        /// </summary>
        private readonly BaseDbContext _dbContext;

        /// <summary>
        ///     Constructor base inicializa dependencias
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="configuration"></param>
        public UserRepository(BaseDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        ///     Consulta listado de usuarios
        /// </summary>
        /// <returns></returns>
        public async Task<List<UsersDto>> Get()
            => await (from user in _dbContext.Users
                       join org in _dbContext.Organization on user.OrganizationId equals org.Id
                       orderby org.Id
                       select new UsersDto { Id = user.Id, Email = user.Email, Password = user.Password, OrganizationId = org.Id, Organization = org.Name }).ToListAsync();
        
        /// <summary>
        ///     Consulta para inicio de sesión
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        public async Task<Users> Login(LoginRequest loginRequest)
            => await _dbContext.Users.FirstOrDefaultAsync(us => us.Email == loginRequest.Email && us.Password == loginRequest.Password);

        /// <summary>
        ///     Registra un nuevo usuario
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<RegisterAccountResponse> Register(Users account) {

            var org = await _dbContext.Organization.AnyAsync(an => an.Id == account.OrganizationId);
            if(!org) return new RegisterAccountResponse { Message = "Organization is not configured" };

            await _dbContext.Users.AddAsync(account);
            await _dbContext.SaveChangesAsync();
            return new RegisterAccountResponse { Account = account, Success = true, Message = "Account created successfully" };
        }
        
    }
}
