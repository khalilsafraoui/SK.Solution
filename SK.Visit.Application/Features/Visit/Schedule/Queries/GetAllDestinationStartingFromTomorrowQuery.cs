using AutoMapper;
using MediatR;
using SK.Visit.Application.Dtos;
using SK.Visit.Application.Interfaces;

namespace SK.Visit.Application.Features.Visit.Schedule.Queries
{
    public sealed record GetAllDestinationStartingFromTomorrowQuery() : IRequest<List<DestinationDto>>;

    public class GetAllDestinationStartingFromTomorrowQueryHandler : IRequestHandler<GetAllDestinationStartingFromTomorrowQuery, List<DestinationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllDestinationStartingFromTomorrowQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DestinationDto>> Handle(GetAllDestinationStartingFromTomorrowQuery request, CancellationToken cancellationToken)
        {
            var destinations = await _unitOfWork.DestinationsRepository.GetDestinationsStartingFromTomorrowAsync();
            if(!destinations.Any())
            {
                return new List<DestinationDto>();
            }
            return _mapper.Map<List<DestinationDto>>(destinations);
        }
    }
}
