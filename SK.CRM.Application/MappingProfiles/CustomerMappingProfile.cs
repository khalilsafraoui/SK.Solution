using AutoMapper;
using SK.CRM.Application.DTOs;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.MappingProfiles
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap(); // Map between Customer and CustomerDTO
        }
    }
}
