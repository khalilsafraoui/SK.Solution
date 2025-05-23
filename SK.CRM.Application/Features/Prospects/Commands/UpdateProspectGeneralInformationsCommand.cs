﻿using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Exceptions;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Customers.Commands
{
    public sealed record UpdateCustomerGeneralInformationsCommand(CustomerGeneralInformationsDto Customer) : IRequest<CustomerGeneralInformationsDto>;

    public class UpdateCustomerGeneralInformationsCommandHandler : IRequestHandler<UpdateCustomerGeneralInformationsCommand, CustomerGeneralInformationsDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateCustomerGeneralInformationsCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerGeneralInformationsDto> Handle(UpdateCustomerGeneralInformationsCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Customer.Id)
                            ?? throw new NotFoundException(nameof(Customer), request.Customer.Id);

            _mapper.Map(request.Customer, customer);

            var updated = await _customerRepository.UpdateAsync(customer);
            if (!updated)
            {
                throw new ApplicationException("Failed to update customer.");
            }

            return _mapper.Map<CustomerGeneralInformationsDto>(customer);
        }
    }

}
