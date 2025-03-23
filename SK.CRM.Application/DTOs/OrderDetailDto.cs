using SK.CRM.Application.Dtos;
using System.ComponentModel.DataAnnotations;

namespace SK.CRM.Application.DTOs
{
    public class OrderDetailDto
    {
        public Guid Id { get; set; }

        public int OrderId { get; set; }

        public OrderDto Order { get; set; }

        public int ProductId { get; set; }

        public ProductDto Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string ProductName { get; set; }
    }
}
