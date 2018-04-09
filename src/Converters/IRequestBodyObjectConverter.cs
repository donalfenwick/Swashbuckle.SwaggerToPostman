using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Request;
using System.Collections.Generic;

namespace Swashbuckle.SwaggerToPostman.Converters
{
    public interface IRequestBodyObjectConverter
    {
        PostmanRequestBody Convert(BodyParameter bodyParam, List<IParameter> allParams, IDictionary<string, Schema> swaggerDocDefinitions);
    }
}
