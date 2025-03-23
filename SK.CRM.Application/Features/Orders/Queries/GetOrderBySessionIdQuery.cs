using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Orders.Queries
{
    public sealed record GetOrderBySessionIdQuery(string Id) : IRequest<OrderDto?>;

    public sealed class GetOrderBySessionIdQueryHandler : IRequestHandler<GetOrderBySessionIdQuery, OrderDto?>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderBySessionIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OrderDto?> Handle(GetOrderBySessionIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetBySessionIdAsync(request.Id);
            return order is null ? null : _mapper.Map<OrderDto>(order);
        }
    }
}
