using SK.Solution.Shared.Interfaces.Identity;
using SK.Solution.Shared.Interfaces.Inventory.Product;

namespace SK.CRM.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository AddressRepository { get; }

        ICustomerRepository CustomerRepository { get; }

        IOrderRepository OrderRepository { get; }

        IOrderDetailRepository OrderDetailRepository { get; }

        IShoppingCartRepository ShoppingCartRepository { get; }

        IQuoteRepository QuoteRepository { get; }

        IQuoteItemRepository QuoteItemRepository { get; }

        ISharedUserServices SharedUserServices { get; }

        ISharedProductServices SharedProductServices { get; }

        Task<int> SaveChangesAsync();
    }

}
