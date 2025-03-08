using SK.Solution.Data.Entities;

namespace SK.Solution.Services
{
    public class OrderServices
    {
        public static List<OrderDetail> ConvertShoppingCartListToOrderDetails(List<ShoppingCart> shoppingCarts)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in shoppingCarts)
            {
                OrderDetail orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Count,
                    Price = Convert.ToDouble(item.Product.Price),
                    ProductName = item.Product.Name
                };
                orderDetails.Add(orderDetail);
            }
            return orderDetails;
        }
    }
}
