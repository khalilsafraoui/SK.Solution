using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Customers.Queries
{
    public sealed record GetCustomerByUserIdQuery(string Id) : IRequest<CustomerGeneralInformationsDto?>;

    public sealed class GetCustomerByUserIdQueryHandler : IRequestHandler<GetCustomerByUserIdQuery, CustomerGeneralInformationsDto?>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByUserIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CustomerGeneralInformationsDto?> Handle(GetCustomerByUserIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByUserIdAsync(request.Id);
            return customer is null ? null : _mapper.Map<CustomerGeneralInformationsDto>(customer);
        }
    }
}
