using System.Linq;
using System.Threading.Tasks;
using Clean.Application.Services.Users.Interfaces;
using Clean.Core.Dto.Common;
using Clean.Core.Dto.Users.User;
using Clean.Core.Exceptions;
using Clean.Core.Resources;
using Clean.DataAccess.Core;
using Clean.DataAccess.Entities.Users;
using Clean.DataAccess.EntityFramework;
using Clean.DataAccess.Repositories.Users.Interfaces;
using Clean.DataAccess.UnitOfWork.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Clean.Core.Enums;

namespace Clean.Application.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork<CleanDbContext> _unitOfWork;

        private readonly IUserRepository _userRepository;

        public UserService(
            IUnitOfWork<CleanDbContext> unitOfWork,
            IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        ///<inheritdoc/>
        public async Task<UserDto> GetUserAsync(long userId)
        {
            var query = GetUserQuery();

            var user = await query
                .FirstOrDefaultAsync(a => a.UserId == userId);

            return user;
        }

        ///<inheritdoc/>
        public async Task<PaginatedResultDto<UserDto>> GetUsersAsync(PaginationInputDto pagination, GetUserDto input)
        {
            var query = GetUserQuery(input);

            var users = await query
                .Skip(pagination.StartFrom)
                .Take(pagination.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PaginatedResultDto<UserDto>()
            {
                Items = users,
                TotalCount = count
            };
        }

        ///<inheritdoc/>
        public async Task<UserDto> GetUserByExternalUserIdAsync(string externalUserId)
        {
            var query = GetUserQuery();

            var user = await query
                .FirstOrDefaultAsync(a => a.ExternalUserId == externalUserId);

            return user;
        }

        ///<inheritdoc/>
        public async Task<UserDto> CreateUserAsync(CreateUserDto input)
        {
            #region validation

            var isUserExist = await _userRepository.Get()
                .AnyAsync(a => a.ExternalUserId == input.ExternalUserId);

            if (isUserExist)
            {
                throw new FriendlyException(ErrorMessageResource.UserAlreadyExistsError);
            }

            #endregion

            var user = new User
            {
                ExternalUserId = input.ExternalUserId,
                UserName = input.UserName,
                Email = input.Email,
                DisplayName = input.DisplayName,
                //account creation createdbyid and updatedbyid is hardcoded to System User
                CreatedById = (long)UserId.SystemUser,
                UpdatedById = (long)UserId.SystemUser
            };

            _userRepository.Add(user);

            await _unitOfWork.SaveChangesAsync();

            var newUser = new UserDto
            {
                UserId = user.UserId,
                ExternalUserId = user.ExternalUserId,
                UserName = user.UserName,
                Email = user.Email,
                DisplayName = user.DisplayName
            };

            return newUser;
        }

        /// <summary>
        /// General Get User query
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private IQueryable<UserDto> GetUserQuery(GetUserDto input = null)
        {
            var query = _userRepository.Get()
                .AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(input?.ExternalUserId), a => a.ExternalUserId == input.ExternalUserId)
                .WhereIf(!string.IsNullOrWhiteSpace(input?.UserName), a => a.UserName == input.UserName)
                .WhereIf(!string.IsNullOrWhiteSpace(input?.Email), a => a.Email == input.Email)
                .WhereIf(!string.IsNullOrWhiteSpace(input?.DisplayName), a => a.DisplayName == input.DisplayName)
                .Select(a => new UserDto
                {
                    UserId = a.UserId,
                    ExternalUserId = a.ExternalUserId,
                    UserName = a.UserName,
                    Email = a.Email,
                    DisplayName = a.DisplayName,
                    TenantId = a.TenantId
                });

            return query;
        }
    }
}
