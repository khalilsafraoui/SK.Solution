using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Radzen;
using SK.Identity.Application.Dtos;
using SK.Identity.Domain.Entities;

namespace SK.Identity.Application.Features.Users.Commands
{
    public sealed record SaveUserRolesCommand(UserRolesDto userCreationDto) : IRequest<(bool IsSuccess, string ErrorMessage)>;
    public class SaveUserRolesCommandHandler : IRequestHandler<SaveUserRolesCommand, (bool IsSuccess, string ErrorMessage)>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public SaveUserRolesCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> Handle(SaveUserRolesCommand request, CancellationToken cancellationToken)
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
            }

            if (rolesToRemove.Any())
            {
                var result = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                if (!result.Succeeded)
                {
                    return (false, "Failed to remove roles.");
                }
            }
            return (true, string.Empty);
        }
    }
}
