using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Prospects.Queries
{
    public sealed record GetProspectGeneralInformationsByIdQuery(Guid Id) : IRequest<(bool IsSuccess, ProspectGeneralInformationsDto? ProspectGeneralInformationsDto, string ErrorMessage)>;

    public sealed class GetProspectGeneralInformationsByIdQueryHandler : IRequestHandler<GetProspectGeneralInformationsByIdQuery, (bool IsSuccess, ProspectGeneralInformationsDto? ProspectGeneralInformationsDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProspectGeneralInformationsByIdQueryHandler> _logger;
        public GetProspectGeneralInformationsByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetProspectGeneralInformationsByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, ProspectGeneralInformationsDto? ProspectGeneralInformationsDto, string ErrorMessage)> Handle(GetProspectGeneralInformationsByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetCustomerByIdAsync(request.Id);
                if (customer is null)
                {
                    return (false, null, "Prospect not found.");
                }
                if (!customer.IsProspect)
                {
                    return (false, null, "The customer is not a prospect.");
                }
                return (true, _mapper.Map<ProspectGeneralInformationsDto>(customer), string.Empty);
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
