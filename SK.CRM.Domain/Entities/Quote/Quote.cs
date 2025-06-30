namespace SK.CRM.Domain.Entities.Quote
{
    public class Quote : BaseEntity
    {
       
        public string? UserId { get; set; } // The user who created the quote

        public Guid CustomerId { get; set; }
        
        public Guid AddressId { get; set; }
        public string? FullName { get; set; }
        public string? FullAddress { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public string Status { get; set; }

        public List<QuoteItem> Items { get; set; } = new();

        // Currency code (e.g., USD, EUR, GBP)
        public string Currency { get; set; } = "EUR";

        // Global discount applied to the whole quote (in percentage, e.g., 0.10 for 10%)
        public double GlobalDiscountRate { get; set; } = 0;

        public double TaxRate { get; set; } = 0; // Example: 20%

        public string? Notes { get; set; }
    }
}
