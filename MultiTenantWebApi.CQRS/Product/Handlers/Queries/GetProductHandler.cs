using MediatR;
using MultiTenantWebApi.Aplication;
using MultiTenantWebApi.CQRS.Product.Request.Queries;
using MultiTenantWebApi.Entities;

namespace MultiTenantWebApi.CQRS.Product.Handlers.Queries
{
    public class GetProductHandler : IRequestHandler<GetProductRequest, List<Products>>
    {
        /// <summary>
        ///     interface del repositorio de productos
        /// </summary>
        private readonly IProductRepository _productRepository;

        /// <summary>
        ///     Constructor base para inicializar dependencias
        /// </summary>
        /// <param name="productRepository"></param>
        public GetProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        /// <summary>
        ///     Manejador del comando
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<Products>> Handle(GetProductRequest request, CancellationToken cancellationToken)
            => await _productRepository.Get();

    }
}
