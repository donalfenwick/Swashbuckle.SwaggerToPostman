using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman.Converters.JsonBuilder;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Request;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Swashbuckle.SwaggerToPostman.Converters
{
    public class RequestBodyObjectConverter : IRequestBodyObjectConverter
    {
        private readonly IRequestBodyJsonBuilder jsonRequestBodyBuilder;
        private readonly DefaultValueFactory defaultValueFactory;

        public RequestBodyObjectConverter(IRequestBodyJsonBuilder jsonRequestBodyBuilder, DefaultValueFactory defaultValueFactory)
        {
            this.jsonRequestBodyBuilder = jsonRequestBodyBuilder;
            this.defaultValueFactory = defaultValueFactory;
        }

        public PostmanRequestBody Convert(BodyParameter bodyParam, List<IParameter> allParams, IDictionary<string, Schema> swaggerDocDefinitions)
        {
            PostmanRequestBody bodyResult = new PostmanRequestBody();
            if (bodyParam != null)
            {
                bodyResult.Mode = PostmanRequestBodyMode.raw;
                if (bodyParam.Schema != null)
                {
                    if (!string.IsNullOrWhiteSpace(bodyParam.Schema.Ref))
                    {
                        string typeName = bodyParam.Schema.Ref.Replace("#/definitions/", "");

                        string json = "";

                        if (swaggerDocDefinitions[typeName] != null)
                        {
                            Schema bodySchema = swaggerDocDefinitions[typeName];

                            JToken bodyJson = this.jsonRequestBodyBuilder.GetJsonResult(bodySchema, swaggerDocDefinitions);
                            json = bodyJson.ToString();
                        }

                        bodyResult.Raw = json;
                    }
                    else
                    {
                        // non complex types to the result directly (primitives and array types)
                        JToken bodyJson = this.jsonRequestBodyBuilder.GetJsonResult(bodyParam.Schema, swaggerDocDefinitions);
                        string json = bodyJson.ToString();
                        bodyResult.Raw = json;
                    }
                }
            }

            var formdataParams = allParams.OfType<NonBodyParameter>()
                .Where(p => p.In == SwashbuckleParameterTypeConstants.FormData);
            if (formdataParams.Count() > 0)
            {
                bodyResult.Mode = PostmanRequestBodyMode.urlencoded;
                bodyResult.UrlEncoded = new List<PostmanKeyValuePair>();
            }
            else
            {
                bodyResult.Mode = PostmanRequestBodyMode.raw;
            }
            
            foreach (NonBodyParameter p in formdataParams)
            {
                object defaultValue = this.defaultValueFactory.GetDefaultValueFromFormat(p.Format, p.Type);
                string value = (defaultValue != null) ? defaultValue.ToString() : "";

                if (p.In == SwashbuckleParameterTypeConstants.FormData)
                {
                    bodyResult.UrlEncoded.Add(new PostmanKeyValuePair
                    {
                        Description = new PostmanDescription { Content = p.Description },
                        Disabled = false,
                        Key = p.Name,
                        Value = value
                    });
                }
            }

            return bodyResult;
        }
    }
}
