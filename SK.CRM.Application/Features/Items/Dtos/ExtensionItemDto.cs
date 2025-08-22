namespace SK.CRM.Application.Features.Items.Dtos
{
    public class ExtensionItemDto : ItemDto
    {
        public Guid ExtensionItemId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal? TotalBeforeDiscount => Quantity * Price;

        public decimal? DiscountAmount => TotalBeforeDiscount * DiscountRate;

        public decimal? TotalAfterDiscount => TotalBeforeDiscount - DiscountAmount;

        public decimal? TaxAmount => TotalAfterDiscount * TaxRate;

        public decimal? TotalAfterDiscountAndTax => TotalAfterDiscount + TaxAmount;
    }
}
