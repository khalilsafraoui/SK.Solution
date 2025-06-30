using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Customers.Queries
{
    public sealed record GetAllCustomersQuery() : IRequest<(bool IsSuccess, List<CustomerDto>? Customers, string ErrorMessage)>;

    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, (bool IsSuccess, List<CustomerDto>? Customers, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCustomersQueryHandler> _logger;
        public GetAllCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllCustomersQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, List<CustomerDto>? Customers, string ErrorMessage)> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customers = await _unitOfWork.CustomerRepository.GetAllCustomersAsync();
                if (customers == null || customers.Count == 0)
                {
                    return (false, null, "No customers found.");
                }
                return (true, _mapper.Map<List<CustomerDto>>(customers), string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all customers.");
                return (false, null, "An error occurred while retrieving customers.");
            }
        }
    }
}
