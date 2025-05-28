using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Prospects.Queries
{
    public sealed record GetProspectByIdQuery(Guid Id) : IRequest<CustomerDto?>;

    public sealed class GetProspectByIdQueryHandler : IRequestHandler<GetProspectByIdQuery, CustomerDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetProspectByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerDto?> Handle(GetProspectByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Id);
            return customer is null ? null : _mapper.Map<CustomerDto>(customer);
        }
    }
}
