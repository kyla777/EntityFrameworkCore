﻿using QuickReach.ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickReach.Ecommerce.Infra.Data.Tests.Utilities
{
    static class SampleEntityHelper
    {
        public static Category SampleCategory()
        {
            Category category = new Category
            {
                Name = "Shoes",
                Description = "Shoes Department"
            };

            return category;
        }

        public static Product SampleProduct(int categoryID)
        {
            Product product = new Product
            {
                Name = "Rubber Shoes",
                Description = "This is a pair of rubber shoes.",
                Price = 2000,
                CategoryID = categoryID,
                ImageUrl = "rubbershoes.jpg"
            };

            return product;
        }

        public static Supplier SampleSupplier()
        {
            Supplier supplier = new Supplier()
            {
                Name = "Converse",
                Description = "Converse supplier",
                IsActive = true
            };

            return supplier;
        }
    }
}
