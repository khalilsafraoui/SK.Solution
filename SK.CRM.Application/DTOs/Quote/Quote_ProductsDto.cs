namespace SK.CRM.Application.DTOs.Quote
{
    public class Quote_ProductsDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public decimal DiscountRate { get; set; } = 0;

        public decimal TaxRate { get; set; } = 0;
    }
}
