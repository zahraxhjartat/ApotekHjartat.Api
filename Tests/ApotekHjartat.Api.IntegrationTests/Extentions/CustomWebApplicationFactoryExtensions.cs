using ApotekHjartat.Api.Integration.Test.AppStartup;
using ApotekHjartat.Api.IntegrationTest.Stubs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace ApotekHjartat.Api.IntegrationTest.Extensions
{
    public static class CustomWebApplicationFactoryExtensions
    {
        public static WebApplicationFactory<StartupStub> InitializeWithHostFromCustomFactory(this CustomWebApplicationFactory<StartupStub> customFactory)
        {
            return customFactory.WithWebHostBuilder(builder =>
            {
                builder.UseSolutionRelativeContentRoot("Tests");
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config
                        .AddJsonFile("ApotekHjartat.Api.IntegrationTests\\appsettings.json")
                        .AddEnvironmentVariables();
                });
                builder.ConfigureTestServices(services =>
                {
                    services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
                });
            });
        }
        public static WebApplicationFactory<StartupStubLocalDb> InitializeWithHostFromCustomFactory(this CustomWebApplicationFactory<StartupStubLocalDb> customFactory)
        {
            return customFactory.WithWebHostBuilder(builder =>
            {
                builder.UseSolutionRelativeContentRoot("Tests");
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config
                        .AddJsonFile("ApotekHjartat.Api.IntegrationTest\\appsettings.json")
                        .AddEnvironmentVariables();
                });
                builder.ConfigureTestServices(services =>
                {
                    services.AddMvc().AddApplicationPart(typeof(Startup).Assembly);
                });
            });
        }
    }
}