namespace Clean.DataAccess.Core.Context
{
    public interface IUserContext
    {
        long? GetUserId();
        long? GetTenantId();
    }
}
