using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Customers.Queries
{
    public sealed record GetCustomerAddressesQuery(Guid customerId) : IRequest<List<AddressDto?>>;

    public sealed class GetCustomerAddressesQueryHandler : IRequestHandler<GetCustomerAddressesQuery, List<AddressDto?>>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public GetCustomerAddressesQueryHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<AddressDto?>> Handle(GetCustomerAddressesQuery request, CancellationToken cancellationToken)
        {
            var addresses = await _addressRepository.GetAddressesForCustomerAsync(request.customerId);
            if (!addresses.Any())
            {
                return new List<AddressDto>();
            }
            return _mapper.Map<List<AddressDto>>(addresses);
        }
    }
}
