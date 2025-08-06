using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Prospects.Queries
{
    public sealed record GetProspectAddressesQuery(Guid customerId) : IRequest<(bool IsSuccess, List<AddressDto>? AddressDtos, string ErrorMessage)>;

    public sealed class GetProspectAddressesQueryHandler : IRequestHandler<GetProspectAddressesQuery, (bool IsSuccess, List<AddressDto>? AddressDtos, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProspectAddressesQueryHandler> _logger;
        public GetProspectAddressesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetProspectAddressesQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, List<AddressDto>? AddressDtos, string ErrorMessage)> Handle(GetProspectAddressesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var addresses = await _unitOfWork.AddressRepository.GetAddressesForCustomerAsync(request.customerId);
                if (addresses == null || !addresses.Any())
                {
                    return (false, null, "No addresses found for the specified customer.");
                }
                return (true, _mapper.Map<List<AddressDto>>(addresses), string.Empty);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while retrieving addresses for customer {CustomerId}: {Message}", request.customerId, ex.Message);
                return (false, null, $"An error occurred while retrieving addresses for customer: {request.customerId}");
            }
            
            
        }
    }
}
