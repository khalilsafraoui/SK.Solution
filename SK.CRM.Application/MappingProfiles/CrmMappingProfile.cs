using AutoMapper;
using SK.CRM.Application.Dtos;
using SK.CRM.Application.DTOs;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.MappingProfiles
{
    public class CrmMappingProfile : Profile
    {
        public CrmMappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap(); // Map between Customer and CustomerDTO
            CreateMap<Customer, CustomerGeneralInformationsDto>().ReverseMap(); // Map between Customer and CustomerDTO
            CreateMap<Customer, ProspectGeneralInformationsDto>().ReverseMap(); // Map between Customer and CustomerDTO
            CreateMap<Order, OrderDto>().ReverseMap(); // Map between Order and OrderDto
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();  // Map between OrderDetail and OrderDetailDto
            CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap(); // Map between ShoppingCart and ShoppingCartDto
            CreateMap<Address, AddressDto>().ReverseMap(); // Map between Address and AddressDto
            CreateMap<Inventory.Application.Dtos.CategoryDto, CategoryDto>().ReverseMap();
            CreateMap<Inventory.Application.Dtos.ProductDto, ProductDto>().ReverseMap();

        }
    }
}
