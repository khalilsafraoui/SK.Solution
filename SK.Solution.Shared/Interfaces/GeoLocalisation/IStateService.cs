using SK.Solution.Shared.Entities;

namespace SK.Solution.Shared.Interfaces.GeoLocalisation
{
    public interface IStateService
    {
        Task<List<StateDto>> GetAllStatesAsync();
    }

}
