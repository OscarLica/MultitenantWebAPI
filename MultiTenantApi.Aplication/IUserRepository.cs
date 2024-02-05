using MultiTenantWebApi.Domain;
using MultiTenantWebApi.Entities;
using MultiTenantWebApi.Entities.Dto;

namespace MultiTenantWebApi.Aplication
{
    public interface IUserRepository
    {
        Task<Users> Login(LoginRequest loginRequest);
        Task<RegisterAccountResponse> Register(Users account);
        Task<List<UsersDto>> Get();
    }
}
