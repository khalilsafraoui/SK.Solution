﻿using AutoMapper;
using SK.CRM.Application.Dtos;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.DTOs.Quote;
using SK.CRM.Application.MappingProfiles.Converter;
using SK.CRM.Domain.Entities;
using SK.CRM.Domain.Entities.Quote;
using SK.Solution.Shared.Model.Crm;
using SK.Solution.Shared.Model.Inventory.Product;

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
            CreateMap<SharedCustomerDto, Customer>().ReverseMap(); // Map between SharedCustomerDto and CustomerDTO
            CreateMap<SharedAddressDto, Address>().ReverseMap(); // Map between SharedAddressDto and AddressDto
            CreateMap<Quote, QuoteDto>().ReverseMap(); // Map between Quote and QuoteDto
            CreateMap<QuoteItem, QuoteItemDto>().ReverseMap(); // Map between QuoteItem and QuoteItemDto
            CreateMap<Quote, Order>().ConvertUsing<QuoteToOrderConverter>(); // Map between QuoteDto and Order
            CreateMap<QuoteItem, OrderDetail>().ConvertUsing<QuoteItemToOrderDetailConverter>(); // Map between QuoteItemDto and OrderDetail
            CreateMap<Quote_ProductsDto, SharedProductForCrmDto>().ReverseMap(); // Map between Quote_ProductsDto and SharedProductForCrmDto
        }
    }
}
