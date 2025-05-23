using MediatR;
using SK.Solution.Shared.Interfaces.Identity;
using SK.Solution.Shared.Model.Identity;
using SK.Solution.Shared.Utility;

namespace SK.Identity.Application.Features.Manager.Queries
{
    public sealed record GetManagerQuery() : IRequest<(bool IsSuccess, UserDto Manager, string ErrorMessage)>;

    public class GetManagerQueryHandler : IRequestHandler<GetManagerQuery, (bool IsSuccess, UserDto Manager, string ErrorMessage)>
    {
        private readonly ISharedUserServices _UserService;

        public GetManagerQueryHandler(ISharedUserServices UserService)
        {
            _UserService = UserService;
        }


        public async Task<(bool IsSuccess, UserDto Manager, string ErrorMessage)> Handle(GetManagerQuery request, CancellationToken cancellationToken)
        {

            var users = await _UserService.GetUsersAsync();
            if (users == null || !users.Any())
            {
                return (false, null, "(0) user retrived");
            }
            var manager = users.FirstOrDefault(u => u.Roles.Any(r =>
                r == RoleType.Inventory_Manager ||
                r == RoleType.Crm_Manager ||
                r == RoleType.Visit_Manager ||
                r == RoleType.Note_Manager));


            if (manager is null)
            {
                return (false, null, "(0) manager with Targeted Roles");
            }

            return (true, manager, string.Empty);
        }
    }
}
