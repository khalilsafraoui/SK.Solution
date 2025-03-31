using System.ComponentModel.DataAnnotations;

namespace SK.CRM.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string ProductName { get; set; }
    }
}
