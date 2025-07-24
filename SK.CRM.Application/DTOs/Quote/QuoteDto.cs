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
        public double GlobalDiscountRate { get; set; } = 0;

        public double? Subtotal => Items.Sum(i => i.TotalAfterDiscountAndTax);

        public double? DiscountAmount => Subtotal * GlobalDiscountRate;

        public double TaxRate { get; set; } = 0; // Example: 20%
        public double? TaxAmount => (Subtotal - DiscountAmount) * TaxRate;

        public double? Total => (Subtotal - DiscountAmount) + TaxAmount;

        public string? Notes { get; set; }
    }
}
