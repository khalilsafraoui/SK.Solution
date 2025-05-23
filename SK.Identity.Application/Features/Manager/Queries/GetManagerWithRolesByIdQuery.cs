using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SK.Identity.Application.Dtos;
using SK.Identity.Domain.Entities;

namespace SK.Identity.Application.Features.Manager.Queries
{
    public sealed record GetManagerWithRolesByIdQuery(string userId) : IRequest<(bool IsSuccess, UserRolesDto UserRoles, string ErrorMessage)>;

    public class GetManagerWithRolesByIdQueryHandler : IRequestHandler<GetManagerWithRolesByIdQuery, (bool IsSuccess, UserRolesDto UserRoles, string ErrorMessage)>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetManagerWithRolesByIdQueryHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<(bool IsSuccess, UserRolesDto UserRoles, string ErrorMessage)> Handle(GetManagerWithRolesByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.userId);
            if (user == null)
            {
                return (false, null, "Manager not found.");
            }

            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            UserRolesDto userRolesDto = new UserRolesDto
            {
                UserId = user.Id,
                UserEmail = user.Email,
                RoleSelections = allRoles.ToDictionary(role => role, role => userRoles.Contains(role)),
                AllRoles = allRoles.Where(x => x.Contains("Manager"))
                                    .GroupBy(role => role.Contains('_') ? role.Split('_')[0] : "All")
                                    .OrderBy(g => g.Key)
                                    .ToDictionary(g => Capitalize(g.Key), g => g.OrderBy(r => r).ToList())
            };

            return (true, userRolesDto, string.Empty);
        }

        private string Capitalize(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}
