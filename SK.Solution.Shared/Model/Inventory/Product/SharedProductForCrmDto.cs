namespace SK.Solution.Shared.Model.Inventory.Product
{
    public class SharedProductForCrmDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public decimal DiscountRate { get; set; } = 0;

        public decimal TaxRate { get; set; } = 0;
    }
}
