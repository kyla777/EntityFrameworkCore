using Microsoft.EntityFrameworkCore;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickReach.ECommerce.Infra.Data.Repositories
{
    public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
    {
        public SupplierRepository(ECommerceDbContext context) : base(context)
        {
        }

        public override Supplier Retrieve(int entityId)
        {
            var entity = this.context.Suppliers
                                .Include(s => s.ProductSuppliers)
                                .Where(s => s.ID == entityId)
                                .FirstOrDefault();
            return entity;
        }

        public IEnumerable<Supplier> Retrieve(string search = "", int skip = 0, int count = 10)
        {
            var results = this.context.Suppliers
                                .AsNoTracking()
                                .Where(s => s.Name.Contains(search) || s.Description.Contains(search))
                                .Skip(skip)
                                .Take(count)
                                .ToList();
            return results;
        }
    }
}
