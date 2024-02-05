using MultiTenantWebApi.Entities;

namespace MultiTenantWebApi.Aplication
{
    public interface IProductRepository
    {
        Task<List<Products>> Get();
        Task<Products> Get(int id);
        Task<Products> Create(Products product);
    }
}
