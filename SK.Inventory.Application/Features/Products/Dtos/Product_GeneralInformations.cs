using SK.Inventory.Application.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.Inventory.Application.Features.Products.Dtos
{
    public class Product_GeneralInformations
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please entre name..")]
        public string Name { get; set; }

        [Range(0.01, 1000, ErrorMessage = "Please enter valid price..")]
        public decimal Price { get; set; }

        public string? Description { get; set; }

        public string? SpecialTag { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category.")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryDto Category { get; set; }

        public string? ImageUrl { get; set; }

        public decimal DiscountRate { get; set; } = 0;

        public decimal TaxRate { get; set; } = 0;
    }
}
