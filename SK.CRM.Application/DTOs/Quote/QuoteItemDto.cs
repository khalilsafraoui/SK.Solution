namespace SK.CRM.Application.DTOs.Quote
{
    public class QuoteItemDto
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }

        public Guid QuoteId { get; set; }
        public QuoteDto Quote { get; set; }

        public string ProductName { get; set; }
        public int Quantity { get; set; } = 1;
        public double? UnitPrice { get; set; }

        // Discount for this item (in percentage, e.g., 0.05 for 5%)
        public double DiscountRate { get; set; } = 0;

        public double? TotalBeforeDiscount => Quantity * UnitPrice;

        public double? DiscountAmount => TotalBeforeDiscount * DiscountRate;

        public double? TotalAfterDiscount => TotalBeforeDiscount - DiscountAmount;
    }
}
