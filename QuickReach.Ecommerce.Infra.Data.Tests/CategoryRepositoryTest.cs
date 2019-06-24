using Microsoft.EntityFrameworkCore;
using QuickReach.ECommerce.Domain.Models;
using QuickReach.ECommerce.Infra.Data;
using QuickReach.ECommerce.Infra.Data.Repositories;
using System.Collections;
using System.Linq;
using System;
using Xunit;

namespace QuickReach.Ecommerce.Infra.Data.Tests
{
    public class CategoryRespositoryTest
    {
        [Fact]
        public void Create_WithValidEntity_ShouldCreateDatabaseRecord()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new CategoryRepository(context);
            var category = new Category
            {
                Name = "Shoes",
                Description = "Shoes Department"
            };

            // Act
            sut.Create(category);

            // Assert
            Assert.True(category.ID != 0);

            var entity = sut.Retrieve(category.ID);
            Assert.NotNull(entity);

            // Cleanup
            sut.Delete(category.ID);
        }

        [Fact]
        public void Retrieve_WithValidEntityId_ReturnsAValidEntity()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var category = new Category
            {
                Name = "Shoes",
                Description = "Shoes Department"
            };
            var sut = new CategoryRepository(context);
            sut.Create(category);

            // Act
            var actual = sut.Retrieve(category.ID);

            // Assert
            Assert.NotNull(actual);

            // Cleanup
            sut.Delete(actual.ID);
        }

        [Fact]
        public void Retrieve_WithNonExistingEntityID_ReturnsNull()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new CategoryRepository(context);

            // Act
            var actual = sut.Retrieve(-1);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void Retrieve_WithSkipAndCount_ReturnsTheCorrectPage()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new CategoryRepository(context);
            for(var i = 1; i <= 20; i += 1)
            {
                sut.Create(new Category
                {
                    Name = string.Format("Category {0}", i),
                    Description = string.Format("Description {0}", i)
                });
            }

            // Act
            var list = sut.Retrieve(5, 5);

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
        public void Delete_WithValidEntityId_ShouldRemoveRecordInDatabase()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new CategoryRepository(context);
            var category = new Category()
            {
                Name = "Shoes",
                Description = "Shoes Department"
            };
            sut.Create(category);
            var entity = sut.Retrieve(category.ID);

            // Act
            Assert.True(entity.ID != 0);
            sut.Delete(entity.ID);
            var actual = sut.Retrieve(entity.ID);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void Update_WithValidEntity_ShouldUpdateDatabaseRecord()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new CategoryRepository(context);
            var category = new Category
            {
                Name = "Shoes",
                Description = "Shoes department"
            };

            sut.Create(category);

            var expectedName = "New Shoes";
            var expectedDescription = "New Shoes department";

            category.Name = expectedName;
            category.Description = expectedDescription;

            // Act
            sut.Update(category.ID, category);

            // Assert
            var actual = sut.Retrieve(category.ID);

            Assert.Equal(expectedName, actual.Name);
            Assert.Equal(expectedDescription, actual.Description);

            // Cleanup
            sut.Delete(actual.ID);
        }
    }
}
