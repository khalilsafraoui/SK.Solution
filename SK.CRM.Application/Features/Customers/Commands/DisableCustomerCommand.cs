using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.Interfaces;


namespace SK.CRM.Application.Features.Customers.Commands
{
    public sealed record DisableCustomerCommand(Guid CustomerId) : IRequest<(bool IsSuccess, string ErrorMessage)>;


    public class DisableCustomerCommandHandler : IRequestHandler<DisableCustomerCommand, (bool IsSuccess, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DisableCustomerCommandHandler> _logger;
        public DisableCustomerCommandHandler(IUnitOfWork unitOfWork, ILogger<DisableCustomerCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> Handle(DisableCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId);
                if (customer == null)
                {
                    return (false, "Customer not found.");
                }

                await _unitOfWork.CustomerRepository.DisableCustomerAsync(request.CustomerId);
                await _unitOfWork.SaveChangesAsync();
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling DisableCustomerCommand: {Message}", ex.Message);
                return (false, $"An error occurred while disabling the customer: {ex.Message}");
            }
        }
    }

}
