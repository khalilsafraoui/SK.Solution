using SK.Solution.Shared.Model.Crm;

namespace SK.Solution.Shared.Interfaces.Crm
{
    public interface ISharedOrderServices
    {
        Task<List<SharedOrderDestinationDto>> GetOrdersDestinaionsAsync();
    }
}
