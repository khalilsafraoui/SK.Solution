using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Exceptions;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Customers.Commands
{
    public sealed record UpdateCustomerAddressCommand(AddressDto Address) : IRequest;

    public class UpdateCustomerAddressCommandHandler : IRequestHandler<UpdateCustomerAddressCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateCustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _unitOfWork.AddressRepository.GetByIdAsync(request.Address.Id)
                            ?? throw new NotFoundException(nameof(Address), request.Address.Id);

            _mapper.Map(request.Address, address);

            await _unitOfWork.AddressRepository.UpdateAddressAsync(address);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
