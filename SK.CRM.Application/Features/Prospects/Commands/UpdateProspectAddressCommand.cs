using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Exceptions;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record UpdateProspectAddressCommand(AddressDto Address) : IRequest<(bool IsSuccess, AddressDto? AddressDto, string ErrorMessage)>;

    public class UpdateProspectAddressCommandHandler : IRequestHandler<UpdateProspectAddressCommand, (bool IsSuccess, AddressDto? AddressDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProspectAddressCommandHandler> _logger;
        public UpdateProspectAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateProspectAddressCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, AddressDto? AddressDto, string ErrorMessage)> Handle(UpdateProspectAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var address = await _unitOfWork.AddressRepository.GetByIdAsync(request.Address.Id)
                           ?? throw new NotFoundException(nameof(Address), request.Address.Id);

                _mapper.Map(request.Address, address);

                await _unitOfWork.AddressRepository.UpdateAddressAsync(address);
                await _unitOfWork.SaveChangesAsync();
                var addressDto = _mapper.Map<AddressDto>(address);
                return (true, addressDto, string.Empty);
            }
            catch (Exception ex) {
                    _logger.LogError(ex, "An error occurred while updating the address: {Message}", ex.Message);
                    return (false, null, "An unexpected error occurred while updating the address. Please try again later.");
            }
           
        }
    }
}
