namespace SK.CRM.Application.Features.Items.Dtos
{
    public class ExtensionItemDto : ItemDto
    {
        public Guid ExtensionItemId { get; set; }
        public int Quantity { get; set; } = 1;
        public double? TotalBeforeDiscount => Quantity * (double)Price;

        public double? DiscountAmount => TotalBeforeDiscount * DiscountRate;

        public double? TotalAfterDiscount => TotalBeforeDiscount - DiscountAmount;

        public double? TaxAmount => TotalAfterDiscount * TaxRate;

        public double? TotalAfterDiscountAndTax => TotalAfterDiscount + TaxAmount;
    }
}
