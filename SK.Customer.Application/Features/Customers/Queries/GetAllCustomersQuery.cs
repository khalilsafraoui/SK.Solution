﻿using AutoMapper;
using MediatR;
using SK.Customer.Application.DTOs;
using SK.Customer.Application.Interfaces;

namespace SK.Customer.Application.Features.Customers.Queries
{
    public sealed record GetAllCustomersQuery() : IRequest<List<CustomerDto>>;

    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync();
            if(!customers.Any())
            {
                return new List<CustomerDto>();
            }
            return _mapper.Map<List<CustomerDto>>(customers);
        }
    }
}
