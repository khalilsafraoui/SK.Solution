using MediatR;
using Microsoft.AspNetCore.Identity;
using SK.Identity.Application.Dtos;
using SK.Identity.Domain.Entities;
using SK.Solution.Shared.Utility;

namespace SK.Identity.Application.Features.Manager.Commands
{
    public sealed record SaveManagerRolesCommand(UserRolesDto userCreationDto) : IRequest<(bool IsSuccess, string ErrorMessage)>;
    public class SaveManagerRolesCommandHandler : IRequestHandler<SaveManagerRolesCommand, (bool IsSuccess, string ErrorMessage)>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SaveManagerRolesCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> Handle(SaveManagerRolesCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.userCreationDto.UserId);
            if (user == null)
            {
                return (false, "User not found.");
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = request.userCreationDto.RoleSelections.Where(kv => kv.Value).Select(kv => kv.Key).ToList();

            var rolesToAdd = selectedRoles.Except(currentRoles).ToList();
            var rolesToRemove = currentRoles.Except(selectedRoles).ToList();

            if (rolesToAdd.Any())
            {
                var result = await _userManager.AddToRolesAsync(user, rolesToAdd);
                if (!result.Succeeded)
                {
                    return (false, "Failed to add roles.");
                }
                foreach (var role in rolesToAdd)
                {
                    await HandleRole(role, true);
                }
            }

            if (rolesToRemove.Any())
            {
                var result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                if (!result.Succeeded)
                {
                    return (false, "Failed to remove roles.");
                }
                foreach (var role in rolesToRemove)
                {
                    await HandleRole(role, false);
                }
            }
            return (true, string.Empty);
        }

        public async Task HandleRole(string role, bool toCreate)
        {
            switch (role)
            {


                case "Inventory_Manager":
                    {
                        if (toCreate)
                        {
                            await CreateRolesAsync(RoleType.InventoryRoles);
                        }
                        else
                        {
                            await DeleteRolesAsync(RoleType.InventoryRoles);
                        }

                    }
                    break;
                case "Crm_Manager":
                    {
                        if (toCreate)
                        {
                            await CreateRolesAsync(RoleType.CrmRoles);
                        }
                        else
                        {
                            await DeleteRolesAsync(RoleType.CrmRoles);
                        }
                    }
                    break;
                case "Visit_Manager":
                    {
                        if (toCreate)
                        {
                            await CreateRolesAsync(RoleType.VisitRoles);
                        }
                        else
                        {
                            await DeleteRolesAsync(RoleType.VisitRoles);
                        }
                    }
                    break;
                case "Note_Manager":
                    {
                        if (toCreate)
                        {
                            await CreateRolesAsync(RoleType.NoteRoles);
                        }
                        else
                        {
                            await DeleteRolesAsync(RoleType.NoteRoles);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public async Task CreateRolesAsync(List<string> roles)
        {
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public async Task DeleteRolesAsync(List<string> roles)
        {
            foreach (var roleName in roles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role == null)
                    continue; // Role doesn't exist

                // Get all users
                var users = _userManager.Users.ToList();

                foreach (var user in users)
                {
                    if (await _userManager.IsInRoleAsync(user, roleName))
                    {
                        await _userManager.RemoveFromRoleAsync(user, roleName);
                    }
                }

                // Delete the role
                await _roleManager.DeleteAsync(role);
            }
        }
    }
}
