using MediatR;
using MultiTenantWebApi.Aplication;
using MultiTenantWebApi.CQRS.Product.Request.Commands;
using MultiTenantWebApi.Entities;

namespace MultiTenantWebApi.CQRS.Product.Handlers.Commands
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Products>
    {
        /// <summary>
        ///     interface del repositorio de productos
        /// </summary>
        private readonly IProductRepository _productRepository;

        /// <summary>
        ///     Constructor base para inicializar dependencias
        /// </summary>
        /// <param name="productRepository"></param>
        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        ///     Manejador del comando
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Products> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.Create(request.Product);
            return result;
        }
    }
}
