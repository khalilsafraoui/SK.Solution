namespace SK.Visit.Application.Interfaces.Common
{
    public interface ICurrentUserService
    {
        Task<Guid> GetUserIdAsync();
    }
}
