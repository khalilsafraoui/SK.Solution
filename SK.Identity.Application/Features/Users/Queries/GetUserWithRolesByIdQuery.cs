using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SK.Identity.Application.Dtos;
using SK.Identity.Domain.Entities;
using SK.Solution.Shared.Utility;

namespace SK.Identity.Application.Features.Users.Queries
{
    public sealed record GetUserWithRolesByIdQuery(string userId) : IRequest<(bool IsSuccess, UserRolesDto UserRoles, string ErrorMessage)>;

    public class GetUserWithRolesByIdQueryHandler : IRequestHandler<GetUserWithRolesByIdQuery, (bool IsSuccess, UserRolesDto UserRoles, string ErrorMessage)>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetUserWithRolesByIdQueryHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<(bool IsSuccess, UserRolesDto UserRoles, string ErrorMessage)> Handle(GetUserWithRolesByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.userId);
            if (user == null)
            {
                return (false, null, "User not found.");
            }

            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            UserRolesDto userRolesDto = new UserRolesDto
            {
                UserId = user.Id,
                UserEmail = user.Email,
                RoleSelections = allRoles.ToDictionary(role => role, role => userRoles.Contains(role)),
                AllRoles = allRoles.Where(x => !x.Contains(RoleType.Customer) && !x.Contains("Manager") && !x.Contains(RoleType.Admin))
                                    .GroupBy(role => role.Contains('_') ? role.Split('_')[0] : "Other")
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
