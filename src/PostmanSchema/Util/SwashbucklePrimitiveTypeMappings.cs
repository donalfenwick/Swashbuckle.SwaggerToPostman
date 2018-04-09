using System;
using System.Collections.Generic;
using System.Text;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Util
{
    public static class SwashbucklePrimitiveTypeMappings
    {
        public static List<SwashbuckleTypeMapping> GetMappings()
        {
            return new List<SwashbuckleTypeMapping>()
            {
                new SwashbuckleTypeMapping{ DataType = typeof(int), ApiTypeName = "integer", Format = "int32", DefaultValue = 1 },
                new SwashbuckleTypeMapping{ DataType = typeof(long), ApiTypeName = "integer", Format = "int64", DefaultValue = 4294967296 },
                new SwashbuckleTypeMapping{ DataType = typeof(float), ApiTypeName = "number", Format = "float" , DefaultValue = 4.23534564F },
                new SwashbuckleTypeMapping{ DataType = typeof(double), ApiTypeName = "number", Format = "double", DefaultValue = 2.34534535D },
                new SwashbuckleTypeMapping{ DataType = typeof(bool), ApiTypeName = "boolean", DefaultValue = true },
                new SwashbuckleTypeMapping{ DataType = typeof(string), ApiTypeName = "string", Format = "string", DefaultValue = "sample value" },
                new SwashbuckleTypeMapping{ DataType = typeof(DateTime), ApiTypeName = "string", Format = "date-time", DefaultValue = DateTime.Parse("2018-1-1T01:22:12") },
                new SwashbuckleTypeMapping{ DataType = typeof(Guid), ApiTypeName = "string", Format = "uuid",DefaultValue = new Guid("a1600c73-05c5-4862-83e3-9c921e5b6e96") },
                new SwashbuckleTypeMapping{ DataType = typeof(string), ApiTypeName = "string", Format = "byte", DefaultValue = new Byte() },
            };
        }
    }
}
