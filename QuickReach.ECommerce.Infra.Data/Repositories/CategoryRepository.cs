using Microsoft.EntityFrameworkCore;
using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickReach.ECommerce.Infra.Data.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(ECommerceDbContext context) : base(context)
        {

        }

        public override void Delete(int entityId)
        {
            var products = this.context.Products
                                    .Where(p => p.CategoryID == entityId)
                                    .ToList();

            if(products.Count() > 0)
            {
                throw new SystemException("Category contains products.");
            }

            //if(entity.Products.Count() != 0 || entity.Products.Count() != null)
            //{
            //    throw new SystemException("Category contains products.");
            //}

            var entity = this.context.Categories
                                .Where(c => c.ID == entityId)
                                .FirstOrDefault();

            this.context.Remove(entity);
            this.context.SaveChanges();
        }

        public override Category Retrieve(int entityId)
        {
            var entity = this.context.Categories
                                .AsNoTracking()
                                .Include(c => c.Products)
                                .Where(c => c.ID == entityId)
                                .FirstOrDefault();
            return entity;
        }

        public IEnumerable<Category> Retrieve(string search = "", int skip = 0, int count = 0)
        {
            var result = this.context.Categories
                                .AsNoTracking()
                                .Where(c => c.Name.Contains(search) || c.Description.Contains(search))
                                .Skip(skip)
                                .Take(count)
                                .ToList();
            return result;
        }
    }
}
