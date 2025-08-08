using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Customers.Commands
{
    public sealed record CreateCustomerAddressCommand(Guid CustomerId,AddressDto Address) : IRequest<(bool IsSuccess, string ErrorMessage)>;
    public class CreateCustomerAddressCommandHandler : IRequestHandler<CreateCustomerAddressCommand, (bool IsSuccess, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerAddressCommandHandler> _logger;
        public CreateCustomerAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateCustomerAddressCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> Handle(CreateCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var address = _mapper.Map<Address>(request.Address);
                await _unitOfWork.AddressRepository.AddAddressToCustomerAsync(request.CustomerId, address);
                await _unitOfWork.SaveChangesAsync();
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling CreateCustomerAddressCommand: {Message}", ex.Message);
                return (false, $"An error occurred while creating the address: {ex.Message}");
            }
        }
    }
}
