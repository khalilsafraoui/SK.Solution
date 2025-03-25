using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Prospects.Queries
{
    public sealed record GetProspectByIdQuery(Guid Id) : IRequest<CustomerDto?>;

    public sealed class GetProspectByIdQueryHandler : IRequestHandler<GetProspectByIdQuery, CustomerDto?>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetProspectByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CustomerDto?> Handle(GetProspectByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);
            return customer is null ? null : _mapper.Map<CustomerDto>(customer);
        }
    }
}
