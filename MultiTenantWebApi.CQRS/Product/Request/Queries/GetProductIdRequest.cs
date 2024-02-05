using MediatR;
using MultiTenantWebApi.Entities;

namespace MultiTenantWebApi.CQRS.Product.Request.Queries
{
    /// <summary>
    ///     request del producto por id
    /// </summary>
    public class GetProductIdRequest : IRequest<Products>
    {
        /// <summary>
        ///     id de consulta para el producto
        /// </summary>
        public int Id { get; set; }
    }
}
