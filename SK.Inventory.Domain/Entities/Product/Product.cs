using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK.Inventory.Domain.Entities.Product
{
    public class Product : BaseEntity
    {
        [Required(ErrorMessage = "Please entre name..")]
        public string Name { get; set; }

        [Range(0.01, 1000, ErrorMessage = "Please enter valid price..")]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? SpecialTag { get; set; }

        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public string? ImageUrl { get; set; }
    }
}
