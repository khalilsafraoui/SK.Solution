using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record CreateProspectAddressCommand(Guid CustomerId,AddressDto Address) : IRequest<(bool IsSuccess, AddressDto? AddressDto, string ErrorMessage)>;
    public class CreateProspectAddressCommandHandler : IRequestHandler<CreateProspectAddressCommand,(bool IsSuccess, AddressDto? AddressDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProspectAddressCommandHandler> _logger;
        public CreateProspectAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateProspectAddressCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, AddressDto? AddressDto, string ErrorMessage)> Handle(CreateProspectAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var address = _mapper.Map<Address>(request.Address);
                await _unitOfWork.AddressRepository.AddAddressToCustomerAsync(request.CustomerId, address);
                await _unitOfWork.SaveChangesAsync();
                var addressDto = _mapper.Map<AddressDto>(address);
                return (true, addressDto, string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while creating the address for customer {CustomerId}: {Message}", request.CustomerId, ex.Message);
                return (false, null, "An unexpected error occurred while creating the address. Please try again later.");
            }
            
        }
    }
}
