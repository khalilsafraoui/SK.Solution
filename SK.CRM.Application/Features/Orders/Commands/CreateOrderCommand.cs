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
        private readonly IMapper _mapper;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Check if the customer exists
           
            
            var order = _mapper.Map<Order>(request.Order);
            order = await _orderRepository.CreateAsync(order);

            // Map back to DTO after creation to include any updates (e.g., ID)
            return _mapper.Map<OrderDto>(order);
        }
    }
}
