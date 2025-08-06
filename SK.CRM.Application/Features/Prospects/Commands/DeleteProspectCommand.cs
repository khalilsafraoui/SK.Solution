using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.Interfaces;


namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record DeleteProspectCommand(Guid CustomerId) : IRequest<(bool IsSuccess, string ErrorMessage)>;


    public class DeleteProspectCommandHandler : IRequestHandler<DeleteProspectCommand, (bool IsSuccess, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteProspectCommandHandler> _logger;
        public DeleteProspectCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteProspectCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> Handle(DeleteProspectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId);
                if (customer == null)
                {
                    return (false, "Prospect not found.");
                }

                await _unitOfWork.CustomerRepository.DeleteAsync(request.CustomerId);
                await _unitOfWork.SaveChangesAsync();
                return (true, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the Prospect: {Message}", ex.Message);
                return (false, "An unexpected error occurred while deleting the Prospect. Please try again later.");

            }
        }
    }

}
