using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record CreateProspectGeneralInformationsCommand(ProspectGeneralInformationsDto Customer) : IRequest<ProspectGeneralInformationsDto>;
    public class CreateProspectGeneralInformationsCommandHandler : IRequestHandler<CreateProspectGeneralInformationsCommand, ProspectGeneralInformationsDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CreateProspectGeneralInformationsCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<ProspectGeneralInformationsDto> Handle(CreateProspectGeneralInformationsCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request.Customer);
            customer.IsProspect = true;
            customer = await _customerRepository.CreateAsync(customer);

            // Map back to DTO after creation to include any updates (e.g., ID)
            return _mapper.Map<ProspectGeneralInformationsDto>(customer);
        }
    }
}
