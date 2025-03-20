﻿using AutoMapper;
using MediatR;
using SK.Customer.Application.DTOs;
using SK.Customer.Application.Exceptions;
using SK.Customer.Application.Interfaces;

namespace SK.Customer.Application.Features.Customers.Commands
{
    public sealed record UpdateCustomerCommand(CustomerDto Customer) : IRequest<CustomerDto>;

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Customer.Id)
                            ?? throw new NotFoundException(nameof(Customer), request.Customer.Id);

            _mapper.Map(request.Customer, customer);

            var updated = await _customerRepository.UpdateAsync(customer);
            if (!updated)
            {
                throw new ApplicationException("Failed to update customer.");
            }

            return _mapper.Map<CustomerDto>(customer);
        }
    }

}
