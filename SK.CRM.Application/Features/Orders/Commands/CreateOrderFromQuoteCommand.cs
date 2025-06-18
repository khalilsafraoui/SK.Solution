using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.DTOs.Quote;
using SK.CRM.Application.Enums;
using SK.CRM.Application.Features.Quotes.Commands;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Orders.Commands
{
    public sealed record CreateOrderFromQuoteCommand(Guid quoteId) : IRequest<(bool IsSuccess, OrderDto? OrderDto, string ErrorMessage)>;
    public class CreateOrderFromQuoteCommandHandler : IRequestHandler<CreateOrderFromQuoteCommand, (bool IsSuccess, OrderDto? OrderDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateOrderFromQuoteCommandHandler> _logger;
        public CreateOrderFromQuoteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateOrderFromQuoteCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, OrderDto? OrderDto, string ErrorMessage)> Handle(CreateOrderFromQuoteCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var quoteResult = await _unitOfWork.QuoteRepository.GetByIdAsync(request.quoteId);
                if (quoteResult == null)
                {
                    return (false, null, "Quote not found.");
                }

                if (quoteResult.Status != QuoteStatus.StatusAccepted)
                {
                    return (false, null, "Only accepted quotes can be converted to orders.");
                }
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(quoteResult.CustomerId);
                if (customer == null)
                {
                    return (false, null, "Customer not found for the quote.");
                }

                var order = _mapper.Map<Order>(quoteResult);

                if (order == null)
                {
                    return (false, null, "Failed to map quote to order.");
                }
                order.Name = customer.FirstName + " " + customer.LastName;
                order.Email = customer.Email; // Assuming the order needs the customer's email
                order.PhoneNumber = customer.PhoneNumber; // Assuming the order needs the customer's phone number
                order = await _unitOfWork.OrderRepository.CreateAsync(order);
                var quote = await _unitOfWork.QuoteRepository.UpdateStatusAsync(quoteResult.Id, QuoteStatus.StatusCompleted);
                await _unitOfWork.SaveChangesAsync();
                if (quote is null)
                {
                    return (false, null, "Failed to update quote status. Please try again later.");
                }
                var orderDto = _mapper.Map<OrderDto>(order);

                if (orderDto == null)
                {
                    return (false, null, "Failed to map order to DTO after saving.");
                }

                return (true, orderDto, string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating an order from quote: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while creating the order. Please try again later.");
            }
        }


    }
}
