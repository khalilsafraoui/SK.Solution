﻿using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Exceptions;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record UpdateProspectGeneralInformationsCommand(ProspectGeneralInformationsDto Customer) : IRequest<ProspectGeneralInformationsDto>;

    public class UpdateProspectGeneralInformationsCommandHandler : IRequestHandler<UpdateProspectGeneralInformationsCommand, ProspectGeneralInformationsDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public UpdateProspectGeneralInformationsCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<ProspectGeneralInformationsDto> Handle(UpdateProspectGeneralInformationsCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Customer.Id)
                            ?? throw new NotFoundException(nameof(Customer), request.Customer.Id);

            _mapper.Map(request.Customer, customer);

            var updated = await _customerRepository.UpdateAsync(customer);
            if (!updated)
            {
                throw new ApplicationException("Failed to update Prospect.");
            }

            return _mapper.Map<ProspectGeneralInformationsDto>(customer);
        }
    }

}
