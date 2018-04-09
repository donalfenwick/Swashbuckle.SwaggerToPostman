using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swashbuckle.SwaggerToPostman.Converters
{
    public interface IHeaderParameterObjectConverter
    {
        List<PostmanHeader> Convert(List<NonBodyParameter> parameterCollection);
    }
}
