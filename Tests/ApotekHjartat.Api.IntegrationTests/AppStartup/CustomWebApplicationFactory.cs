using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
namespace ApotekHjartat.Api.Integration.Test.AppStartup
{
    public class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder(null).UseEnvironment("Testing")
                          .UseStartup<TEntryPoint>();
        }
    }
    public class SwaggerCustomWebApplicationFactory<TEntryPoint> : CustomWebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder(null).UseEnvironment("Development")
                          .UseStartup<TEntryPoint>();
        }
    }
}