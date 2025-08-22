namespace SK.CRM.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string ProductName { get; set; }

        public decimal DiscountRate { get; set; }

        public decimal TaxRate { get; set; }
    }
}
