using System;
using System.Linq;
using Xunit;
using QuickReach.ECommerce.Domain.Models;
using QuickReach.ECommerce.Infra.Data;
using QuickReach.ECommerce.Infra.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using QuickReach.Ecommerce.Infra.Data.Tests.Utilities;
using System.Collections.Generic;

namespace QuickReach.Ecommerce.Infra.Data.Tests
{
    public class ProductRepositoryTest
    {
        [Fact]
        public void Create_WithValidEntity_ShouldCreateDatabaseRecord()
        {
            // Arrange
            var options = ConnectionOptionHelper.Sqlite();

            Category category;
            Product product;

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                // Create Category
                category = SampleEntityHelper.SampleCategory();
                context.Categories.Add(category);

                context.SaveChanges();

                // Product Object
                product = SampleEntityHelper.SampleProduct();

                //ProductCategory productCategory = new ProductCategory()
                //{
                //    ProductID = product.ID,
                //    Product = product,
                //    CategoryID = category.ID,
                //    Category = category
                //};

                //List<ProductCategory> productCategories = new List<ProductCategory>();
                //productCategories.Add(productCategory);

                //product.ProductCategories = productCategories;

                var sut = new ProductRepository(context);

                // Act
                sut.Create(product);
            }

            using (var context = new ECommerceDbContext(options))
            {
                // Assert
                var actual = context.Products.Find(product.ID);

                Assert.NotNull(actual);
                Assert.Equal(product.Name, actual.Name);
                Assert.Equal(product.Description, actual.Description);
                Assert.Equal(product.Price, actual.Price);
                Assert.Equal(product.ImageUrl, actual.ImageUrl);
            }
        }

        [Fact]
        public void Delete_WithValidEntityID_ShouldRemoveDatabaseRecord()
        {
            // Arrange
            var options = ConnectionOptionHelper.Sqlite();

            Category category;
            Product product;

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var sut = new ProductRepository(context);

                // Create category
                category = SampleEntityHelper.SampleCategory();
                context.Categories.Add(category);

                // New product object
                product = SampleEntityHelper.SampleProduct();

                context.Products.Add(product);

                //ProductCategory productCategory = new ProductCategory()
                //{
                //    ProductID = product.ID,
                //    Product = product,
                //    CategoryID = category.ID,
                //    Category = category
                //};

                //List<ProductCategory> productCategories = new List<ProductCategory>();
                //productCategories.Add(productCategory);

                //product.ProductCategories = productCategories;

                context.SaveChanges();

                // Act
                sut.Delete(product.ID);
            }

            using (var context = new ECommerceDbContext(options))
            {
                // Assert
                var actual = context.Products.Find(product.ID);

                Assert.Null(actual);
            }
        }
        
        [Fact]
        public void Retrieve_WithValidEntityID_ShouldReturnAValidProduct()
        {
            // Arrange
            var options = ConnectionOptionHelper.Sqlite();

            Product product;

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                // Create Category
                Category category = SampleEntityHelper.SampleCategory();
                context.Categories.Add(category);

                // Product object
                product = SampleEntityHelper.SampleProduct();

                //ProductCategory productCategory = new ProductCategory()
                //{
                //    ProductID = product.ID,
                //    Product = product,
                //    CategoryID = category.ID,
                //    Category = category
                //};

                //List<ProductCategory> productCategories = new List<ProductCategory>();
                //productCategories.Add(productCategory);

                //product.ProductCategories = productCategories;

                context.Products.Add(product);
                context.SaveChanges();
            }

            using (var context = new ECommerceDbContext(options))
            {
                var sut = new ProductRepository(context);

                // Act
                var actual = sut.Retrieve(product.ID);

                // Assert
                Assert.NotNull(actual);
            }
        }

        [Fact]
        public void Retrieve_WithNonExistingEntityID_ShouldReturnNull()
        {
            // Arrange
            var options = ConnectionOptionHelper.Sqlite();

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var sut = new ProductRepository(context);

                // Act
                var actual = sut.Retrieve(-1);

                // Assert
                Assert.Null(actual);
            }
        }

        [Fact]
        public void Retrieve_WithSkipAndAccount_ShouldReturnTheCorrectRecords()
        {
            // Arrange
            var options = ConnectionOptionHelper.Sqlite();

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                // Create Category
                Category category = SampleEntityHelper.SampleCategory();
                context.Categories.Add(category);

                // Add Product Objects
                for (var i = 1; i <= 10; i += 1)
                {
                    Product product = new Product
                    {
                        Name = string.Format("Rubber Shoes {0}", i),
                        Description = string.Format("This is a pair of rubber shoes {0}.", i),
                        Price = 2000,
                        ImageUrl = string.Format("rubbershoes{0}.jpg", i)
                    };

                    //ProductCategory productCategory = new ProductCategory()
                    //{
                    //    ProductID = product.ID,
                    //    Product = product,
                    //    CategoryID = category.ID,
                    //    Category = category
                    //};

                    //List<ProductCategory> productCategories = new List<ProductCategory>();
                    //productCategories.Add(productCategory);

                    //product.ProductCategories = productCategories;

                    context.Products.Add(product);
                }

                context.SaveChanges();
            }

            using (var context = new ECommerceDbContext(options))
            {
                var sut = new ProductRepository(context);
                var expectCount = 5;

                // Act
                var list = sut.Retrieve(0, expectCount);

                // Assert
                Assert.True(list.Count() == expectCount);
            }
        }

        [Fact]
        public void Update_WithValidEntity_ShouldUpdateDatabaseRecord()
        {
            // Arrange
            var options = ConnectionOptionHelper.Sqlite();

            var expectedName = "Leather Shoes";
            var expectedDescription = "This is a pair of leather shoes.";
            var expectedImageUrl = "leathershoes.jpg";
            int? expectedId;

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                Category category = SampleEntityHelper.SampleCategory();
                context.Categories.Add(category);

                Product product = SampleEntityHelper.SampleProduct();
                context.Products.Add(product);

                //ProductCategory productCategory = new ProductCategory()
                //{
                //    ProductID = product.ID,
                //    Product = product,
                //    CategoryID = category.ID,
                //    Category = category
                //};

                //List<ProductCategory> productCategories = new List<ProductCategory>();
                //productCategories.Add(productCategory);

                //product.ProductCategories = productCategories;

                context.SaveChanges();

                expectedId = product.ID;
            }

            using (var context = new ECommerceDbContext(options))
            {
                // Arrange
                var product = context.Products.Find(expectedId);
                product.Name = expectedName;
                product.Description = expectedDescription;
                product.ImageUrl = expectedImageUrl;

                var sut = new ProductRepository(context);

                // Act
                sut.Update(product.ID, product);
                var actual = context.Products.Find(product.ID);

                // Assert
                Assert.Equal(expectedName, actual.Name);
                Assert.Equal(expectedDescription, actual.Description);
                Assert.Equal(expectedImageUrl, actual.ImageUrl);
            }
        }

        #region CreateShouldThrowAnException
        //[Fact]
        //public void Create_WithNonExistingCategory_ShouldThrowAnException()
        //{
        //    var options = ConnectionOptionHelper.Sqlite();

        //    Product product;

        //    using (var context = new ECommerceDbContext(options))
        //    {
        //        context.Database.OpenConnection();
        //        context.Database.EnsureCreated();

        //        product = new Product
        //        {
        //            Name = "Nike",
        //            Description = "Nike Product",
        //            ImageUrl = "nike.jpg"
        //        };

        //        var sut = new ProductRepository(context);

        //        // Act & Assert
        //        Assert.Throws<DbUpdateException>(() => sut.Create(product));
        //    }
        //} 
        #endregion
    }
}
