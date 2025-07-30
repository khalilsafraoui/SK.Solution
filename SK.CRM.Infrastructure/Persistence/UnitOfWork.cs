using SK.Solution.Shared.Interfaces.Identity;
using SK.CRM.Application.Interfaces;
using SK.Solution.Shared.Interfaces.Inventory.Product;

namespace SK.CRM.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CrmDbContext _context;

        public ISharedUserServices SharedUserServices { get; private set; }

        public IAddressRepository AddressRepository { get; private set; }

        public ICustomerRepository CustomerRepository { get; private set; }

        public IOrderRepository OrderRepository { get; private set; }

        public IOrderDetailRepository OrderDetailRepository { get; private set; }

        public IShoppingCartRepository ShoppingCartRepository { get; private set; }

        public IQuoteRepository QuoteRepository { get; private set; }

        public IQuoteItemRepository QuoteItemRepository { get; private set; }

        public ISharedProductServices SharedProductServices { get; private set; }

        public UnitOfWork(
        CrmDbContext context, ISharedUserServices sharedUserServices, IAddressRepository addressRepository, ICustomerRepository customerRepository, IOrderRepository orderRepository, IShoppingCartRepository shoppingCartRepository, IQuoteRepository quoteRepository, IQuoteItemRepository quoteItemRepository, ISharedProductServices sharedProductServices, IOrderDetailRepository orderDetailRepository)
        {
            _context = context;
            SharedUserServices = sharedUserServices;
            AddressRepository = addressRepository;
            CustomerRepository = customerRepository;
            OrderRepository = orderRepository;
            ShoppingCartRepository = shoppingCartRepository;
            QuoteRepository = quoteRepository;
            QuoteItemRepository = quoteItemRepository;
            SharedProductServices = sharedProductServices;
            OrderDetailRepository = orderDetailRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
