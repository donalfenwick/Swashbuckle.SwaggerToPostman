using Microsoft.AspNetCore.Mvc;
using Swashbuckle.SwaggerToPostman.Converters;
using Swashbuckle.SwaggerToPostman.Converters.JsonBuilder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwashbucklePostmanServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerToPostman(this IServiceCollection services)
        {

            services.AddTransient<DefaultValueFactory>();
            services.AddTransient<IRequestBodyJsonBuilder, RequestBodyJsonBuilder>();
            services.AddTransient<IUrlObjectConverter, UrlObjectConverter>();
            services.AddTransient<IHeaderParameterObjectConverter, HeaderParameterObjectConverter>();
            services.AddTransient<IRequestBodyObjectConverter, RequestBodyObjectConverter>();
            services.AddTransient<IOperationObjectConverter, OperationObjectConverter>();
            services.AddTransient<ISwaggerToPostmanConverter, SwaggerToPostmanConverter>();

            return services;
        }
    }
}
