using AutoMapper;
using SK.Customer.Application.DTOs;

namespace SK.Customer.Application.MappingProfiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Domain.Entities.Customer, CustomerDto>().ReverseMap(); // Map between Customer and CustomerDTO
        }
    }
}
