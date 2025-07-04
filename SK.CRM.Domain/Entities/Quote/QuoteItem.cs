﻿namespace SK.CRM.Domain.Entities.Quote
{
    public class QuoteItem : BaseEntity
    {
        public int ProductId { get; set; }

        public Guid QuoteId { get; set; }

        public required string ProductName { get; set; }
        public int Quantity { get; set; } = 1;
        public double? UnitPrice { get; set; }

        // Discount for this item (in percentage, e.g., 0.05 for 5%)
        public double DiscountRate { get; set; } = 0;

        public double TaxRate { get; set; }

    }
}
