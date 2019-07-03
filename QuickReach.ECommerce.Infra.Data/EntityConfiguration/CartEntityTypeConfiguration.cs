﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickReach.ECommerce.Infra.Data.EntityConfiguration
{
    public class CartEntityTypeConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.Property(c => c.ID)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.HasMany(c => c.Items)
                   .WithOne(c => c.Cart)
                   .HasForeignKey("CartID")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
