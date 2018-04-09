using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swashbuckle.SwaggerToPostman.Converters
{
    public class HeaderParameterObjectConverter : IHeaderParameterObjectConverter
    {
        public HeaderParameterObjectConverter()
        {

        }

        public List<PostmanHeader> Convert(List<NonBodyParameter> parameterCollection)
        {
            List<PostmanHeader> result = new List<PostmanHeader>();
            var formdataParams = parameterCollection.Where(p => p.In == SwashbuckleParameterTypeConstants.FormData);
            // set the content type header depending on if any formdata params are 
            // present in the list of NonBodyParameter types
            if (formdataParams.Count() > 0)
            {
                result.Add(new PostmanHeader()
                {
                    Key = "Content-Type",
                    Value = "application/x-www-form-urlencoded",
                    Disabled = false
                });
            }
            else
            {
                result.Add(new PostmanHeader()
                {
                    Key = "Content-Type",
                    Value = "application/json",
                    Disabled = false
                });
            }
            // filter only header NonBodyParameter types to exclude path, query and formdata types
            var headerParams = parameterCollection.Where(p => p.In == SwashbuckleParameterTypeConstants.Header);
            foreach (NonBodyParameter headerParam in headerParams)
            {
                if (headerParam.In == SwashbuckleParameterTypeConstants.Header)
                {
                    result.Add(new PostmanHeader()
                    {
                        Key = headerParam.Name,
                        Description = new PostmanDescription { Content = headerParam.Description },
                        Disabled = false,
                        Value = ""
                    });
                }
            }
            return result;
        }
    }
}
