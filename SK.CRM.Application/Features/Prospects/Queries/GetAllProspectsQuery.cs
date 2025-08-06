using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Prospects.Queries
{
    public sealed record GetAllProspectsQuery(int pageIndex = 0, int pageSize = 0) : IRequest<(bool IsSuccess, List<CustomerDto> ? ProspectsDto, int TotalCount, string ErrorMessage)>;

    public class GetAllProspectsQueryHandler : IRequestHandler<GetAllProspectsQuery, (bool IsSuccess, List<CustomerDto>? ProspectsDto, int TotalCount, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllProspectsQueryHandler> _logger;
        public GetAllProspectsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllProspectsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, List<CustomerDto>? ProspectsDto, int TotalCount, string ErrorMessage)> Handle(GetAllProspectsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _unitOfWork.CustomerRepository.GetAllProspectAsync(request.pageIndex, request.pageSize, false,true);
                if (!result.Items.Any())
                {
                    return (false, new List<CustomerDto>(), 0, "0 Prospect found");
                }
                return (true, _mapper.Map<List<CustomerDto>>(result.Items), result.TotalCount, string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while retrieving prospects: {Message}", ex.Message);
                return (false, null, 0, "An unexpected error occurred while retrieving prospects. Please try again later.");

            }
        }
    }
}
