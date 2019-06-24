using QuickReach.ECommerce.Domain;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickReach.ECommerce.Infra.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IRepository<Product>
    {
        // :base(context) calls the repository base class
        public ProductRepository(ECommerceDbContext context) : base(context)
        {

        }
    }
}
