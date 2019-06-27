using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using QuickReach.ECommerce.Infra.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickReach.Ecommerce.Infra.Data.Tests.Utilities
{
    static class ConnectionOptionHelper
    {
        public static DbContextOptions<ECommerceDbContext> Sqlite()
        {
            var connectionBuilder = new SqliteConnectionStringBuilder()
            {
                DataSource = ":memory:"
            };
            var connection = new SqliteConnection(connectionBuilder.ConnectionString);

            var options = new DbContextOptionsBuilder<ECommerceDbContext>()
                                .UseSqlite(connection)
                                .Options;
            return options;
        }
    }
}
