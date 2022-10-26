
namespace ApotekHjartat.Common.Swagger
{
    internal class ModelAsStringExtension : Microsoft.OpenApi.Interfaces.IOpenApiExtension
    {
        private string name;
        private bool modelAsString;
        public ModelAsStringExtension(string propertyName, bool modelAsString = false)
        {
            name = propertyName;
            this.modelAsString = modelAsString;
        }

        public void Write(Microsoft.OpenApi.Writers.IOpenApiWriter writer, Microsoft.OpenApi.OpenApiSpecVersion specVersion)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("name");
            writer.WriteValue(name);
            writer.WritePropertyName("modelAsString");
            writer.WriteValue(modelAsString);
            writer.WriteEndObject();
        }
    }
}
