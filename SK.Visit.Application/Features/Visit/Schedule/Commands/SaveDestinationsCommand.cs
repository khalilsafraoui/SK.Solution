using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SK.Visit.Application.Dtos;
using SK.Visit.Application.Interfaces;
using SK.Visit.Application.Settings;
using SK.Visit.Domain.Entities;

namespace SK.Visit.Application.Features.Visit.Schedule.Commands
{
    public sealed record SaveDestinationsCommand(List<VisitPlanningDto> VisitPlannings) : IRequest<List<VisitPlanningDto>>;
    public class SaveDestinationsCommandHandler : IRequestHandler<SaveDestinationsCommand, List<VisitPlanningDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly VisitSettings _settings;
        public SaveDestinationsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IOptions<VisitSettings> options)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _settings = options.Value;
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
            var cutoffDate = DateTime.Today.AddDays(_settings.NumberOfDaysToAddInScheduleGetAndSave).Date;
            destinations = await _unitOfWork.DestinationsRepository.SaveDestinationsAsync(destinations, cutoffDate);

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
