namespace SK.Solution.Shared.Model.Inventory.Product
{
    public class SharedProductForCrmDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public double DiscountRate { get; set; } = 0;

        public double TaxRate { get; set; } = 0;
    }
}
