using ApotekHjartat.Order.DbAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ApotekHjartat.Order.DbAccess.Setup
{
    public class OrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
    {
        public IConfiguration Configuration { get; }

        public OrderDbContextFactory()
        {
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string settingsEx = (env != null && env != string.Empty) ? "." + env : string.Empty;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine($"{System.IO.Directory.GetCurrentDirectory()}", $"../ApotekHjartat.Order.Api/appsettings{settingsEx}.json"), optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public OrderDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<OrderDbContext> builder = new DbContextOptionsBuilder<OrderDbContext>();

            builder.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions => sqlOptions.MigrationsAssembly("ApotekHjartat.Order.DbAccess"));

            return new OrderDbContext(builder.Options);
        }
    }
}