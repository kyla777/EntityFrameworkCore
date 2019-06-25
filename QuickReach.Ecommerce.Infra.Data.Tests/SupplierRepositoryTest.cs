using System;
using System.Linq;
using Xunit;
using QuickReach.ECommerce.Domain.Models;
using QuickReach.ECommerce.Infra.Data;
using QuickReach.ECommerce.Infra.Data.Repositories;

namespace QuickReach.Ecommerce.Infra.Data.Tests
{
    public class SupplierRepositoryTest
    {
        [Fact]
        public void Create_WithValidEntity_ShouldCreateDatabaseRecord()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new SupplierRepository(context);
            var supplier = new Supplier()
            {
                Name = "Converse",
                Description = "Converse supplier",
                IsActive = true
            };

            // Act
            sut.Create(supplier);

            // Assert
            var result = sut.Retrieve(supplier.ID);
            Assert.NotNull(result);

            // Cleanup
            sut.Delete(result.ID);
        }

        [Fact]
        public void Delete_WithValidEntityID_ShouldRemoveDatabaseRecord()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new SupplierRepository(context);
            var supplier = new Supplier()
            {
                Name = "Converse",
                Description = "Converse supplier",
                IsActive = true
            };
            sut.Create(supplier);
            var result = sut.Retrieve(supplier.ID);
            Assert.NotNull(result);

            // Act
            sut.Delete(supplier.ID);
            var actual = sut.Retrieve(supplier.ID);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void Retrieve_WithValidEntityID_ShouldReturnAValidSupplier()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new SupplierRepository(context);
            var supplier = new Supplier()
            {
                Name = "Converse",
                Description = "Converse supplier",
                IsActive = true
            };
            sut.Create(supplier);

            // Act
            var actual = sut.Retrieve(supplier.ID);

            // Assert
            Assert.NotNull(actual);

            // Cleanup
            sut.Delete(supplier.ID);
        }

        [Fact]
        public void Retrieve_WithNonExistingEntityID_ShouldReturnNull()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new SupplierRepository(context);

            // Act
            var actual = sut.Retrieve(-1);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public void Retrieve_WithSkipAndCount_ShouldReturnTheCorrectRecords()
        {
            // Arrange
            var context = new ECommerceDbContext();
            var sut = new SupplierRepository(context);
            for (var i = 1; i <= 10; i += 1)
            {
                sut.Create(new Supplier
                {
                    Name = string.Format("Supplier {0}", i),
                    Description = string.Format("Supplier Description {0}.", i),
                    IsActive = true
                });
            }

            // Act
            var list = sut.Retrieve(0, 5);

            // Assert
            Assert.True(list.Count() == 5);

            // Cleanup
            list = context.Suppliers.ToList();
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
            var sut = new SupplierRepository(context);
            var supplier = new Supplier()
            {
                Name = "Converse",
                Description = "Converse supplier",
                IsActive = true
            };
            sut.Create(supplier);

            var expectedName = "Nike";
            var expectedDescription = "Nike supplier";

            supplier.Name = expectedName;
            supplier.Description = expectedDescription;

            // Act
            sut.Update(supplier.ID, supplier);
            var actual = sut.Retrieve(supplier.ID);

            // Assert
            Assert.Equal(expectedName, actual.Name);
            Assert.Equal(expectedDescription, actual.Description);
        }
    }
}
