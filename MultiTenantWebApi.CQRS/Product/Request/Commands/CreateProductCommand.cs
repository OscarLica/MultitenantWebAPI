using MediatR;
using MultiTenantWebApi.Entities;

namespace MultiTenantWebApi.CQRS.Product.Request.Commands
{
    /// <summary>
    ///     comando del producto
    /// </summary>
    public class CreateProductCommand : IRequest<Products>
    {
        /// <summary>
        ///     Entidad producto
        /// </summary>
        public Products Product { get; set; }
    }
}
    