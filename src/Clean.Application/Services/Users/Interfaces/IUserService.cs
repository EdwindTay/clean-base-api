using System.Threading.Tasks;
using Clean.Core.Dto.Common;
using Clean.Core.Dto.Users.User;

namespace Clean.Application.Services.Users.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserDto> GetUserAsync(long userId);

        /// <summary>
        /// Get users
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PaginatedResultDto<UserDto>> GetUsersAsync(PaginationInputDto pagination, GetUserDto input);

        /// <summary>
        /// Get user by external user id
        /// </summary>
        /// <param name="externalUserId"></param>
        /// <returns></returns>
        Task<UserDto> GetUserByExternalUserIdAsync(string externalUserId);

        /// <summary>
        /// Create user
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UserDto> CreateUserAsync(CreateUserDto input);
    }
}
