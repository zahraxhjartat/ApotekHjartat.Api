using ApotekHjartat.Order.Common.Swagger;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using ApotekHjartat.Order.Api.Filters;

namespace ApotekHjartat.Order.Api
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    /// <remarks>This allows API versioning to define a Swagger document per API version after the
    /// <see cref="IApiVersionDescriptionProvider"/> service has been resolved from the service container.</remarks>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions"/> class.
        /// </summary>
        /// <param name="provider">The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger documents.</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        /// <inheritdoc />
        public void Configure(SwaggerGenOptions options)
        {
            options.CustomOperationIds(e => $"{e.GroupName ?? string.Empty}_{e.ActionDescriptor.RouteValues["controller"]}{(e.TryGetMethodInfo(out System.Reflection.MethodInfo methodInfo) ? methodInfo.Name : e.HttpMethod)}");
            // add a swagger document for each discovered API version
            // note: you might choose to skip or document deprecated API versions differently
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }

            options.OperationFilter<RemoveVersionParameters>();
            options.DocumentFilter<SetVersionInPaths>();

            //Set the comments path for the swagger json and ui.
            var basePath = Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath;
            var dir = System.IO.Directory.CreateDirectory(basePath);
            foreach (var xmlFile in dir.EnumerateFiles("*.xml"))
            {
                if (System.IO.File.Exists(xmlFile.FullName.Replace(".xml", ".dll")))
                {
                    options.IncludeXmlComments(xmlFile.FullName);
                }
            }

            //Show enums as string representations
            options.SchemaFilter<EnumFilter>();
            options.ParameterFilter<AutoRestParameterFilter>();
            options.OperationFilter<SecurityRequirementsOperationFilter>();

            // Specifying security definition shown in swagger UI. Actual security requirements for each controller are specified in SecurityRequirementsOperationFilter above.
            options.AddSecurityDefinition(
                "Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "ORDER API",
                Version = description.ApiVersion.ToString(),
                Description = "API for accessing order functionality"
            };

            info.Description += " ! Protected by JWT !";

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
