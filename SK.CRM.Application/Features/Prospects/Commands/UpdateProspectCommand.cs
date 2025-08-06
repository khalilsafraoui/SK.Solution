using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record UpdateProspectCommand(CustomerDto Customer) : IRequest<(bool IsSuccess, CustomerDto? CustomerDto, string ErrorMessage)>;

    public class UpdateProspectCommandHandler : IRequestHandler<UpdateProspectCommand, (bool IsSuccess, CustomerDto? CustomerDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProspectCommandHandler> _logger;
        public UpdateProspectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateProspectCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, CustomerDto? CustomerDto, string ErrorMessage)> Handle(UpdateProspectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Customer.Id);
                if (customer == null)
                    return (false, null, "Customer not found.");

                _mapper.Map(request.Customer, customer);

                var updated = await _unitOfWork.CustomerRepository.UpdateAsync(customer);
                await _unitOfWork.SaveChangesAsync();
                if (!updated)
                {
                    return (false, null, "Failed to update customer.");
                }
                return (true, _mapper.Map<CustomerDto>(customer), string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the prospect: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while updating the prospect. Please try again later.");
            }
        }
    }

}
