using System;
using System.Collections.Generic;
using System.Text;

namespace Swashbuckle.SwaggerToPostman.Middleware
{
    public class SwaggerToPostmanMiddlewareOptions
    {

        /// <summary>
        /// Sets a custom route for the Swagger JSON endpoint(s). Must include the {documentName} parameter
        /// </summary>
        public string RouteTemplate { get; set; } = "postman/{documentName}/collection.json";
    }
}
