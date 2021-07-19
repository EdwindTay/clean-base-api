namespace Clean.Core.Dto.Users.User
{
    public class GetUserDto
    {
        public string ExternalUserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}
