using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Prospects.Queries
{
    public sealed record GetProspectGeneralInformationsByIdQuery(Guid Id) : IRequest<ProspectGeneralInformationsDto?>;

    public sealed class GetProspectGeneralInformationsByIdQueryHandler : IRequestHandler<GetProspectGeneralInformationsByIdQuery, ProspectGeneralInformationsDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetProspectGeneralInformationsByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProspectGeneralInformationsDto?> Handle(GetProspectGeneralInformationsByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetCustomerByIdAsync(request.Id);
            return customer is null ? null : _mapper.Map<ProspectGeneralInformationsDto>(customer);
        }
    }
}
