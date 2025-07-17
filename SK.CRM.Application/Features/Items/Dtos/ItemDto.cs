namespace SK.CRM.Application.Features.Items.Dtos
{
    public class ItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public double DiscountRate { get; set; } = 0;

        public double TaxRate { get; set; } = 0;

    }
}
