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

        public Category Create(Category newEntity, string parent)
        {
            var parentCategory = this.context.Categories
                                                .Where(c => c.Name.Contains(parent))
                                                .FirstOrDefault();

            parentCategory.AddChild(newEntity.ID);

            this.context.Set<Category>().Add(newEntity);
            this.context.SaveChanges();
            return newEntity;
        }

        public override Category Retrieve(int entityId)
        {
            var entity = this.context.Categories
                                // .AsNoTracking()
                                .Include(c => c.ProductCategories)
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
