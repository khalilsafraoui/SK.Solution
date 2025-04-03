using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record CreateProspectAddressCommand(Guid CustomerId,AddressDto Address) : IRequest;
    public class CreateProspectAddressCommandHandler : IRequestHandler<CreateProspectAddressCommand>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public CreateProspectAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task Handle(CreateProspectAddressCommand request, CancellationToken cancellationToken)
        {
            var address = _mapper.Map<Address>(request.Address);
            await _addressRepository.AddAddressToCustomerAsync(request.CustomerId,address);
        }
    }
}
