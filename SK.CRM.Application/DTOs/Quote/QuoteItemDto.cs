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
        public decimal? UnitPrice { get; set; }

        // Discount for this item (in percentage, e.g., 0.05 for 5%)
        public decimal DiscountRate { get; set; } = 0;

        public decimal TaxRate { get; set; } = 0;

        public decimal? TotalBeforeDiscount => Quantity * UnitPrice;

        public decimal? DiscountAmount => TotalBeforeDiscount * DiscountRate;

        public decimal? TotalAfterDiscount => TotalBeforeDiscount - DiscountAmount;

        public decimal? TaxAmount => TotalAfterDiscount * TaxRate;

        public decimal? TotalAfterDiscountAndTax => TotalAfterDiscount + TaxAmount;
    }
}
