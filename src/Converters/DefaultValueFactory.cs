using Swashbuckle.SwaggerToPostman.PostmanSchema.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swashbuckle.SwaggerToPostman.Converters
{
    public class DefaultValueFactory
    {
        public object GetDefaultValueFromFormat(string format, string apiType)
        {
            object defaultValue = null;
            SwashbuckleTypeMapping typeMapping = null;
            
            // first try and match both the format and type, if we cant find a fit then try to match on the format first and then the type
            typeMapping = SwashbucklePrimitiveTypeMappings.GetMappings().FirstOrDefault(x => x.Format == format && x.ApiTypeName == apiType);
            

            if (typeMapping == null)
            {
                typeMapping = SwashbucklePrimitiveTypeMappings.GetMappings().FirstOrDefault(x => x.Format == format);
            }

            if (typeMapping == null)
            {
                typeMapping = SwashbucklePrimitiveTypeMappings.GetMappings().FirstOrDefault(x => x.ApiTypeName == apiType);
            }
            // generate a default value based on the discovered type
            if (typeMapping != null)
            {
                defaultValue = typeMapping.DefaultValue ?? GetDefault(typeMapping.DataType);
            }
            return defaultValue;
        }


        private static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}
