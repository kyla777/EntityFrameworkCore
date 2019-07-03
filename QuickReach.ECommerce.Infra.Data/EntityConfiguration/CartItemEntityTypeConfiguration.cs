﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickReach.ECommerce.Infra.Data.EntityConfiguration
{
    public class CartItemEntityTypeConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.Property(ci => ci.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.HasOne(ci => ci.Cart)
                   .WithMany(c => c.Items)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
