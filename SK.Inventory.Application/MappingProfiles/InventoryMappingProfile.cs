using AutoMapper;
using SK.Inventory.Application.Dtos;
using SK.Inventory.Domain.Entities.Product;
using SK.Solution.Shared.Model.Inventory.Product;

namespace SK.Inventory.Application.MappingProfiles
{
     public class InventoryMappingProfile : Profile
    {
        public InventoryMappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap(); // Map between Product and ProductDto
            CreateMap<Category, CategoryDto>().ReverseMap(); // Map between Category and CategoryDto
            CreateMap<Product, SharedProductForCrmDto>().ReverseMap(); // Map between Product and SharedProductForCrmDto with custom mapping for CategoryName


        }
    }
}
