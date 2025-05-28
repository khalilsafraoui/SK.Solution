using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Prospects.Queries
{
    public sealed record GetAllProspectsQuery() : IRequest<List<CustomerDto>>;

    public class GetAllProspectsQueryHandler : IRequestHandler<GetAllProspectsQuery, List<CustomerDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllProspectsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CustomerDto>> Handle(GetAllProspectsQuery request, CancellationToken cancellationToken)
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllProspectAsync();
            if(!customers.Any())
            {
                return new List<CustomerDto>();
            }
            return _mapper.Map<List<CustomerDto>>(customers);
        }
    }
}
