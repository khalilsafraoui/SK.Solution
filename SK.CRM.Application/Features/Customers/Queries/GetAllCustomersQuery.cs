using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Customers.Queries
{
    public sealed record GetAllCustomersQuery(int pageIndex = 0, int pageSize = 0) : IRequest<(bool IsSuccess, List<CustomerDto>? Customers, int TotalCount, string ErrorMessage)>;

    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, (bool IsSuccess, List<CustomerDto>? Customers, int TotalCount, string ErrorMessage)>
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

        public async Task<(bool IsSuccess, List<CustomerDto>? Customers, int TotalCount, string ErrorMessage)> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.CustomerRepository.GetAllCustomersAsync(request.pageIndex, request.pageSize, false, true);
                if (!result.Items.Any())
                {
                    return (false, new List<CustomerDto>(), 0, "0 Customer found");
                }
                return (true, _mapper.Map<List<CustomerDto>>(result.Items), result.TotalCount, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all customers.");
                return (false, null, 0, "An error occurred while retrieving customers.");
            }
        }
    }
}
