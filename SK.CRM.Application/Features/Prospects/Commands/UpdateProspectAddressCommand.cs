using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Exceptions;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record UpdateProspectAddressCommand(AddressDto Address) : IRequest;

    public class UpdateProspectAddressCommandHandler : IRequestHandler<UpdateProspectAddressCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateProspectAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(UpdateProspectAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _unitOfWork.AddressRepository.GetByIdAsync(request.Address.Id)
                            ?? throw new NotFoundException(nameof(Address), request.Address.Id);

            _mapper.Map(request.Address, address);

            await _unitOfWork.AddressRepository.UpdateAddressAsync(address);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
