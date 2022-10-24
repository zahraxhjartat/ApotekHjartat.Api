using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApotekHjartat.Order.Common.Swagger
{
    public class AdditionalParametersDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument openApiDoc, DocumentFilterContext context)
        {
            foreach (var schema in context.SchemaRepository.Schemas)
            {
                if (schema.Value.AdditionalProperties == null)
                {
                    schema.Value.AdditionalPropertiesAllowed = true;
                }
            }
        }
    }
}