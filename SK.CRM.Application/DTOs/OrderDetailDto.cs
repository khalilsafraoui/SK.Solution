using SK.CRM.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace SK.CRM.Application.DTOs
{
    public class OrderDetailDto
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public OrderDto Order { get; set; }

        public int ProductId { get; set; }

        public ProductDto Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ProductName { get; set; }

        public decimal DiscountRate { get; set; } = 0;

        public decimal TaxRate { get; set; } = 0;

        public decimal? TotalBeforeDiscount => Quantity * Price;

        public decimal? DiscountAmount => TotalBeforeDiscount * DiscountRate;

        public decimal? TotalAfterDiscount => TotalBeforeDiscount - DiscountAmount;

        public decimal? TaxAmount => TotalAfterDiscount * TaxRate;

        public decimal? TotalAfterDiscountAndTax => TotalAfterDiscount + TaxAmount;
    }
}
