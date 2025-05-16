using SK.Visit.UI.Blazor.Model;

namespace SK.Visit.UI.Blazor.Services
{
    public interface IVisitPlanningServices
    {
        List<Destination> GetDestinationsBySelectedDate(VisitPlanning visitPlanning, DateTime selectedDate);

        int DestinationCountBySelectedDate(VisitPlanning visitPlanning, DateTime selectedDate);

        List<Destination> AddDestination(VisitPlanning visitPlanning, Destination destination, DateTime selectedDate);

        bool DeleteVisitPlanned(VisitPlanning visitPlanning, Destination destination, DateTime selectedDate);


        Task<List<VisitPlanning>> GetVisitPlanningsAsync();

        List<Destination> GetDistinations();

        List<KeyValueAddress> GetCities();

        List<KeyValueAddress> GetStates();

        List<KeyValueAddress> GetCountries();
    }
}
