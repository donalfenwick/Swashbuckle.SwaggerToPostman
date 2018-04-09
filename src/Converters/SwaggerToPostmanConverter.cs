using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman.Converters;
using Swashbuckle.SwaggerToPostman.PostmanSchema;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Request;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Swashbuckle.SwaggerToPostman.Converters
{
    public class SwaggerToPostmanConverter : ISwaggerToPostmanConverter
    {
        private readonly ISwaggerProvider swaggerProvider;
        private readonly IOperationObjectConverter operationConverter;
        private readonly DefaultValueFactory defaultValueFactory;

        public SwaggerToPostmanConverter(ISwaggerProvider swaggerProvider,  IOperationObjectConverter operationConverter)
        {
            this.swaggerProvider = swaggerProvider;
            this.operationConverter = operationConverter;
            this.defaultValueFactory = new DefaultValueFactory();
        }

        public PostmanRootCollection GetPostmanCollection(string swaggerDocumentName, string host = null, string basePath = null, string[] schemes = null)
        {
            SwaggerDocument swagger = swaggerProvider.GetSwagger(swaggerDocumentName, host, basePath, schemes);
            
            string apiBasePath = swagger.BasePath;

            PostmanRootCollection postmanRoot = new PostmanRootCollection();
            postmanRoot.Info = new PostmanCollectionInfo
            {
                PostmanId = Guid.NewGuid().ToString(),
                Schema = "https://schema.getpostman.com/json/collection/v2.1.0/collection.json",
            };
            if(swagger.Info != null)
            {
                postmanRoot.Info.Name = swagger.Info.Title ?? "";
                postmanRoot.Info.Version = swagger.Info.Version ?? "";
                postmanRoot.Info.Description = new PostmanDescription(swagger.Info.Description ??"");
                postmanRoot.Description = new PostmanDescription(swagger.Info.Description ?? "");
            }

            //TODO: Organise results into folders based on grouping
            //PostmanFolder folder = new PostmanFolder() { Name = "folderTitle", Id = "folderId" };
            foreach (var pathKey in swagger.Paths.Keys)
            {
                var path = swagger.Paths[pathKey];

                if (path.Get != null)
                {
                    postmanRoot.Items.Add(this.operationConverter.Convert(pathKey, PostmanHttpMethod.GET, path.Get, swagger));
                }
                if (path.Post != null)
                {
                    postmanRoot.Items.Add(this.operationConverter.Convert(pathKey, PostmanHttpMethod.POST, path.Post, swagger));
                }
                if (path.Put != null)
                {
                    postmanRoot.Items.Add(this.operationConverter.Convert(pathKey, PostmanHttpMethod.PUT, path.Put, swagger));
                }
                if (path.Delete != null)
                {
                    postmanRoot.Items.Add(this.operationConverter.Convert(pathKey, PostmanHttpMethod.DELETE, path.Delete, swagger));
                }
                if (path.Head != null)
                {
                    postmanRoot.Items.Add(this.operationConverter.Convert(pathKey, PostmanHttpMethod.HEAD, path.Head, swagger));
                }
                if (path.Patch != null)
                {
                    postmanRoot.Items.Add(this.operationConverter.Convert(pathKey, PostmanHttpMethod.PATCH, path.Patch, swagger));
                }
                if (path.Options != null)
                {
                    postmanRoot.Items.Add(this.operationConverter.Convert(pathKey, PostmanHttpMethod.OPTIONS, path.Options, swagger));
                }
            }
            //postmanRoot.Items.Add(folder);

            return postmanRoot;
        }

    }
}
