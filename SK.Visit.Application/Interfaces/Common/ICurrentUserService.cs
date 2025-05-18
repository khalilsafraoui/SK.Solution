namespace SK.Visit.Application.Interfaces.Common
{
    public interface ICurrentUserService
    {
       // public Guid UserId { get; }

        Task<Guid> GetUserIdAsync();
    }
}
