using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Customers.Commands
{
    public sealed record DeleteCustomerAddressCommand(Guid AddressId) : IRequest<(bool IsSuccess, string ErrorMessage)>;


    public class DeleteCustomerAddressCommandHandler : IRequestHandler<DeleteCustomerAddressCommand, (bool IsSuccess, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteCustomerAddressCommandHandler> _logger;
        public DeleteCustomerAddressCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteCustomerAddressCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var address = await _unitOfWork.AddressRepository.GetByIdAsync(request.AddressId);
                if (address == null)
                {
                    return (false, "Address not found.");
                }

                if (address.IsDefault)
                {
                    return (false, "Cannot delete the default address. Please set another address as default before deleting this one.");
                }

                await _unitOfWork.AddressRepository.DeleteAsync(request.AddressId);
                await _unitOfWork.SaveChangesAsync();
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling DeleteCustomerAddressCommand: {Message}", ex.Message);
                return (false, $"An error occurred while deleting the address: {ex.Message}");
            }
        }
    }
}
