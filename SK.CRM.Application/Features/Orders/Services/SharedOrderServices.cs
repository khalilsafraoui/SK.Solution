using AutoMapper;
using SK.CRM.Application.Enums;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;
using SK.Solution.Shared.Interfaces.Crm;
using SK.Solution.Shared.Model.Crm;

namespace SK.CRM.Application.Features.Orders.Services
{
    public class SharedOrderServices: ISharedOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        
        public SharedOrderServices(IOrderRepository orderRepository, ICustomerRepository customerRepository, IAddressRepository addressRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
        }

        public async Task<List<SharedOrderDestinationDto>> GetOrdersDestinaionsAsync()
        {
            IEnumerable<Order> orders = await _orderRepository.GetOrdersByStatusAsync(OrderStatus.StatusReadyForPickUp);
            List<SharedOrderDestinationDto> orderDestinations = new List<SharedOrderDestinationDto>();
            foreach (var order in orders) {
                if (order.CustomerId == null) continue;
                Customer customer = await _customerRepository.GetCustomerByIdAsync(order.CustomerId.Value);
                if(order.AddressId == null) continue;
                Address address = await _addressRepository.GetByIdAsync(order.AddressId.Value);

                orderDestinations.Add(new SharedOrderDestinationDto
                    {
                        IsDelevery = true,
                        OrderId = order.Id,
                        CustomerId = customer.Id,
                    AddressId = address?.Id,
                    Name = customer.FirstName + " " + customer.LastName,
                        Address = address.FullAddress,
                        Phone = customer.PhoneNumber,
                        CityId = address?.CityId,
                        CountryId = address?.CountryId,
                        StateId = address?.StateId,
                        Mark = address != null && address?.Latitude != decimal.Parse("0,000000") ? new SharedMarkDto(double.Parse(address.Latitude.ToString()), double.Parse(address.Longitude.ToString()), customer.FirstName + " " + customer.LastName, customer.Id.ToString()) : null,
                    });
            }
            return orderDestinations;
        }
    }
}
