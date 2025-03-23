using Microsoft.AspNetCore.Components;
using SK.CRM.Application.DTOs;

using Stripe;
using Stripe.Checkout;
using MediatR;
using SK.CRM.Application.Features.Orders.Queries;
using SK.CRM.Application.Features.Orders.Commands;
using SK.CRM.Application.Enums;

namespace SK.CRM.UI.Blazor.Services
{
    public class PaymentService
    {
        private readonly NavigationManager _navigationManager;
        private readonly IMediator _mediator;

        public PaymentService(NavigationManager navigationManager, IMediator mediator)
        {
            _navigationManager = navigationManager;
            _mediator = mediator;
        }

        public Session CreateStripeCheckoutSession(OrderDto order)
        {
            StripeConfiguration.ApiKey = "sk_test_51R0l2YPupSv7iO06N91owa9Sw1qqRuUYmSCoFdY0TPOLHTaQjSbrq6G6HtJE5V0lmuPGx05kYfLF8g4RjxEMJ4xb00vo4Cf6Uh";

            var lineItems = order.OrderDetails.Select(order => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    UnitAmountDecimal = (decimal?)(order.Price * 100),
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = order.ProductName,
                    },
                },
                Quantity = order.Quantity,
            }).ToList();

            var options = new SessionCreateOptions
            {
                SuccessUrl = $"{_navigationManager.BaseUri}order/confirmation/{{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{_navigationManager.BaseUri}cart",
                LineItems = lineItems,
                Mode = "payment",
            };


            

            var service = new SessionService();
            var session = service.Create(options);
            return session;
        }

        public async Task<OrderDto> CheckPaymentStatusAndUpdateOrderAsync(string sessionId)
        {
            var query = new GetOrderBySessionIdQuery(sessionId);
            OrderDto order = await _mediator.Send(query);
           
            if (order != null)
            {
                var service = new SessionService();
                var session = service.Get(sessionId);
                if(session.PaymentStatus.ToLower() == "paid")
                {
                    var command = new UpdateOrderStatusCommand(order.Id, OrderStatus.StatusApproved, session.PaymentIntentId);
                    order = await _mediator.Send(command);
                }
            }
            return order;
        }
    }
}
