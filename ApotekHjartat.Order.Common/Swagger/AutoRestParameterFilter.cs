using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace ApotekHjartat.Order.Common.Swagger
{
    public class AutoRestParameterFilter : IParameterFilter
    {
        public void Apply(Microsoft.OpenApi.Models.OpenApiParameter parameter, ParameterFilterContext context)
        {
            var type = context.ApiParameterDescription.Type;

            if (type.IsEnum)
            {
                parameter.Extensions.Add("x-ms-enum", new ModelAsStringExtension(type.Name));
            }
        }
    }
}
