

using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Exceptions;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Orders.Commands
{
   

    public sealed record UpdateOrderStatusCommand(Guid orderId, string status, string paymentIntentId) : IRequest<OrderDto>;

    public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, OrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateOrderStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.OrderRepository.UpdateStatusAsync(request.orderId,request.status,request.paymentIntentId)
                            ?? throw new NotFoundException(nameof(Order), request.orderId);
            await _unitOfWork.SaveChangesAsync();

            if (order is null)
            {
                throw new ApplicationException("Failed to update customer.");
            }

            return _mapper.Map<OrderDto>(order);
        }
    }
}
