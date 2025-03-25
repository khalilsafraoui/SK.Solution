using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Customers.Commands
{
    public sealed record CreateProspectCommand(CustomerDto Customer) : IRequest<CustomerDto>;
    public class CreateProspectCommandHandler : IRequestHandler<CreateProspectCommand, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CreateProspectCommandHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Handle(CreateProspectCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request.Customer);
            customer.IsProspect = true;
            customer = await _customerRepository.CreateAsync(customer);

            // Map back to DTO after creation to include any updates (e.g., ID)
            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
