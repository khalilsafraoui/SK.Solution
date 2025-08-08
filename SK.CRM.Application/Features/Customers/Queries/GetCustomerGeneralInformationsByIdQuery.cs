using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Features.Prospects.Queries;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Customers.Queries
{
    public sealed record GetCustomerGeneralInformationsByIdQuery(Guid Id) : IRequest<(bool IsSuccess, CustomerGeneralInformationsDto? CustomerGeneralInformationsDto, string ErrorMessage)>;

    public sealed class GetCustomerGeneralInformationsByIdQueryHandler : IRequestHandler<GetCustomerGeneralInformationsByIdQuery, (bool IsSuccess, CustomerGeneralInformationsDto? CustomerGeneralInformationsDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCustomerGeneralInformationsByIdQueryHandler> _logger;
        public GetCustomerGeneralInformationsByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetCustomerGeneralInformationsByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, CustomerGeneralInformationsDto? CustomerGeneralInformationsDto, string ErrorMessage)> Handle(GetCustomerGeneralInformationsByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetCustomerByIdAsync(request.Id);
                if (customer is null)
                {
                    return (false, null, "Customer not found.");
                }
                if (customer.IsProspect)
                {
                    return (false, null, "The customer is a prospect.");
                }
                return (true, _mapper.Map<CustomerGeneralInformationsDto>(customer), string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while retrieving the customer with ID {CustomerId}: {Message}", request.Id, ex.Message);
                return (false, null, $"An error occurred while retrieving the customer: {ex.Message}");
            }
        }
    }
}
