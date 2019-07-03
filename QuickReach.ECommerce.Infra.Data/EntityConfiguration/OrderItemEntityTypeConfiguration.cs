using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickReach.ECommerce.Infra.Data.EntityConfiguration
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(oi => oi.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.Items)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
