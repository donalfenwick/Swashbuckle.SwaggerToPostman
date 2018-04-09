using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman.Contracts;
using Swashbuckle.SwaggerToPostman.Converters;
using Swashbuckle.SwaggerToPostman.PostmanSchema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Swashbuckle.SwaggerToPostman.Middleware
{
    public class SwaggerToPostmanMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SwaggerToPostmanMiddlewareOptions _options;
        private readonly ISwaggerToPostmanConverter _postmanConverter;
        private TemplateMatcher _requestMatcher;

        //private readonly TemplateMatcher _requestMatcher;

        public SwaggerToPostmanMiddleware(RequestDelegate next, 
            SwaggerToPostmanMiddlewareOptions options,
            ISwaggerToPostmanConverter postmanConverter)
        {
            _next = next;
            _options = options;
            _postmanConverter = postmanConverter;
            _requestMatcher = new TemplateMatcher(TemplateParser.Parse(options.RouteTemplate), new RouteValueDictionary());
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string documentName;
            if (!PathMatchesPostmanDoc(httpContext.Request, out documentName))
            {
                await _next(httpContext);
                return;
            }

            var basePath = string.IsNullOrEmpty(httpContext.Request.PathBase)
                ? null
                : httpContext.Request.PathBase.ToString();
            var host = httpContext.Request.Host != null ? httpContext.Request.Host.Value : "";
            JsonSerializer jsonSerializer = new JsonSerializer()
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore
            };
            jsonSerializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            var jsonBuilder = new StringBuilder();
            using (var writer = new StringWriter(jsonBuilder))
            {
                httpContext.Response.ContentType = "application/json";

                try
                {
                    PostmanRootCollection postmanCollection = _postmanConverter.GetPostmanCollection(documentName, host, basePath);

                    httpContext.Response.StatusCode = 200;
                    jsonSerializer.Serialize(writer, postmanCollection);
                    await httpContext.Response.WriteAsync(jsonBuilder.ToString(), new UTF8Encoding(false));

                }
                catch (UnknownSwaggerDocument)
                {
                    httpContext.Response.StatusCode = 404;
                    jsonSerializer.Serialize(writer, new ErrorResponse
                    {
                        Message = $"Unknown document '{documentName??""}'"
                    });
                    await httpContext.Response.WriteAsync(jsonBuilder.ToString(), new UTF8Encoding(false));
                }
            }
        }

        private bool PathMatchesPostmanDoc(HttpRequest request, out string documentName)
        {
            documentName = null;
            if (request.Method != "GET") return false;

            var routeValues = new RouteValueDictionary();
            if (!_requestMatcher.TryMatch(request.Path, routeValues) || !routeValues.ContainsKey("documentName")) return false;

            documentName = routeValues["documentName"].ToString();
            return true;
        }


    }
}
