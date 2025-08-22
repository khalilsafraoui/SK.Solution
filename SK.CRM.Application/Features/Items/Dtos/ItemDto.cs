namespace SK.CRM.Application.Features.Items.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public decimal DiscountRate { get; set; } = 0;

        public decimal TaxRate { get; set; } = 0;

    }
}
