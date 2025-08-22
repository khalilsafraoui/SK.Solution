using SK.CRM.Application.Enums;

namespace SK.CRM.Application.DTOs.Quote
{
    public class QuoteDto
    {
        public Guid Id { get; set; }
       
        public string UserId { get; set; } // The user who created the quote

        public Guid CustomerId { get; set; }
        public CustomerGeneralInformationsDto Customer { get; set; }
        public Guid AddressId { get; set; }

        public AddressDto Address { get; set; }

        public string FullName { get; set; }
        public string FullAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public string Status { get; set; } = QuoteStatus.StatusSubmitted; // e.g., see QuoteStatus

        public List<QuoteItemDto> Items { get; set; } = new();

        // Currency code (e.g., USD, EUR, GBP)
        public string Currency { get; set; } = "EUR";

        // Global discount applied to the whole quote (in percentage, e.g., 0.10 for 10%)
        public decimal GlobalDiscountRate { get; set; } = 0;

        public decimal? Subtotal => Items.Sum(i => i.TotalAfterDiscountAndTax);

        public decimal? DiscountAmount => Subtotal * GlobalDiscountRate;

        public decimal TaxRate { get; set; } = 0; // Example: 20%
        public decimal? TaxAmount => (Subtotal - DiscountAmount) * TaxRate;

        public decimal? Total => (Subtotal - DiscountAmount) + TaxAmount;

        public string? Notes { get; set; }
    }
}
