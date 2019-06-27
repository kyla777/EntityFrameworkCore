using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickReach.ECommerce.Infra.Data.EntityConfiguration
{
    public class CategoryRollUpEntityTypeConfiguration : IEntityTypeConfiguration<CategoryRollUp>
    {
        public void Configure(EntityTypeBuilder<CategoryRollUp> builder)
        {
            builder.HasKey(cr => new { cr.ParentCategoryID, cr.ChildCategoryID });

            builder.HasOne(cr => cr.ParentCategory)
                   .WithMany(c => c.ChildCategories)
                   .HasForeignKey("ParentCategoryID")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cr => cr.ChildCategory)
                   .WithMany(c => c.ParentCategories)
                   .HasForeignKey("ChildCategoryID")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
