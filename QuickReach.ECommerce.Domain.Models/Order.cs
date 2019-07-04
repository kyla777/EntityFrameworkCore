using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace QuickReach.ECommerce.Domain.Models
{
    [Table("Order")]
    public class Order : EntityBase
    {
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> Items { get; set; }
        public Cart Cart { get; set; }
        public int CartId { get; set; }
        public Order(int customerId)
        {
            CustomerId = customerId;
            Items = new List<OrderItem>();
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            ((ICollection<OrderItem>)this.Items).Add(orderItem);
        }

        public void RemoveOrderItem(int orderItemId)
        {
            var orderItem = this.Items.Where(oi => oi.Id == orderItemId)
                                     .FirstOrDefault();

            ((ICollection<OrderItem>)this.Items).Remove(orderItem);
        }
    }
}
