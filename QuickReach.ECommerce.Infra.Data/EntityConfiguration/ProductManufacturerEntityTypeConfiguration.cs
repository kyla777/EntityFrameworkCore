using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickReach.ECommerce.Infra.Data.EntityConfiguration
{
    public class ProductManufacturerEntityTypeConfiguration : IEntityTypeConfiguration<ProductManufacturer>
    {
        public void Configure(EntityTypeBuilder<ProductManufacturer> builder)
        {
            builder.HasKey(pm => new { pm.ManufacturerID, pm.ProductID });

            builder.HasOne(pm => pm.Manufacturer)
                   .WithMany(m => m.ProductManufacturers)
                   .HasForeignKey("ManufacturerID");
            builder.HasOne(pm => pm.Product)
                   .WithMany(p => p.ProductManufacturers)
                   .HasForeignKey("ProductID");
        }
    }
}
