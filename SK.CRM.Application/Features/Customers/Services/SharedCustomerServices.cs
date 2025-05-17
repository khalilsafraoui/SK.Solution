using AutoMapper;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;
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

        public async Task<List<SharedCustomerDestinationDto>> GetCusomersDestinationsAsync()
        {
            List<Customer> customers = await _customerRepository.GetAllCustomersAsync();
            List<SharedCustomerDestinationDto> customerDestinations = new List<SharedCustomerDestinationDto>();
            foreach (var customer in customers)
            {
                customer.Addresses.ToList().ForEach(address =>
                {
                    customerDestinations.Add(new SharedCustomerDestinationDto
                    {
                        CustomerId = customer.Id,
                        AddressId = address.Id,
                        Name = customer.FirstName + " " + customer.LastName,
                        Address = address.FullAddress,
                        Phone = customer.PhoneNumber,
                        CityId = address.CityId,
                        CountryId = address.CountryId,
                        StateId = address.StateId,
                        Mark = address.Latitude != decimal.Parse("0,000000") ? new SharedMarkDto(double.Parse(address.Latitude.ToString()), double.Parse(address.Longitude.ToString()), customer.FirstName + " " + customer.LastName, customer.Id.ToString()) : null,
                    });

                });
            }
            return customerDestinations;
        }
    }
    
}
