namespace SK.Inventory.Application.Interfaces.Common
{
    public interface ICurrentUserService
    {
        Task<Guid> GetUserIdAsync();
    }
}
