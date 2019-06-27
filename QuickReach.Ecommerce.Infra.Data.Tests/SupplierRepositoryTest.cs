using System;
using System.Linq;
using Xunit;
using QuickReach.ECommerce.Domain.Models;
using QuickReach.ECommerce.Infra.Data;
using QuickReach.ECommerce.Infra.Data.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using QuickReach.Ecommerce.Infra.Data.Tests.Utilities;

namespace QuickReach.Ecommerce.Infra.Data.Tests
{
    public class SupplierRepositoryTest
    {
        [Fact]
        public void Create_WithValidEntity_ShouldCreateDatabaseRecord()
        {
            // Arrange
            var options = ConnectionOptionHelper.Sqlite();

            Supplier supplier;

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                supplier = SampleEntityHelper.SampleSupplier();

                var sut = new SupplierRepository(context);

                // Act
                sut.Create(supplier);
            }

            using (var context = new ECommerceDbContext(options))
            {
                // Assert
                var actual = context.Suppliers.Find(supplier.ID);

                Assert.NotNull(actual);
                Assert.Equal(supplier.Name, actual.Name);
                Assert.Equal(supplier.Description, actual.Description);
            }
        }

        [Fact]
        public void Delete_WithValidEntityID_ShouldRemoveDatabaseRecord()
        {
            // Arrange
            var options = ConnectionOptionHelper.Sqlite();

            Supplier supplier;

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                supplier = SampleEntityHelper.SampleSupplier();

                context.Suppliers.Add(supplier);

                context.SaveChanges();

                var sut = new SupplierRepository(context);

                // Act
                sut.Delete(supplier.ID);
            }

            using (var context = new ECommerceDbContext(options))
            {
                // Assert
                var actual = context.Suppliers.Find(supplier.ID);

                Assert.Null(actual);
            }
        }

        [Fact]
        public void Retrieve_WithValidEntityID_ShouldReturnAValidSupplier()
        {
            // Arrange
            var options = ConnectionOptionHelper.Sqlite();

            Supplier supplier;
            var expectedName = "Converse";
            var expectedDescription = "Converse supplier";

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                supplier = new Supplier()
                {
                    Name = expectedName,
                    Description = expectedDescription,
                    IsActive = true
                };

                context.Suppliers.Add(supplier);

                context.SaveChanges();
            }

            using (var context = new ECommerceDbContext(options))
            {
                var sut = new SupplierRepository(context);

                // Act
                var actual = sut.Retrieve(supplier.ID);

                // Assert
                Assert.NotNull(actual);
                Assert.Equal(expectedName, actual.Name);
                Assert.Equal(expectedDescription, actual.Description);
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

                var sut = new SupplierRepository(context);

                // Act
                var actual = sut.Retrieve(-1);

                // Assert
                Assert.Null(actual);
            }
        }

        [Fact]
        public void Retrieve_WithSkipAndCount_ShouldReturnTheCorrectRecords()
        {
            // Arrange
            var options = ConnectionOptionHelper.Sqlite();

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                for (var i = 1; i <= 10; i += 1)
                {
                    context.Suppliers.Add(new Supplier
                    {
                        Name = string.Format("Supplier {0}", i),
                        Description = string.Format("Supplier Description {0}.", i),
                        IsActive = true
                    });
                }

                context.SaveChanges();
            }

            using (var context = new ECommerceDbContext(options))
            {
                var sut = new SupplierRepository(context);
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

            var expectedName = "Nike";
            var expectedDescription = "Nike supplier";
            int? expectedId;

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                Supplier supplier = SampleEntityHelper.SampleSupplier();

                context.Suppliers.Add(supplier);
                context.SaveChanges();

                expectedId = supplier.ID;
            }

            using (var context = new ECommerceDbContext(options))
            {
                // Arrange
                var supplier = context.Suppliers.Find(expectedId);
                supplier.Name = expectedName;
                supplier.Description = expectedDescription;

                var sut = new SupplierRepository(context);

                // Act
                sut.Update(supplier.ID, supplier);
                var actual = context.Suppliers.Find(supplier.ID);

                // Assert
                Assert.NotNull(actual);
                Assert.Equal(expectedName, actual.Name);
                Assert.Equal(expectedDescription, actual.Description);
            }
        }
    }
}
