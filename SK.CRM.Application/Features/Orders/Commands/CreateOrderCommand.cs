using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;
using SK.CRM.Domain.Entities.Quote;
using System.ComponentModel.DataAnnotations;

namespace SK.CRM.Application.Features.Orders.Commands
{
    public sealed record CreateOrderCommand(OrderDto Order) : IRequest<(bool IsSuccess, OrderDto? OrderDto, string ErrorMessage)>;
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, (bool IsSuccess, OrderDto? OrderDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateOrderCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, OrderDto? OrderDto, string ErrorMessage)> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // Check if the customer exists
            try
            {
                var order = _mapper.Map<Order>(request.Order);
                await Validate(order);
                order = await _unitOfWork.OrderRepository.CreateAsync(order);
                await _unitOfWork.SaveChangesAsync();
                // Map back to DTO after creation to include any updates (e.g., ID)
                return (true,_mapper.Map<OrderDto>(order),string.Empty);
            }
            catch (ValidationException valEx)
            {
                return (false, null, $"Validation failed: {valEx.Message}");
            }
            catch (Exception ex)
            {

                // Log the exception or handle it as needed
                _logger.LogError(ex, "An error occurred while creating the order: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while creating the order. Please try again later.");
            }
        }

        private async Task Validate(Order order)
        {
            if (!order.OrderDetails.Any())
            {
                throw new ValidationException("Items are required.");
            }

            if (string.IsNullOrEmpty(order.Email))
            {
                throw new ValidationException("Email is required.");
            }

            if (string.IsNullOrEmpty(order.PhoneNumber))
            {
                throw new ValidationException("Phone Number is required.");
            }

        }
    }
}
