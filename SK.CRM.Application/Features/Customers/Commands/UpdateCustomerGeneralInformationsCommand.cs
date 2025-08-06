using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Customers.Commands
{
    public sealed record UpdateCustomerGeneralInformationsCommand(CustomerGeneralInformationsDto Customer) : IRequest<(bool IsSuccess, CustomerGeneralInformationsDto? CustomerGeneralInformationsDto, string ErrorMessage)>;

    public class UpdateCustomerGeneralInformationsCommandHandler : IRequestHandler<UpdateCustomerGeneralInformationsCommand, (bool IsSuccess, CustomerGeneralInformationsDto? CustomerGeneralInformationsDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCustomerGeneralInformationsCommandHandler> _logger;
        public UpdateCustomerGeneralInformationsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateCustomerGeneralInformationsCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, CustomerGeneralInformationsDto? CustomerGeneralInformationsDto, string ErrorMessage)> Handle(UpdateCustomerGeneralInformationsCommand request, CancellationToken cancellationToken)
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
                return (true, _mapper.Map<CustomerGeneralInformationsDto>(customer), string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the customer: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while updating the customer. Please try again later.");

            }
        }
    }

}