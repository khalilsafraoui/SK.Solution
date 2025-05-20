using SK.Solution.Shared.Entities;

namespace SK.Solution.Shared.Interfaces.GeoLocalisation
{
    public interface ICityService
    {
        Task<List<CityDto>> GetAllCitiesAsync();
    }

}
