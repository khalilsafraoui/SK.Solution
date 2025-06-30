using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Customers.Queries
{
    public sealed record GetCustomerAddressesQuery(Guid customerId) : IRequest<(bool IsSuccess, List<AddressDto>? Addresses, string ErrorMessage)>;

    public sealed class GetCustomerAddressesQueryHandler : IRequestHandler<GetCustomerAddressesQuery, (bool IsSuccess, List<AddressDto>? Addresses, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetCustomerAddressesQueryHandler> _logger;
        public GetCustomerAddressesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetCustomerAddressesQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, List<AddressDto>? Addresses, string ErrorMessage)> Handle(GetCustomerAddressesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var addresses = await _unitOfWork.AddressRepository.GetAddressesForCustomerAsync(request.customerId);
                if (addresses == null || !addresses.Any())
                {
                    return (false, null, "No addresses found for the customer.");
                }
                return (true, _mapper.Map<List<AddressDto>>(addresses), string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting addresses for customer {CustomerId}.", request.customerId);
                return (false, null, "An error occurred while retrieving addresses.");
            }
        }
    }
}
