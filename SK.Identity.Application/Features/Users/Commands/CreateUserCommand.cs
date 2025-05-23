using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SK.Identity.Application.Dtos;
using SK.Identity.Domain.Entities;

namespace SK.Identity.Application.Features.Users.Commands
{
    public sealed record CreateUserCommand(UserCreationDto userCreationDto) : IRequest<UserCreationDto>;
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserCreationDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        
        public async Task<UserCreationDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<ApplicationUser>(request.userCreationDto);
            user.UserName = request.userCreationDto.Email;
            var result = await _userManager.CreateAsync(user, request.userCreationDto.Password);

            if (!result.Succeeded)
            {
                var errorMessages = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new Exception($"Operation failed: {errorMessages}");
            }

            // Map back to DTO after creation to include any updates (e.g., ID)
            return _mapper.Map<UserCreationDto>(user);
        }
    }
}
