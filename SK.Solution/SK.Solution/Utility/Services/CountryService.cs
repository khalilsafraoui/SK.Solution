using SK.Solution.Shared.Entities;
using SK.Solution.Shared.Interfaces.GeoLocalisation;

namespace SK.Solution.Utility.Services
{
    public class CountryService : ICountryService
    {
        private readonly JsonFileService _jsonService;

        public CountryService(JsonFileService jsonService)
        {
            _jsonService = jsonService;
        }

        public Task<List<CountryDto>> GetAllCountriesAsync()
        {
            return _jsonService.ReadJsonFileAsync<CountryDto>("geo/countries.json");
        }
    }

}
