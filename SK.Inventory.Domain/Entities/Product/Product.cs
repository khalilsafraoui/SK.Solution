﻿namespace SK.Inventory.Domain.Entities.Product
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? SpecialTag { get; set; }

        public int CategoryId { get; set; }
        
        public Category Category { get; set; }

        public string? ImageUrl { get; set; }
    }
}
