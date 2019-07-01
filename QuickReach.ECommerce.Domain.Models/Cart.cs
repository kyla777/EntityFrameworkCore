using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QuickReach.ECommerce.Domain.Models
{
    [Table("Cart")]
    public class Cart : EntityBase

    {
        public int CustomerId { get; set; }
        public List<CartItem> Items { get; set; }
        public Cart(int customerId)
        {
            CustomerId = customerId;
            Items = new List<CartItem>();
        }
    }
}
