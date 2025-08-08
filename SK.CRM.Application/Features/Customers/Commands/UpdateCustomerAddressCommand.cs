using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Exceptions;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Customers.Commands
{
    public sealed record UpdateCustomerAddressCommand(AddressDto Address) : IRequest<(bool IsSuccess, string ErrorMessage)>;

    public class UpdateCustomerAddressCommandHandler : IRequestHandler<UpdateCustomerAddressCommand, (bool IsSuccess, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCustomerAddressCommandHandler> _logger;
        public UpdateCustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateCustomerAddressCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var address = await _unitOfWork.AddressRepository.GetByIdAsync(request.Address.Id)
                            ?? throw new NotFoundException(nameof(Address), request.Address.Id);

                _mapper.Map(request.Address, address);

                await _unitOfWork.AddressRepository.UpdateAddressAsync(address);
                await _unitOfWork.SaveChangesAsync();
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling UpdateCustomerAddressCommand: {Message}", ex.Message);
                return (false, $"An error occurred while updating the address: {ex.Message}");
            }
            
        }
    }
}
