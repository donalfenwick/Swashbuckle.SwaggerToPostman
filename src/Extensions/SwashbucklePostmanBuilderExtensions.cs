using Swashbuckle.SwaggerToPostman.Middleware;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    public static class SwashbucklePostmanBuilderExtensions
    {
        public static IApplicationBuilder UseSwaggerToPostman(
            this IApplicationBuilder app,
            Action<SwaggerToPostmanMiddlewareOptions> setupAction = null)
        {
            var options = new SwaggerToPostmanMiddlewareOptions();
            setupAction?.Invoke(options);

            return app.UseMiddleware<SwaggerToPostmanMiddleware>(options);
        }
    }
}
