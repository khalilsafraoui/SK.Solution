namespace SK.CRM.Application.Interfaces.Common
{
    public interface ICurrentUserService
    {
        Task<Guid> GetUserIdAsync();
    }
}
