using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Interfaces
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        // You can add custom methods specific to Customer, if needed
        Task<IEnumerable<Address>> GetAddressesForCustomerAsync(Guid customerId);

        Task AddAddressToCustomerAsync(Guid customerId, Address address);

        Task UpdateAddressAsync(Address updatedAddress);

    }
}
