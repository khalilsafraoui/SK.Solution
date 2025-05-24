using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SK.Identity.Application.Dtos;
using SK.Identity.Domain.Entities;
using SK.Solution.Shared.Utility;

namespace SK.Identity.Application.Features.Manager.Commands
{

    public sealed record CreateManagerCommand(ManagerCreationDto managerCreationDto) : IRequest<ManagerCreationDto>;
    public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, ManagerCreationDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public CreateManagerCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<ManagerCreationDto> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<ApplicationUser>(request.managerCreationDto);
            user.UserName = request.managerCreationDto.Email;
            user.EmailConfirmed = true; // Assuming email confirmation is not required for manager creation
            var result = await _userManager.CreateAsync(user, request.managerCreationDto.Password);

            if (!result.Succeeded)
            {
                var errorMessages = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception($"Operation failed: {errorMessages}");
            }

            foreach (var role in RoleType.managerRoles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Ensure roles exist
            foreach (var role in request.managerCreationDto.Roles.Where(r => r.Value).Select(r => r.Key))
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            // Map back to DTO after creation to include any updates (e.g., ID)
            return _mapper.Map<ManagerCreationDto>(user);
        }
    }
}
