
using ApotekHjartat.Order.Api.Controllers.Services;
using ApotekHjartat.Order.Common.Swagger;
using ApotekHjartat.Order.DbAccess.Context;
using ApotekHjartat.Order.DbAccess.DataAccess.v1;
using ApotekHjartat.Order.DbAccess.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace ApotekHjartat.Order.Api
{
    public class Startup
    {
        public Startup(
              IConfiguration configuration,
              IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");
            services.AddControllers();
            services.AddApiVersioning(o => o.ReportApiVersions = true);
            services.AddMemoryCache();
            services.AddSwaggerGen(c =>
            {
                //AdditionalParametersDocumentFilter is only needed since autorest can't handle additionalProperties = false, this is a bug in autorest.
                c.DocumentFilter<AdditionalParametersDocumentFilter>();
                // To set decimal format in swagger so that decimal becomes decimal when generating and not double
                c.MapType<decimal>(() => new OpenApiSchema { Type = "number", Format = "decimal" });
                c.MapType<decimal?>(() => new OpenApiSchema { Type = "number", Format = "decimal" });
                c.MapType<IFormFile>(() => new OpenApiSchema { Type = "object", Format = "file" });
                c.MapType<FormFile>(() => new OpenApiSchema { Type = "object", Format = "file" });

                c.MapType<System.IO.Stream>(() => new OpenApiSchema() { Type = "object", Format = "file" });
            });
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            ConfigureDataAccess(services);

            services.AddTransient<ICustomerOrderService, CustomerOrderService>();

            services.AddHealthChecks();
            services.AddRouting(options => options.LowercaseUrls = true);
        }

        public virtual void ConfigureDataAccess(IServiceCollection services)
        {
            services.AddTransient<ICustomerOrderDataAccess, CustomerOrderDataAccess>();
            var connectionString = Configuration.GetSection("ConnectionStrings")["DefaultConnection"];

            services.AddDbContext<OrderDbContext>(
               option => option
               .EnableSensitiveDataLogging()
               .UseSqlServer(connectionString, (providerOptions) => { providerOptions.CommandTimeout(180); }), ServiceLifetime.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // migrate and seed database locally
                using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var dbContext = serviceScope.ServiceProvider.GetService<OrderDbContext>();
                dbContext.SetupLocalDb();

                app.UseSwagger(c =>
                {
                    c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                        swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{httpReq.PathBase}" } }
                    );
                });


                app.UseSwaggerUI(c =>
                {
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        c.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
