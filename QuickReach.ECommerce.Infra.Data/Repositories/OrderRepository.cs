using Microsoft.EntityFrameworkCore;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickReach.ECommerce.Infra.Data.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(ECommerceDbContext context) : base(context)
        {
        }

        public override Order Retrieve(int entityId)
        {
            var order = this.context.Orders
                                   .Include(o => o.Items)
                                   .Where(o => o.ID == entityId)
                                   .FirstOrDefault();
            return order;
        }
    }
}
