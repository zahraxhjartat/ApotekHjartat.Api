using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace ApotekHjartat.Order.Common.Swagger
{
    public class EnumFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema == null)
                throw new ArgumentNullException("schema");

            if (context == null)
                throw new ArgumentNullException("context");

            if (context.Type.IsEnum)
            {
                schema.Extensions.Add("x-ms-enum", new ModelAsStringExtension(context.Type.Name));
            }
            if (Nullable.GetUnderlyingType(context.Type) != null && Nullable.GetUnderlyingType(context.Type).IsEnum)
            {
                schema.Extensions.Add("x-ms-enum", new ModelAsStringExtension(context.Type.Name));
            }
        }
    }
}