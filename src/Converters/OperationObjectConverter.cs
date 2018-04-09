using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman.Converters.JsonBuilder;
using Swashbuckle.SwaggerToPostman.PostmanSchema;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Request;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Util;

namespace Swashbuckle.SwaggerToPostman.Converters
{
    public class OperationObjectConverter : IOperationObjectConverter
    {
        private readonly IUrlObjectConverter urlConverter;
        private readonly IHeaderParameterObjectConverter headerConverter;
        private readonly IRequestBodyObjectConverter requestBodyConverter;
        private readonly DefaultValueFactory defaultValueFactory;

        public OperationObjectConverter(IUrlObjectConverter urlConverter, IHeaderParameterObjectConverter headerConverter, IRequestBodyObjectConverter requestBodyConverter, DefaultValueFactory defaultValueFactory)
        {
            this.urlConverter = urlConverter;
            this.headerConverter = headerConverter;
            this.requestBodyConverter = requestBodyConverter;
            this.defaultValueFactory = defaultValueFactory;
        }

        public PostmanCollectionItem Convert(string path, PostmanHttpMethod method, Operation operation, SwaggerDocument swaggerDoc)
        {
            IList<IParameter> parameters = operation.Parameters;
            if(parameters == null)
            {
                parameters = new List<IParameter>();
            }

            PostmanUrl urlObject = this.urlConverter.Convert(path, parameters.ToList(), swaggerDoc.Host, swaggerDoc.BasePath);
            List<PostmanHeader> headerList = this.headerConverter.Convert(parameters.OfType<NonBodyParameter>().ToList());

            BodyParameter bodyParam = parameters.OfType<BodyParameter>().FirstOrDefault(p => p is BodyParameter);
            PostmanRequestBody body = requestBodyConverter.Convert(bodyParam, parameters.ToList(), swaggerDoc.Definitions);

            var collectionItem = new PostmanCollectionItem()
            {
                Description = new PostmanDescription { Content = operation.Description },
                Id = operation.OperationId,
                Name = path,
                Request = new PostmanRequest
                {
                    Method = method,
                    Description = new PostmanDescription { Content = operation.Description ?? "" },
                    Url = urlObject,
                    Headers = headerList,
                    Body = body
                }
            };
            return collectionItem;
        }



        
    }
}
