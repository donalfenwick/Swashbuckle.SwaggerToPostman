using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swashbuckle.SwaggerToPostman.Converters.JsonBuilder
{
    /// <summary>
    /// Builds a JObject which can be used to populate a sample request body within the postman collection
    /// </summary>
    public class RequestBodyJsonBuilder : IRequestBodyJsonBuilder
    {
        public RequestBodyJsonBuilder(DefaultValueFactory defaultValueFactory)
        {
            this.defaultValueFactory = defaultValueFactory;
        }
        public JToken GetJsonResult(Schema schema, IDictionary<string, Schema> swaggerDocDefinitions)
        {
            return this.GetJTokenForSchemaInternal(schema, swaggerDocDefinitions, 0);
        }
        /// <summary>
        /// Maximum tree depth for building example json from api models
        /// </summary>
        private const int MaxRecursionDepth = 20;
        private readonly DefaultValueFactory defaultValueFactory;

        /// <summary>
        /// Recursivly build a json.net JObject from the root schema
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        private JToken GetJTokenForSchemaInternal(Schema schema, IDictionary<string, Schema> swaggerDocDefinitions, int recursionDepth)
        {

            if (recursionDepth < MaxRecursionDepth)
            {
                // first check if the current schema has a reference instead of type info and pull in the reference data if possible
                if (schema.Type == null && !string.IsNullOrWhiteSpace(schema.Ref))
                {
                    var refschema = this.FindSchemaFromReference(schema.Ref, swaggerDocDefinitions);
                    if (refschema != null)
                    {
                        schema = refschema;
                    }
                }

                if (schema.Type == "object")
                {
                    JObject obj = new JObject();
                    foreach (string propKey in schema.Properties.Keys)
                    {
                        Schema childSchema = schema.Properties[propKey];
                        JToken token = GetJTokenForSchemaInternal(childSchema, swaggerDocDefinitions, recursionDepth + 1);
                        obj.Add(propKey, token);

                    }
                    return obj;
                }
                else if (schema.Type == "array")
                {
                    int exampleArrayLength = 2;
                    var array = new JArray();
                    for (int i = 0; i < exampleArrayLength; i++)
                    {
                        JToken token = GetJTokenForSchemaInternal(schema.Items, swaggerDocDefinitions, recursionDepth + 1);
                        array.Add(token);
                    }
                    return array;
                }
                else
                {
                    object defaultValue = this.defaultValueFactory.GetDefaultValueFromFormat(schema.Format, schema.Type);
                    return new JValue(defaultValue);
                }
            }
            else
            {
                return null;
            }
        }



        private Schema FindSchemaFromReference(string reference, IDictionary<string, Schema> swaggerDocDefinitions)
        {
            string typeName = reference.Replace("#/definitions/", "");

            if (swaggerDocDefinitions[typeName] != null)
            {
                Schema schema = swaggerDocDefinitions[typeName];
                return schema;
            }
            return null;
        }
    }
}
