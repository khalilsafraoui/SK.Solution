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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var address = _mapper.Map<Address>(request.Address);
            await _unitOfWork.AddressRepository.AddAddressToCustomerAsync(request.CustomerId,address);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
