using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Prospects.Queries
{
    public sealed record GetProspectByIdQuery(Guid Id) : IRequest<(bool IsSuccess, CustomerDto? CustomerDto, string ErrorMessage)>;

    public sealed class GetProspectByIdQueryHandler : IRequestHandler<GetProspectByIdQuery, (bool IsSuccess, CustomerDto? CustomerDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProspectByIdQueryHandler> _logger;
        public GetProspectByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetProspectByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, CustomerDto? CustomerDto, string ErrorMessage)> Handle(GetProspectByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Id);
                if (customer is null)
                {
                    return (false, null, "Prospect not found.");
                }
                if (!customer.IsProspect)
                {
                    return (false, null, "The customer is not a prospect.");
                }
                return (true, _mapper.Map<CustomerDto>(customer), string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while retrieving the prospect with ID {ProspectId}: {Message}", request.Id, ex.Message);
                return (false, null, $"An error occurred while retrieving the prospect: {ex.Message}");
            }
        }
    }
}
