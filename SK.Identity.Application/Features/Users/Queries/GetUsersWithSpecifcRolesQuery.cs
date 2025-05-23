using MediatR;
using SK.Solution.Shared.Interfaces.Identity;
using SK.Solution.Shared.Model.Identity;
using SK.Solution.Shared.Utility;

namespace SK.Identity.Application.Features.Users.Queries
{
    public sealed record GetUsersWithSpecifcRolesQuery() : IRequest<(bool IsSuccess, List<UserDto> UserRoles, string ErrorMessage)>;

    public class GetUsersWithSpecifcRolesQueryHandler : IRequestHandler<GetUsersWithSpecifcRolesQuery, (bool IsSuccess, List<UserDto> UserDto, string ErrorMessage)>
    {
        private readonly ISharedUserServices _UserService;

        public GetUsersWithSpecifcRolesQueryHandler(ISharedUserServices UserService)
        {
            _UserService = UserService;
        }


        public async Task<(bool IsSuccess, List<UserDto> UserDto, string ErrorMessage)> Handle(GetUsersWithSpecifcRolesQuery request, CancellationToken cancellationToken)
        {
            var users = await _UserService.GetUsersAsync();
            if (users == null || !users.Any())
            {
                return (false, null, "(0) user retrived");
            }
            users = users.Where(x => !x.Roles.Contains(RoleType.Customer) && !x.Roles.Contains(RoleType.Inventory_Manager) && !x.Roles.Contains(RoleType.Crm_Manager) && !x.Roles.Contains(RoleType.Visit_Manager) && !x.Roles.Contains(RoleType.Note_Manager) && !x.Roles.Contains(RoleType.Admin)).ToList();

            
            if (!users.Any())
            {
                return (false, null, "(0) user with Targeted Roles");
            }

            return (true, users, string.Empty);
        }
    }
}
