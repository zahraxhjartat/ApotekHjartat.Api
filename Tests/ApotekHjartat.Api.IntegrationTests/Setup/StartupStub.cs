
using ApotekHjartat.DbAccess.Context;
using ApotekHjartat.DbAccess.IoC;
using ApotekHjartat.DbAccess.Setup;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ApotekHjartat.Api.IntegrationTest.Stubs
{
    public class StartupStub : Startup
    {
        public StartupStub(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
        {
        }
        public override void ConfigureDataAccess(IServiceCollection services)
        {
            services.AddDataAccessServices();
            var dbName = Guid.NewGuid().ToString();
            services.AddDbContext<OrderDbContext>(options =>
            {
                options.UseInMemoryDatabase(dbName)
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
            // Create a scope to obtain a reference to the database
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var dbContext = scopedServices.GetRequiredService<OrderDbContext>();
            dbContext.Database.EnsureCreated();
            dbContext.SetupLocalDb();
        }
      
    }
}