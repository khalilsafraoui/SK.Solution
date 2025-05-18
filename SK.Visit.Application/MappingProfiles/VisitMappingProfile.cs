using AutoMapper;
using SK.Visit.Application.Dtos;
using SK.Visit.Domain.Entities;

namespace SK.Visit.Application.MappingProfiles
{
     public class VisitMappingProfile : Profile
    {
        public VisitMappingProfile()
        {
            CreateMap<Destination, DestinationDto>().ReverseMap(); // Map between Destination and DestinationDto

        }
    }
}
