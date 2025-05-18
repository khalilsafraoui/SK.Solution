using SK.Visit.Application.Dtos;
using SK.Visit.UI.Blazor.Model;

namespace SK.Visit.UI.Blazor.Services
{
    public interface IVisitPlanningServices
    {
        List<DestinationDto> GetDestinationsBySelectedDate(VisitPlanningDto visitPlanning, DateTime selectedDate);

        int DestinationCountBySelectedDate(VisitPlanningDto visitPlanning, DateTime selectedDate);

        List<DestinationDto> AddDestination(List<VisitPlanningDto> visitPlannings, List<DestinationDto> Destinations, VisitPlanningDto visitPlanning, DestinationDto destination, DateTime selectedDate);

        bool DeleteVisitPlanned(List<VisitPlanningDto> visitPlannings, List<DestinationDto> Destinations, VisitPlanningDto visitPlanning, DestinationDto destination, DateTime selectedDate);

        List<KeyValueAddress> GetCities();

        List<KeyValueAddress> GetStates();

        List<KeyValueAddress> GetCountries();
    }
}
