using SK.CRM.Application.Dtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace SK.CRM.Application.DTOs
{
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public ProductDto Product { get; set; }

        public int Count { get; set; }

    }
}
