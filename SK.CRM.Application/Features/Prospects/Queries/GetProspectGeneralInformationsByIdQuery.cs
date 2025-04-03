using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Prospects.Queries
{
    public sealed record GetProspectGeneralInformationsByIdQuery(Guid Id) : IRequest<ProspectGeneralInformationsDto?>;

    public sealed class GetProspectGeneralInformationsByIdQueryHandler : IRequestHandler<GetProspectGeneralInformationsByIdQuery, ProspectGeneralInformationsDto?>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetProspectGeneralInformationsByIdQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProspectGeneralInformationsDto?> Handle(GetProspectGeneralInformationsByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(request.Id);
            return customer is null ? null : _mapper.Map<ProspectGeneralInformationsDto>(customer);
        }
    }
}
