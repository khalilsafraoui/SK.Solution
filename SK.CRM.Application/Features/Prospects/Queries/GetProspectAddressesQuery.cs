using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Prospects.Queries
{
    public sealed record GetProspectAddressesQuery(Guid customerId) : IRequest<List<AddressDto?>>;

    public sealed class GetProspectAddressesQueryHandler : IRequestHandler<GetProspectAddressesQuery, List<AddressDto?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetProspectAddressesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AddressDto?>> Handle(GetProspectAddressesQuery request, CancellationToken cancellationToken)
        {
            var addresses = await _unitOfWork.AddressRepository.GetAddressesForCustomerAsync(request.customerId);
            if (!addresses.Any())
            {
                return new List<AddressDto>();
            }
            return _mapper.Map<List<AddressDto>>(addresses);
        }
    }
}
