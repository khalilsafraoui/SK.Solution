using System.ComponentModel.DataAnnotations;

namespace SK.CRM.Domain.Entities
{
    public class Order : BaseEntity
    {
       
        public string UserId { get; set; }

        public Guid? CustomerId { get; set; }

        public Customer Customer { get; set; }

        public Guid? AddressId { get; set; }

        public Address Address { get; set; }

        
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }
        
        public DateTime OrderDate { get; set; }

        
        [Display(Name = "Status")]
        public string Status { get; set; }

       
        [Display(Name = "Name")]
        public string Name { get; set; }

        
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string? SessionId { get; set; }

        public string? PaymentIntentId { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
