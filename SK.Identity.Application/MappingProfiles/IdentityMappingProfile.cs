using AutoMapper;
using SK.Identity.Application.Dtos;
using SK.Identity.Domain.Entities;

namespace SK.Identity.Application.MappingProfiles
{
    public class IdentityMappingProfile : Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<ApplicationUser, UserCreationDto>().ReverseMap(); // Map between ApplicationUser and UserCreationDto
            CreateMap<ApplicationUser, ManagerCreationDto>().ReverseMap(); // Map between ApplicationUser and ManagerCreationDto
        }
    }
}
