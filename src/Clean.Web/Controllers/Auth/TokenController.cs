using System.Threading.Tasks;
using Clean.Core.Dto.Auth;
using Clean.Application.HttpClients.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Clean.Web.Controllers.Auth
{
    [Route("api/Auth/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IFirebaseAuthHttpClient _firebaseAuthHttpClient;

        public TokenController(IWebHostEnvironment environment, IFirebaseAuthHttpClient firebaseAuthHttpClient)
        {
            _environment = environment;
            _firebaseAuthHttpClient = firebaseAuthHttpClient;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get([FromForm] string username, [FromForm] string password)
        {
            if (_environment.IsProduction())
            {
                return NotFound();
            }

            var token = await _firebaseAuthHttpClient.LoginAsync(username, password);

            return Ok(token);
        }
    }
}
