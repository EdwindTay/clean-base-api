using System.Threading.Tasks;
using Clean.Application.Services.Users.Interfaces;
using Clean.Core.Dto.Users.Tenant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.Controllers.Users
{
    [Route("api/Users/[controller]")]
    [ApiController]
    [Authorize]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TenantDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(long id)
        {
            var tenant = await _tenantService.GetTenantAsync(tenantId: id);

            return Ok(tenant);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Post(CreateTenantDto input)
        {
            await _tenantService.CreateTenantAsync(input: input);

            return Ok();
        }
    }
}
