using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Orders.Queries
{
    public sealed record GetOrderBySessionIdQuery(string Id) : IRequest<OrderDto?>;

    public sealed class GetOrderBySessionIdQueryHandler : IRequestHandler<GetOrderBySessionIdQuery, OrderDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetOrderBySessionIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto?> Handle(GetOrderBySessionIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.OrderRepository.GetBySessionIdAsync(request.Id);
            return order is null ? null : _mapper.Map<OrderDto>(order);
        }
    }
}
