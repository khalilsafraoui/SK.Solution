using SK.CRM.Application.Dtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK.CRM.Application.DTOs
{
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public ProductDto Product { get; set; }

        public int Count { get; set; }

        public int newCount { get; set; }
        public bool IsEditing { get; set; } = false;

        // Computed field: unit price after discount and tax
        public double? TotalBeforeDiscount => Count * Convert.ToDouble(Product.Price);

        public double? DiscountAmount => TotalBeforeDiscount * Product.DiscountRate;

        public double? TotalAfterDiscount => TotalBeforeDiscount - DiscountAmount;

        public double? TaxAmount => TotalAfterDiscount * Product.TaxRate;

        public double? TotalAfterDiscountAndTax => TotalAfterDiscount + TaxAmount;

    }
}
