using AutoMapper;
using MediatR;
using SK.Visit.Application.Dtos;
using SK.Visit.Application.Interfaces;
using SK.Visit.Domain.Entities;

namespace SK.Visit.Application.Features.Visit.Schedule.Commands
{
    public sealed record SaveDestinationsCommand(List<VisitPlanningDto> VisitPlannings) : IRequest<List<VisitPlanningDto>>;
    public class SaveDestinationsCommandHandler : IRequestHandler<SaveDestinationsCommand, List<VisitPlanningDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SaveDestinationsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<VisitPlanningDto>> Handle(SaveDestinationsCommand request, CancellationToken cancellationToken)
        {
            List<Destination> destinations = new List<Destination>();
            foreach (var visitPlanning in request.VisitPlannings)
            {
                // Remove fake destinations
                visitPlanning.Destinations.RemoveAll(i => i.IsFake == true);
                var destinationsToBeSaved = _mapper.Map<List<Destination>>(visitPlanning.Destinations);
                foreach (var item in destinationsToBeSaved)
                {
                    item.AgentId = visitPlanning._Agent.Id;
                }
                // Validate the destinations entity before proceeding
                await ValidateProduct(destinationsToBeSaved);
                destinations.AddRange(destinationsToBeSaved);


            }
            destinations = await _unitOfWork.DestinationsRepository.SaveDestinationsAsync(destinations);

            await _unitOfWork.SaveChangesAsync();
            // Map back to DTO after creation to include any updates (e.g., ID)
           // return _mapper.Map<List<DestinationDto>>(destinations);
            return request.VisitPlannings;
        }

        private async Task ValidateProduct(List<Destination> Destinations)
        {
            // Validate Name
           
        }
    }
}
