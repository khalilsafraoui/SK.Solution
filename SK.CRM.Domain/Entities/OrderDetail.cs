﻿using System.ComponentModel.DataAnnotations;

namespace SK.CRM.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public string ProductName { get; set; }
    }
}
