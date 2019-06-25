using Microsoft.EntityFrameworkCore;
using QuickReach.ECommerce.Domain.Models;
using System;

namespace QuickReach.ECommerce.Infra.Data
{
    public class ECommerceDbContext : DbContext
    {
        // For every aggregate root create a property here
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {

        }

        public ECommerceDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = "Server=.;Database=QuickReachDb;Integrated Security=true;";
            //optionsBuilder.UseSqlServer(connectionString);
        }

        // Format => public DbSet<Entity> Entities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}
