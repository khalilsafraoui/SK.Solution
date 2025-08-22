using SK.CRM.Application.DTOs;

namespace SK.CRM.UI.Blazor.Services
{
    public class OrderServices
    {
        public static List<OrderDetailDto> ConvertShoppingCartListToOrderDetails(List<ShoppingCartDto> shoppingCarts)
        {
            List<OrderDetailDto> orderDetails = new List<OrderDetailDto>();
            foreach (var item in shoppingCarts)
            {
                OrderDetailDto orderDetail = new OrderDetailDto
                {
                    ProductId = item.ProductId,
                    Quantity = item.Count,
                    Price = item.Product.Price,
                    ProductName = item.Product.Name
                };
                orderDetails.Add(orderDetail);
            }
            return orderDetails;
        }
    }
}
