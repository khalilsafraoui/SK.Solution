using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SK.Inventory.Application.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please entre name..")]
        public string Name { get; set; }

        [Range(0.01, 1000, ErrorMessage = "Please enter valid price..")]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? SpecialTag { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryDto Category { get; set; }

        public string? ImageUrl { get; set; }
    }
}
