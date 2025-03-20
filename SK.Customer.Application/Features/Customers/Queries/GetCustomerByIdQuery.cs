using AutoMapper;
using MediatR;
using SK.Customer.Application.DTOs;
using SK.Customer.Application.Interfaces;

namespace SK.Customer.Application.Features.Customers.Queries
{
    public sealed record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerDto?>;

    public sealed class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerByIdHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);
            return customer is null ? null : _mapper.Map<CustomerDto>(customer);
        }
    }
}
