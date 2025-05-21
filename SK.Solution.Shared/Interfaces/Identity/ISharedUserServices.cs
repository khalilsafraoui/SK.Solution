using SK.Solution.Shared.Model.Identity;

namespace SK.Solution.Shared.Interfaces.Identity
{
    public interface ISharedUserServices
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<List<UserDto>> GetUsersInRolesAsync(IEnumerable<string> roleNames);

        Task<UserDto> GetUserByIdAsync(string id);
    }
}
