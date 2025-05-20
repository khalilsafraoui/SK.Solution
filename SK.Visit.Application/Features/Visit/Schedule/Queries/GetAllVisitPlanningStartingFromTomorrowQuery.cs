using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SK.Solution.Shared.Model.Identity;
using SK.Visit.Application.Dtos;
using SK.Visit.Application.Interfaces;
using SK.Visit.Application.Settings;

namespace SK.Visit.Application.Features.Visit.Schedule.Queries
{
    public sealed record GetAllVisitPlanningStartingFromTomorrowQuery() : IRequest<List<VisitPlanningDto>>;

    public class GetAllVisitPlanningStartingFromTomorrowQueryHandler : IRequestHandler<GetAllVisitPlanningStartingFromTomorrowQuery, List<VisitPlanningDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly VisitSettings _settings;

        public GetAllVisitPlanningStartingFromTomorrowQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,IOptions<VisitSettings> options)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _settings = options.Value;
        }

        public async Task<List<VisitPlanningDto>> Handle(GetAllVisitPlanningStartingFromTomorrowQuery request, CancellationToken cancellationToken)
        {
            List<VisitPlanningDto> visitPlannings = new List<VisitPlanningDto>();
            var tomorrow = DateTime.Now.AddDays(_settings.NumberOfDaysToAddInScheduleGetAndSave).Date;
            var destinations = await _unitOfWork.DestinationsRepository.GetDestinationsStartingFromSpecificDateAsync(tomorrow);
           
            List<UserDto> agents = await _unitOfWork.SharedUserServices.GetUsersInRolesAsync(new[] { "Admin", "Manager" });

            agents.ForEach(agent =>
            {
                var agentDestinations = destinations.Where(x => x.AgentId == agent.UserId);
                List<DestinationDto> agentDestinationsDto = new List<DestinationDto>();
                if (agentDestinations.Any())
                {
                   agentDestinationsDto = _mapper.Map<List<DestinationDto>>(agentDestinations);
                }
                
                visitPlannings.Add(new VisitPlanningDto { _Agent = new AgentDto { Id = agent.UserId, Name = agent.FirstName + " " + agent.LastName, Color = GetRandomHexColor() }, Destinations = agentDestinationsDto });
            });
            return visitPlannings;
        }

        private static string GetRandomHexColor()
        {
            var random = new Random();
            return $"#{random.Next(0x1000000):X6}"; // Generates something like "#A1B2C3"
        }
    }
}
