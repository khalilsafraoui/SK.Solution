using Microsoft.AspNetCore.Components;
using SK.Solution.Data.Entities;
using SK.Solution.Repository.IRepository;
using SK.Solution.Utility;
using Stripe;
using Stripe.Checkout;

namespace SK.Solution.Services
{
    public class PaymentService
    {
        private readonly NavigationManager _navigationManager;
        private readonly IOrderRepository _orderRepository;

        public PaymentService(NavigationManager navigationManager, IOrderRepository orderRepository)
        {
            _navigationManager = navigationManager;
            _orderRepository = orderRepository;
        }

        public Session CreateStripeCheckoutSession(Order order)
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

        public async Task<Order> CheckPaymentStatusAndUpdateOrderAsync(string sessionId)
        {
            Order order = await _orderRepository.GetBySessionIdAsync(sessionId);
            if (order != null)
            {
                var service = new SessionService();
                var session = service.Get(sessionId);
                if(session.PaymentStatus.ToLower() == "paid")
                {
                    order = await _orderRepository.UpdateStatusAsync(order.Id, OrderStatus.StatusApproved, session.PaymentIntentId);
                }
            }
            return order;
        }
    }
}
