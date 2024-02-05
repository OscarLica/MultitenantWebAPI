using MediatR;
using MultiTenantWebApi.Entities;

namespace MultiTenantWebApi.CQRS.Product.Request.Queries
{

    /// <summary>
    ///     request del procutos
    /// </summary>
    public class GetProductRequest : IRequest<List<Products>>
    {
    }
}
