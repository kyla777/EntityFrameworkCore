using Microsoft.EntityFrameworkCore;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickReach.ECommerce.Infra.Data.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(ECommerceDbContext context) : base(context)
        {
        }

        public override Customer Retrieve(int entityId)
        {
            var customer = this.context.Customers
                                       .Include(c => c.Carts)
                                       .Where(c => c.ID == entityId)
                                       .FirstOrDefault();
            return customer;
        }

        public IEnumerable<Customer> Retrieve(string search = "", int skip = 0, int count = 0)
        {
            var result = this.context.Customers
                                .AsNoTracking()
                                .Where(c => c.CardHolderName.Contains(search) ||
                                       c.Street.Contains(search) ||
                                       c.City.Contains(search) ||
                                       c.State.Contains(search))
                                .Skip(skip)
                                .Take(count)
                                .ToList();
            return result;
        }
    }
}
