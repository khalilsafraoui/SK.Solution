using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SK.CRM.Application.Features.Orders.Commands
{
    public sealed record UpdateOrderCommand(OrderDto OrderDto) : IRequest<(bool IsSuccess, OrderDto? OrderDto, string ErrorMessage)>;

    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, (bool IsSuccess, OrderDto? OrderDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;
        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, OrderDto? OrderDto, string ErrorMessage)> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var order = _mapper.Map<Order>(request.OrderDto);
                await Validate(order);
                var IsUpdated = await _unitOfWork.OrderRepository.UpdateAsync(order);
                foreach (var item in order.OrderDetails)
                {
                    item.OrderId = order.Id; // Ensure each item is linked to the quote
                }
                await _unitOfWork.OrderDetailRepository.UpdateItemsAsync(order.Id, order.OrderDetails.ToList());
                await _unitOfWork.SaveChangesAsync();

                if (!IsUpdated)
                {
                    return (false, null, "Failed to update order. Please try again later.");
                }

                return (true, _mapper.Map<OrderDto>(order), string.Empty);
            }
            catch (ValidationException valEx)
            {
                return (false, null, $"Validation failed: {valEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the order: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while updating the order. Please try again later.");
            }
        }

        private async Task Validate(Order order)
        {
            if (!order.OrderDetails.Any())
            {
                throw new ValidationException("Items are required.");
            }

        }
    }
}
