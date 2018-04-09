using System;
using System.Collections.Generic;
using System.Text;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Util
{
    public class SwashbuckleTypeMapping
    {
        public Type DataType { get; set; }
        public string ApiTypeName { get; set; }
        public string Format { get; set; }
        public object DefaultValue { get; set; }
    }
}
