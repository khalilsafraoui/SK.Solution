using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        // You can add custom methods specific to Customer, if needed
        Task<List<Customer>> GetAllProspectAsync();

        Task<(List<Customer> Items, int TotalCount)> GetAllProspectAsync(int pageIndex = 0,
            int pageSize = 10,
            bool include = false,
            bool noTracking = false);

        Task<List<Customer>> GetAllCustomersAsync();

        Task<(List<Customer> Items, int TotalCount)> GetAllCustomersAsync(int pageIndex = 0,
            int pageSize = 10,
            bool include = false,
            bool noTracking = false);

        Task<bool> DisableCustomerAsync(Guid customerId);

        Task<Customer> GetCustomerByIdAsync(Guid id);

        Task<Customer> GetCustomerByUserIdAsync(string userId);
    }
}
