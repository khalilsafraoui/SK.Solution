using Microsoft.EntityFrameworkCore;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;
using SK.CRM.Infrastructure.PostgreSql.Persistence;


namespace SK.CRM.Infrastructure.PostgreSql.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        private readonly CrmDbContext _context;
        public AddressRepository(CrmDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetAddressesForCustomerAsync(Guid customerId)
        {
            return await _context.Addresses.Where(a => a.CustomerId == customerId).AsNoTracking().ToListAsync();
        }

        public async Task AddAddressToCustomerAsync(Guid customerId, Address address)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                if (address.IsDefault == true)
                {
                    await _context.Addresses.Where(a => a.CustomerId == customerId).ExecuteUpdateAsync(setters => setters.SetProperty(u => u.IsDefault, false));

                }

                if (address.IsBillingAddress == true)
                {
                    await _context.Addresses.Where(a => a.CustomerId == customerId).ExecuteUpdateAsync(setters => setters.SetProperty(u => u.IsBillingAddress, false));
                }
                else
                {
                    address.IsDeliveryAddress = true;
                }
                address.CustomerId = customerId;
                _context.Addresses.Add(address);
            }
        }

        public async Task UpdateAddressAsync(Address updatedAddress)
        {
            if(updatedAddress.IsDefault == true)
            {
                await _context.Addresses.Where(a => a.CustomerId == updatedAddress.CustomerId).ExecuteUpdateAsync(setters => setters.SetProperty(u => u.IsDefault, false));
                
            }

            if (updatedAddress.IsBillingAddress == true)
            {
                await _context.Addresses.Where(a => a.CustomerId == updatedAddress.CustomerId).ExecuteUpdateAsync(setters => setters.SetProperty(u => u.IsBillingAddress, false));
            }
            else
            {
                updatedAddress.IsDeliveryAddress = true;
            }
                var address = await _context.Addresses.FindAsync(updatedAddress.Id);
            if (address != null)
            {
                address.Street = updatedAddress.Street;
                address.CityId = updatedAddress.CityId;
                address.StateId = updatedAddress.StateId;
                address.ZipCode = updatedAddress.ZipCode;
                address.CountryId = updatedAddress.CountryId;
                address.IsDeliveryAddress = updatedAddress.IsDeliveryAddress;
                address.IsBillingAddress = updatedAddress.IsBillingAddress;
                address.IsDefault = updatedAddress.IsDefault;
                _context.Attach(address);
                _context.Entry(address).State = EntityState.Modified;
            }
        }
        
    }
}
