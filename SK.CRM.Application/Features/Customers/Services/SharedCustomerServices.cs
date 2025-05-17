using AutoMapper;
using SK.CRM.Application.Interfaces;
using SK.Solution.Shared.Interfaces.Crm;
using SK.Solution.Shared.Model.Crm;

namespace SK.CRM.Application.Features.Customers.Services
{
    public class SharedCustomerServices : ISharedCustomerServices
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public SharedCustomerServices(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        public async Task<List<SharedCustomerDto>> GetCusomersAsync()
        {
            return _mapper.Map<List<SharedCustomerDto>>(await _customerRepository.GetAllCustomersAsync()); 
        }
    }
    
}
