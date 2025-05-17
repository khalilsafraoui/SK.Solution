using Microsoft.AspNetCore.Identity;
using SK.Identity.Domain.Entities;
using SK.Solution.Shared.Interfaces.Identity;
using SK.Solution.Shared.Model.Identity;

namespace SK.Identity.Application.Account
{
    public class SharedUserServices : ISharedUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SharedUserServices(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var result = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UserDto
                {
                    UserId = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = roles.ToList()
                });
            }

            return result;
        }

        public async Task<List<UserDto>> GetUsersInRolesAsync(IEnumerable<string> roleNames)
        {
            var result = new List<UserDto>();
            var processedUserIds = new HashSet<string>();

            foreach (var roleName in roleNames)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                    continue;

                var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);

                foreach (var user in usersInRole)
                {
                    if (processedUserIds.Contains(user.Id))
                        continue;

                    var roles = await _userManager.GetRolesAsync(user);

                    result.Add(new UserDto
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Roles = roles.ToList()
                    });

                    processedUserIds.Add(user.Id);
                }
            }

            return result;
        }

    }
}
