using Microsoft.AspNetCore.Mvc;
using MultiTenantWebApi.Aplication;
using MultiTenantWebApi.Entities;

namespace MultiTenantWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        /// <summary>
        ///     Interface del repositorio de orgnización
        /// </summary>
        private readonly IOrganzationRepository _organzationRepository;

        /// <summary>
        ///     Constructor base
        /// </summary>
        /// <param name="organzationRepository"></param>
        public OrganizationController(IOrganzationRepository organzationRepository)
        {
            _organzationRepository = organzationRepository;
        }

        /// <summary>
        ///     Consulta listado de organizaciones
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var org = await _organzationRepository.Get();
            return Ok(org);
        }

        /// <summary>
        ///     Crea un nueva organización
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(Organization organization) {
            var org = await _organzationRepository.Create(organization);
            return Ok(org);
        }
    }
}
