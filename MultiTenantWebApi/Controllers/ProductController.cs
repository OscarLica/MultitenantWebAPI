using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantWebApi.CQRS.Product.Request.Commands;
using MultiTenantWebApi.CQRS.Product.Request.Queries;
using MultiTenantWebApi.Entities;

namespace MultiTenantWebApi.Controllers
{
    [Route("{tenant}/api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        /// <summary>
        ///     Interface de Mediator para comunicarse con los objetos
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        ///     Constructor base
        /// </summary>
        /// <param name="mediator"></param>
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Consulta listado de productos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
            => Ok(await _mediator.Send(new GetProductRequest()));

        /// <summary>
        ///     Consulta producto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductId(int id)
            => Ok(await _mediator.Send(new GetProductIdRequest { Id = id}));

        /// <summary>
        ///     Crea un nuevo proyecto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> Create(Products product)
            => Ok(await _mediator.Send(new CreateProductCommand { Product = product}));
    }
}
