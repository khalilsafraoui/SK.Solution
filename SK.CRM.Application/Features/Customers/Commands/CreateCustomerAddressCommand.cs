using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Customers.Commands
{
    public sealed record CreateCustomerAddressCommand(Guid CustomerId,AddressDto Address) : IRequest;
    public class CreateCustomerAddressCommandHandler : IRequestHandler<CreateCustomerAddressCommand>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public CreateCustomerAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var address = _mapper.Map<Address>(request.Address);
            await _addressRepository.AddAddressToCustomerAsync(request.CustomerId,address);
        }
    }
}
