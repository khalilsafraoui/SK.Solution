using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record DeleteProspectAddressCommand(Guid AddressId) : IRequest<(bool IsSuccess, string ErrorMessage)>;


    public class DeleteProspectAddressCommandHandler : IRequestHandler<DeleteProspectAddressCommand, (bool IsSuccess, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteProspectAddressCommandHandler> _logger;
        public DeleteProspectAddressCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteProspectAddressCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> Handle(DeleteProspectAddressCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var address = await _unitOfWork.AddressRepository.GetByIdAsync(request.AddressId);
                if (address == null)
                {
                    return (false, "Prospect address not found.");
                }

                if (address.IsDefault)
                {
                    return (false, "Cannot delete the default address. Please set another address as default before deleting this one.");
                }

                await _unitOfWork.AddressRepository.DeleteAsync(request.AddressId);
                await _unitOfWork.SaveChangesAsync();
                return (true,string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the Prospect address: {Message}", ex.Message);
                return (false, "An unexpected error occurred while deleting the Prospect address. Please try again later.");
            }
        }
    }
}
