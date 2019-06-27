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

        public override void Delete(int entityId)
        {
            ProductCategory productCategory = this.context.ProductCategories
                                                    .FirstOrDefault(pc => pc.ProductID == entityId);
            this.context.ProductCategories.Remove(productCategory);
            Product product = this.context.Products
                                    .FirstOrDefault(p => p.ID == entityId);
            this.context.Products.Remove(product);
            this.context.SaveChanges();
        }

        public IEnumerable<Product> Retrieve(string search = "", int skip = 0, int count = 10)
        {
            var results = this.context.Products
                                //.AsNoTracking()
                                .Where(p => p.Name.Contains(search) || p.Description.Contains(search))
                                .Skip(skip)
                                .Take(count)
                                .ToList();
            return results;
        }
    }
}
