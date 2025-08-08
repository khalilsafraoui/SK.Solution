using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Customers.Commands
{
    public sealed record CreateCustomerGeneralInformationsCommand(CustomerGeneralInformationsDto Customer) : IRequest<(bool IsSuccess, CustomerGeneralInformationsDto? CustomerGeneralInformationsDto, string ErrorMessage)>;
    public class CreateCustomerGeneralInformationsHandler : IRequestHandler<CreateCustomerGeneralInformationsCommand, (bool IsSuccess, CustomerGeneralInformationsDto? CustomerGeneralInformationsDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCustomerGeneralInformationsHandler> _logger;
        public CreateCustomerGeneralInformationsHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateCustomerGeneralInformationsHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, CustomerGeneralInformationsDto? CustomerGeneralInformationsDto, string ErrorMessage)> Handle(CreateCustomerGeneralInformationsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = _mapper.Map<Customer>(request.Customer);
                
                customer = await _unitOfWork.CustomerRepository.CreateAsync(customer);
                await _unitOfWork.SaveChangesAsync();
                // Map back to DTO after creation to include any updates (e.g., ID)
                return (true, _mapper.Map<CustomerGeneralInformationsDto>(customer), string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling CreateCustomerGeneralInformationsCommand: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while creating the customer. Please try again later.");

            }
        }
    }
}
