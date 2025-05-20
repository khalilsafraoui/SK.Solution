using SK.Solution.Shared.Entities;
using SK.Solution.Shared.Interfaces.GeoLocalisation;

namespace SK.Solution.Utility.Services
{
    public class StateService : IStateService
    {
        private readonly JsonFileService _jsonService;

        public StateService(JsonFileService jsonService)
        {
            _jsonService = jsonService;
        }

        public Task<List<StateDto>> GetAllStatesAsync()
        {
            return _jsonService.ReadJsonFileAsync<StateDto>("geo/states.json");
        }
    }

}
