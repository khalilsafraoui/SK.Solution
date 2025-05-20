using SK.Solution.Shared.Entities;
using SK.Solution.Shared.Interfaces.GeoLocalisation;

namespace SK.Solution.Utility.Services
{
    public class CityService : ICityService
    {
        private readonly JsonFileService _jsonService;

        public CityService(JsonFileService jsonService)
        {
            _jsonService = jsonService;
        }

        public Task<List<CityDto>> GetAllCitiesAsync()
        {
            return _jsonService.ReadJsonFileAsync<CityDto>("geo/cities.json");
        }
    }

}
