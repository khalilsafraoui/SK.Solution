using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        // You can add custom methods specific to Customer, if needed
        Task<List<Customer>> GetAllProspectAsync();

        Task<List<Customer>> GetAllCustomersAsync();

        Task<bool> DisableCustomerAsync(Guid customerId);

        Task<Customer> GetCustomerByIdAsync(Guid id);
    }
}
