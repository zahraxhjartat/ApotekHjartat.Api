using ApotekHjartat.Api.IntegrationTest.Setup;
using ApotekHjartat.DbAccess.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApotekHjartat.DbAccess.IoC;

namespace ApotekHjartat.Api.IntegrationTest
{
    public class StartupStubLocalDb : Startup
    {
        public StartupStubLocalDb(IConfiguration configuration, IWebHostEnvironment environment) : base(configuration, environment)
        {

        }

        public override void ConfigureDataAccess(IServiceCollection services)
        {
            services.AddDataAccessServices();

            services.AddDbContext<OrderDbContext>(options =>
            {
                options.UseSqlServer(DbContextConfigurator.ConnString);
            });
        }
    }
}