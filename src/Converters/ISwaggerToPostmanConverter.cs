using Swashbuckle.SwaggerToPostman.PostmanSchema;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swashbuckle.SwaggerToPostman.Converters
{
    public interface ISwaggerToPostmanConverter
    {
        PostmanRootCollection GetPostmanCollection(string swaggerDocumentName, string host = null, string basePath = null, string[] schemes = null);
    }
}
