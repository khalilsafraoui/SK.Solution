﻿using System.ComponentModel.DataAnnotations;

namespace SK.CRM.Domain.Entities
{
    public class Order : BaseEntity
    {
       
        public string UserId { get; set; }

        public Guid? CustomerId { get; set; }

        public Customer Customer { get; set; }

        public Guid? AddressId { get; set; }

        public Address Address { get; set; }

        [Required]
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Order Total")]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string? SessionId { get; set; }

        public string? PaymentIntentId { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
