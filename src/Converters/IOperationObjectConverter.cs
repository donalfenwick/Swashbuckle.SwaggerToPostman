using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman.PostmanSchema;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Request;

namespace Swashbuckle.SwaggerToPostman.Converters
{
    public interface IOperationObjectConverter
    {
        PostmanCollectionItem Convert(string path, PostmanHttpMethod method, Operation op, SwaggerDocument swaggerDoc);
    }
}
