using SK.CRM.Application.Enums;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;
using SK.Solution.Shared.Interfaces.Crm;
using SK.Solution.Shared.Model.Crm;

namespace SK.CRM.Application.Features.Orders.Services
{
    public class SharedOrderServices: ISharedOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public SharedOrderServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SharedOrderDestinationDto>> GetOrdersDestinaionsAsync()
        {
            IEnumerable<Order> orders = await _unitOfWork.OrderRepository.GetOrdersByStatusAsync(OrderStatus.StatusReadyForPickUp);
            List<SharedOrderDestinationDto> orderDestinations = new List<SharedOrderDestinationDto>();
            foreach (var order in orders) {
                if (order.CustomerId == null) continue;
                Customer customer = await _unitOfWork.CustomerRepository.GetCustomerByIdAsync(order.CustomerId.Value);
                if(order.AddressId == null) continue;
                Address address = await _unitOfWork.AddressRepository.GetByIdAsync(order.AddressId.Value);

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
