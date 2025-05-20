using AutoMapper;
using MediatR;
using SK.Visit.Application.Dtos;
using SK.Visit.Application.Interfaces;

namespace SK.Visit.Application.Features.Visit.AgentTrip.Queries
{
    public sealed record GetAgentToDayTripQuery(string AgentId, DateTime Date) : IRequest<List<TripDto>>;

    public class GetAgentToDayTripQueryHandler : IRequestHandler<GetAgentToDayTripQuery, List<TripDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAgentToDayTripQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TripDto>> Handle(GetAgentToDayTripQuery request, CancellationToken cancellationToken)
        {
            var destinations = await _unitOfWork.DestinationsRepository.GetDestinationsByAgentAndDayAsync(request.AgentId, request.Date);

            
            if(destinations == null || destinations.Count == 0)
            {
                return new List<TripDto>();
            }
            return _mapper.Map<List<TripDto>>(destinations);
        }
    }
}
