using SK.Solution.Shared.Model.Crm;


namespace SK.Solution.Shared.Interfaces.Crm
{
    public interface ISharedCustomerServices
    {
        Task<List<SharedCustomerDto>> GetCusomersAsync();
    }
}
