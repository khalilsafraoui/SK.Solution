using SK.Solution.Shared.Entities;

namespace SK.Solution.Shared.Interfaces.GeoLocalisation
{
    public interface ICountryService
    {
        Task<List<CountryDto>> GetAllCountriesAsync();
    }
}
