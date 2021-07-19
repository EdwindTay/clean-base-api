using System.Threading.Tasks;
using Clean.Application.Services.Users.Interfaces;
using Clean.Core.Dto.Common;
using Clean.Core.Dto.Users.User;
using Clean.Web.Core.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Web.Controllers.Users
{
    [Route("api/Users/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("me")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetMe()
        {
            var userExternalId = GetCurrentUserExternalId();
            var user = await _userService.GetUserByExternalUserIdAsync(externalUserId: userExternalId);

            if (user == null)
            {
                user = await CreateUserUsingJWTTokenAsync();
            }

            return Ok(user);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetUser(long id)
        {
            var user = await _userService.GetUserAsync(userId: id);

            return Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedResultDto<UserDto>))]
        public async Task<ActionResult> GetUsers([FromQuery] PaginationInputDto pagination, [FromQuery] GetUserDto input)
        {
            var users = await _userService.GetUsersAsync(pagination: pagination, input: input);

            return Ok(users);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Post(CreateUserDto input)
        {
            await _userService.CreateUserAsync(input: input);

            return Ok();
        }

        /// <summary>
        /// Create user using jwt token
        /// </summary>
        /// <returns></returns>
        private async Task<UserDto> CreateUserUsingJWTTokenAsync()
        {
            var externalUserId = GetCurrentUserExternalId();
            var email = GetCurrentUserEmail();

            var newUser = new CreateUserDto()
            {
                ExternalUserId = externalUserId,
                Email = email,
                UserName = email,
                DisplayName = email
            };

            return await _userService.CreateUserAsync(input: newUser);
        }
    }
}
