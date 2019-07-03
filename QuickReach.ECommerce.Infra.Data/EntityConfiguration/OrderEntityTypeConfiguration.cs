using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickReach.ECommerce.Infra.Data.EntityConfiguration
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.ID)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.HasMany(o => o.Items)
                   .WithOne(o => o.Order)
                   .HasForeignKey("OrderID")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Cart);
        }
    }
}
