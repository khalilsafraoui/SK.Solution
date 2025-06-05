using SK.Solution.Shared.Interfaces.Identity;

namespace SK.CRM.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository AddressRepository { get; }

        ICustomerRepository CustomerRepository { get; }

        IOrderRepository OrderRepository { get; }

        IShoppingCartRepository ShoppingCartRepository { get; }

        IQuoteRepository QuoteRepository { get; }

        IQuoteItemRepository QuoteItemRepository { get; }

        ISharedUserServices SharedUserServices { get; }

        Task<int> SaveChangesAsync();
    }

}
