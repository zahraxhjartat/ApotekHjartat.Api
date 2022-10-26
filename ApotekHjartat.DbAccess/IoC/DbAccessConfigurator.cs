using ApotekHjartat.DbAccess.Context;
using ApotekHjartat.DbAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApotekHjartat.DbAccess.IoC
{
    public static class DbAccessConfigurator
    {
        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddTransient<ICustomerOrderDataAccess, CustomerOrderDataAccess>();

        }

        public static void AddDbContext(this IServiceCollection services, string connString)
        {
            services.AddDbContext<OrderDbContext>(
                option => option
                .EnableSensitiveDataLogging()
                .UseSqlServer(connString, (providerOptions) => { providerOptions.CommandTimeout(180); }), ServiceLifetime.Scoped);
        }
    }
}