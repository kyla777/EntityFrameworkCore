using Microsoft.EntityFrameworkCore;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickReach.ECommerce.Infra.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        // :base(context) calls the repository base class
        public ProductRepository(ECommerceDbContext context) : base(context)
        {

        }

        public override Product Create(Product newEntity)
        {
            var category = this.context.Categories
                                .Where(c => c.ID == newEntity.CategoryID)
                                .FirstOrDefault();

            if(category == null)
            {
                throw new SystemException("Category does not exist.");
            }

            this.context.Products.Add(newEntity);
            this.context.SaveChanges();
            return newEntity;
        }

        public IEnumerable<Product> Retrieve(string search = "", int skip = 0, int count = 10)
        {
            var results = this.context.Products
                                .AsNoTracking()
                                .Where(p => p.Name.Contains(search) || p.Description.Contains(search))
                                .Skip(skip)
                                .Take(count)
                                .ToList();
            return results;
        }
    }
}
