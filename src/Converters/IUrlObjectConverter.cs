using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swashbuckle.SwaggerToPostman.Converters
{
    public interface IUrlObjectConverter
    {
        PostmanUrl Convert(string path, List<IParameter> swaggerOperationParameters, string host, string basePath);
    }
}
