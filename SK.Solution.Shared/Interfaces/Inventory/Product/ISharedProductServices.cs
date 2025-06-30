using SK.Solution.Shared.Model.Inventory.Product;

namespace SK.Solution.Shared.Interfaces.Inventory.Product
{
    public interface ISharedProductServices
    {
        Task<(bool IsSuccess, List<SharedProductForCrmDto>? Products, string ErrorMessage)> GetProductForCrmUseAsync();
    }
}
