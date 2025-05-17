using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Orders.Commands
{
    public sealed record CreateOrderCommand(OrderDto Order) : IRequest<OrderDto>;
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ICustomerRepository customerRepository, IAddressRepository addressRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
        }

        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Check if the customer exists
            if(request.Order.CustomerId == Guid.Empty)
            {
                var customer = await _customerRepository.GetCustomerByUserIdAsync(request.Order.UserId);
                if(customer is not null)
                {
                    request.Order.CustomerId = customer.Id;
                    var addresses = await _addressRepository.GetAddressesForCustomerAsync(customer.Id);
                    if (addresses != null && addresses.Any())
                    {
                        var defaultAddress = addresses.FirstOrDefault(a => a.IsDefault);
                        if (defaultAddress != null)
                        {
                            request.Order.AddressId = defaultAddress.Id;
                        }
                    }
                }
            }
            
            var order = _mapper.Map<Order>(request.Order);
            order = await _orderRepository.CreateAsync(order);

            // Map back to DTO after creation to include any updates (e.g., ID)
            return _mapper.Map<OrderDto>(order);
        }
    }
}
