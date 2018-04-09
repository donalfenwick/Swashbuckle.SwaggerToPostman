using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swashbuckle.SwaggerToPostman.Converters.JsonBuilder
{
    public interface IRequestBodyJsonBuilder
    {
        /// <summary>
        /// Builds a sample json object from the provided swagger schema object
        /// </summary>
        /// <param name="schema">root schema to build the json object for</param>
        /// <param name="swaggerDocDefinitions">dictionary of schema references from swagger doc, used to lookup nested type references</param>
        /// <returns></returns>
        JToken GetJsonResult(Schema schema, IDictionary<string, Schema> swaggerDocDefinitions);
    }
}
