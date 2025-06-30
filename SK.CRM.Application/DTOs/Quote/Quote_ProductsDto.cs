namespace SK.CRM.Application.DTOs.Quote
{
    public class Quote_ProductsDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public double DiscountRate { get; set; } = 0;

        public double TaxRate { get; set; } = 0;
    }
}
