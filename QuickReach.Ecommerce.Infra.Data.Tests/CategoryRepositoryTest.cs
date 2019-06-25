using Microsoft.EntityFrameworkCore;
using QuickReach.ECommerce.Domain.Models;
using QuickReach.ECommerce.Infra.Data;
using QuickReach.ECommerce.Infra.Data.Repositories;
using System.Collections;
using System.Linq;
using System;
using Xunit;
using Microsoft.Data.Sqlite;

namespace QuickReach.Ecommerce.Infra.Data.Tests
{
    public class CategoryRespositoryTest
    {
        [Fact]
        public void Create_WithValidEntity_ShouldCreateDatabaseRecord()
        {
            // Arrange
            var connectionBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };
            var connection = new SqliteConnection(connectionBuilder.ConnectionString);

            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                                .UseSqlite(connection)
                                .Options;

            Category category;

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                category = new Category
                {
                    Name = "Shoes",
                    Description = "Shoes Department"
                };

                var sut = new CategoryRepository(context);

                // Act
                sut.Create(category);
            }

            using (var context = new ECommerceDbContext(options))
            {
                // Assert
                var actual = context.Categories.Find(category.ID);

                Assert.NotNull(actual);
                Assert.Equal(category.Name, actual.Name);
                Assert.Equal(category.Description, actual.Description);
            }
        }

        [Fact]
        public void Delete_WithValidEntityId_ShouldRemoveRecordInDatabase()
        {
            // Arrange
            var connectionBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };
            var connection = new SqliteConnection(connectionBuilder.ConnectionString);

            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                                .UseSqlite(connection)
                                .Options;

            Category category;

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                category = new Category
                {
                    Name = "Shoes",
                    Description = "Shoes Department"
                };

                context.Categories.Add(category);

                var sut = new CategoryRepository(context);

                // Act
                sut.Delete(category.ID);
            }

            using (var context = new ECommerceDbContext(options))
            {
                // Assert
                var actual = context.Categories.Find(category.ID);

                Assert.Null(actual);
            }
        }

        [Fact]
        public void Retrieve_WithValidEntityId_ReturnsAValidEntity()
        {
            // Arrange
            var connectionBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };
            var connection = new SqliteConnection(connectionBuilder.ConnectionString);

            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                                .UseSqlite(connection)
                                .Options;

            Category category;
            var expectedName = "Shoes";
            var expectedDescription = "Shoes Department";

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                category = new Category
                {
                    Name = expectedName,
                    Description = expectedDescription
                };

                context.Categories.Add(category);

                context.SaveChanges();
            }

            using (var context = new ECommerceDbContext(options))
            {
                var sut = new CategoryRepository(context);

                // Act
                var actual = sut.Retrieve(category.ID);

                // Assert
                Assert.NotNull(actual);
                Assert.Equal(expectedName, actual.Name);
                Assert.Equal(expectedDescription, actual.Description);
            }
        }

        [Fact]
        public void Retrieve_WithNonExistingEntityID_ReturnsNull()
        {
            // Arrange
            var connectionBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };
            var connection = new SqliteConnection(connectionBuilder.ConnectionString);
            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                                .UseSqlite(connection)
                                .Options;

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                var sut = new CategoryRepository(context);

                // Act
                var actual = sut.Retrieve(-1);

                // Assert
                Assert.Null(actual);
            }
        }

        [Fact]
        public void Retrieve_WithSkipAndCount_ReturnsTheCorrectPage()
        {
            // Arrange
            var connectionBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };
            var connection = new SqliteConnection(connectionBuilder.ConnectionString);

            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                                .UseSqlite(connection)
                                .Options;

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                for (var i = 1; i <= 10; i += 1)
                {
                    context.Categories.Add(new Category
                    {
                        Name = string.Format("Category {0}", i),
                        Description = string.Format("Description {0}", i)
                    });
                }

                context.SaveChanges();
            }

            using (var context = new ECommerceDbContext(options))
            {
                var sut = new CategoryRepository(context);
                var expectCount = 5;

                // Act
                var list = sut.Retrieve(0 , expectCount);

                // Assert
                Assert.True(list.Count() == expectCount);
            }
        }

        [Fact]
        public void Update_WithValidEntity_ShouldUpdateDatabaseRecord()
        {
            // Arrange
            var connectionBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };
            var connection = new SqliteConnection(connectionBuilder.ConnectionString);

            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                                .UseSqlite(connection)
                                .Options;

            var expectedName = "Shoes";
            var expectedDescription = "Shoes Department";
            int? expectedId;

            using (var context = new ECommerceDbContext(options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                Category category = new Category
                {
                    Name = expectedName,
                    Description = expectedDescription
                };

                context.Categories.Add(category);
                context.SaveChanges();

                expectedId = category.ID;
            }

            using (var context = new ECommerceDbContext(options))
            {
                // Arrange
                var category = context.Categories.Find(expectedId);
                category.Name = expectedName;
                category.Description = expectedDescription;

                var sut = new CategoryRepository(context);

                // Act
                sut.Update(category.ID, category);
                var actual = context.Categories.Find(category.ID);

                // Assert
                Assert.NotNull(actual);
                Assert.Equal(expectedName, actual.Name);
                Assert.Equal(expectedDescription, actual.Description);
            }
        }
    }
}
