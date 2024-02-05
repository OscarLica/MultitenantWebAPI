using Microsoft.EntityFrameworkCore;
using MultiTenantWebApi.Aplication;
using MultiTenantWebApi.Entities;

namespace MultiTenantWebApi.Infraestructura.Repositories
{
    public class ProductRepository : IProductRepository
    {
        /// <summary>
        ///     Contexto de base de datos para la entidad producto
        /// </summary>
        private readonly AppDbContext _dbContext;

        /// <summary>
        ///     Constructor base
        /// </summary>
        /// <param name="dbContext"></param>
        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;            
        }

        /// <summary>
        ///     Consulta listado de productos
        /// </summary>
        /// <returns></returns>
        public async Task<List<Products>> Get()
            => await _dbContext.Products.ToListAsync();

        /// <summary>
        ///     Consulta producto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Products> Get(int id)
            => await _dbContext.Products.FindAsync(id);

        /// <summary>
        ///     Crea un nuevo producto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Products> Create(Products product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}
