using System;
using System.Linq;
using Xunit;
using QuickReach.ECommerce.Domain.Models;
using QuickReach.ECommerce.Infra.Data;
using QuickReach.ECommerce.Infra.Data.Repositories;

namespace QuickReach.Ecommerce.Infra.Data.Tests
{
    public class ProductRepositoryTest
    {
        [Fact]
        public void Create_WithValidEntity_ShouldCreateDatabaseRecord()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new ProductRepository(context);
            Product product = new Product
            {
                Name = "Rubber Shoes",
                Description = "This is a pair of rubber shoes.",
                Price = 2000,
                CategoryID = 139,
                ImageUrl = "rubbershoes.jpg"
            };

            // Act
            sut.Create(product);

            // Assert
            var result = sut.Retrieve(product.ID);
            Assert.NotNull(result);

            // Cleanup
            sut.Delete(result.ID);
        }

        [Fact]
        public void Delete_WithValidEntityID_ShouldRemoveDatabaseRecord()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new ProductRepository(context);
            Product product = new Product
            {
                Name = "Rubber Shoes",
                Description = "This is a pair of rubber shoes.",
                Price = 2000,
                CategoryID = 139,
                ImageUrl = "rubbershoes.jpg"
            };
            sut.Create(product);
            var result = sut.Retrieve(product.ID);
            Assert.NotNull(result);

            // Act
            sut.Delete(result.ID);
            var actual = sut.Retrieve(result.ID);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void Retrieve_WithValidEntityID_ShouldReturnAValidProduct()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new ProductRepository(context);
            Product product = new Product
            {
                Name = "Rubber Shoes",
                Description = "This is a pair of rubber shoes.",
                Price = 2000,
                CategoryID = 139,
                ImageUrl = "rubbershoes.jpg"
            };
            sut.Create(product);

            // Act
            var actual = sut.Retrieve(product.ID);

            // Assert
            Assert.NotNull(actual);

            // Cleanup
            sut.Delete(actual.ID);

        }

        [Fact]
        public void Retrieve_WithNonExistingEntityID_ShouldReturnNull()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new ProductRepository(context);

            // Act
            var result = sut.Retrieve(-1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Retrieve_WithSkipAndAccount_ShouldReturnTheCorrectRecords()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new ProductRepository(context);
            for(var i=1; i<=10; i += 1)
            {
                sut.Create(new Product
                {
                    Name = string.Format("Rubber Shoes {0}", i),
                    Description = string.Format("This is a pair of rubber shoes {0}.", i),
                    Price = 2000,
                    CategoryID = 139,
                    ImageUrl = string.Format("rubbershoes{0}.jpg", i)
                });
            }

            // Act
            var list = sut.Retrieve(0, 5);

            // Assert
            Assert.True(list.Count() == 5);

            // Cleanup
            list = sut.Retrieve(0, Int32.MaxValue);
            foreach (var item in list)
            {
                sut.Delete(item.ID);
            }
        }

        [Fact]
        public void Update_WithValidEntity_ShouldUpdateDatabaseRecord()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new ProductRepository(context);
            Product product = new Product
            {
                Name = "Rubber Shoes",
                Description = "This is a pair of rubber shoes.",
                Price = 2000,
                CategoryID = 139,
                ImageUrl = "rubbershoes.jpg"
            };
            sut.Create(product);

            var expectedName = "Leather Shoes";
            var expectedDescription = "This is a pair of leather shoes.";
            var expectedImageUrl = "leathershoes.jpg";

            product.Name = expectedName;
            product.Description = expectedDescription;
            product.ImageUrl = expectedImageUrl;

            // Act
            sut.Update(product.ID, product);
            var actual = sut.Retrieve(product.ID);

            // Assert
            Assert.Equal(expectedName, actual.Name);
            Assert.Equal(expectedDescription, actual.Description);
            Assert.Equal(expectedImageUrl, actual.ImageUrl);

            // Cleanup
            sut.Delete(actual.ID);
        }

    }
}
