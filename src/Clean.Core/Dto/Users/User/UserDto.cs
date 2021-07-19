namespace Clean.Core.Dto.Users.User
{
    public class UserDto
    {
        public long UserId { get; set; }
        public string ExternalUserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public long? TenantId { get; set; }
    }
}
