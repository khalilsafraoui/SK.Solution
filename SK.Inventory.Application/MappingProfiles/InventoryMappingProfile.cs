using AutoMapper;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Domain.Entities.Product;

namespace SK.Inventory.Application.MappingProfiles
{
     public class InventoryMappingProfile : Profile
    {
        public InventoryMappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap(); // Map between Product and ProductDto
            CreateMap<Category, CategoryDto>().ReverseMap(); // Map between Category and CategoryDto
        }
    }
}
