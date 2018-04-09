using Moq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman;
using Swashbuckle.SwaggerToPostman.Converters;
using Swashbuckle.SwaggerToPostman.PostmanSchema;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Converters
{
    [Trait("Category", "ConverterTests")]
    public class SwaggerToPostmanConverterTests
    {
        private readonly Mock<ISwaggerProvider> _swaggerProviderMock;
        private readonly Mock<IOperationObjectConverter> _operationConverterMock;
        private readonly SwaggerDocument _validSwaggerDoc;

        public SwaggerToPostmanConverterTests()
        {
            _validSwaggerDoc = new SwaggerDocument()
            {
                Info = new Info
                {
                    Contact = new Contact { Name = "sample name", Email = "person@domain.tld" },
                    Description = "sample description",
                    Title = "sample title",
                    Version = "v1.5.1"
                },
                BasePath = "http://mysite.com/",
                Host = "mysite.com",
                Paths = new Dictionary<string, PathItem>()
            };
            
            _validSwaggerDoc.Paths.Add("/api/item1",new PathItem
            {
                Get = new Operation(),
                Post = new Operation()
            });

            _validSwaggerDoc.Paths.Add("/api/item2", new PathItem
            {
                Put = new Operation(),
                Delete = new Operation()
            });


            _swaggerProviderMock = new Mock<ISwaggerProvider>();
            _swaggerProviderMock.Setup(m => m.GetSwagger(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string[]>()))
                .Returns(_validSwaggerDoc);
            _operationConverterMock = new Mock<IOperationObjectConverter>();

            _operationConverterMock.Setup(m => m.Convert(It.IsAny<string>(), It.IsAny<PostmanHttpMethod>(), It.IsAny<Operation>(), _validSwaggerDoc))
                .Returns((string path, PostmanHttpMethod method, Operation op, SwaggerDocument doc) =>
                {
                    return new PostmanCollectionItem
                    {
                        Id = "sample_id",
                        Description = new PostmanDescription("sample_description"),
                        Name = path,
                        Request = new PostmanRequest {
                            Method = method
                        } 
                    };
                });
        }

        [Fact]
        public void SwaggerToPostmanConverter_ResultNotNull_WithValidInput()
        {
            SwaggerToPostmanConverter converter = new SwaggerToPostmanConverter(_swaggerProviderMock.Object, _operationConverterMock.Object);
            PostmanRootCollection result = converter.GetPostmanCollection("v1", "mysite.com", "http://mysite.com/");

            Assert.NotNull(result);
        }

        [Fact]
        public void SwaggerToPostmanConverter_InfoIsNotNull()
        {
            SwaggerToPostmanConverter converter = new SwaggerToPostmanConverter(_swaggerProviderMock.Object, _operationConverterMock.Object);
            PostmanRootCollection result = converter.GetPostmanCollection("v1", "mysite.com", "http://mysite.com/");

            Assert.NotNull(result.Info);
        }

        [Fact]
        public void SwaggerToPostmanConverter_ReturnsValidDescription()
        {
            SwaggerToPostmanConverter converter = new SwaggerToPostmanConverter(_swaggerProviderMock.Object, _operationConverterMock.Object);
            PostmanRootCollection result = converter.GetPostmanCollection("v1", "mysite.com", "http://mysite.com/");

            Assert.Equal(_validSwaggerDoc.Info.Description,result.Info.Description.Content);
        }

        [Fact]
        public void SwaggerToPostmanConverter_ReturnsValidTitle()
        {
            SwaggerToPostmanConverter converter = new SwaggerToPostmanConverter(_swaggerProviderMock.Object, _operationConverterMock.Object);
            PostmanRootCollection result = converter.GetPostmanCollection("v1", "mysite.com", "http://mysite.com/");

            Assert.Equal(_validSwaggerDoc.Info.Title, result.Info.Name);
        }
        
        [Fact]
        public void SwaggerToPostmanConverter_ReturnsValidVersion()
        {
            SwaggerToPostmanConverter converter = new SwaggerToPostmanConverter(_swaggerProviderMock.Object, _operationConverterMock.Object);
            PostmanRootCollection result = converter.GetPostmanCollection("v1", "mysite.com", "http://mysite.com/");

            Assert.Equal(_validSwaggerDoc.Info.Version, result.Info.Version);
        }
        [Fact]
        public void SwaggerToPostmanConverter_ReturnsCorrectPostmanSchema()
        {
            SwaggerToPostmanConverter converter = new SwaggerToPostmanConverter(_swaggerProviderMock.Object, _operationConverterMock.Object);
            PostmanRootCollection result = converter.GetPostmanCollection("v1", "mysite.com", "http://mysite.com/");

            Assert.Equal("https://schema.getpostman.com/json/collection/v2.1.0/collection.json", result.Info.Schema);
        }
        

        [Fact]
        public void SwaggerToPostmanConverter_ReturnsCorrectNumberOfCollectionItem_ForFirstPath()
        {
            SwaggerToPostmanConverter converter = new SwaggerToPostmanConverter(_swaggerProviderMock.Object, _operationConverterMock.Object);
            PostmanRootCollection result = converter.GetPostmanCollection("v1", "mysite.com", "http://mysite.com/");

            Assert.Equal(1, result.Items.OfType<PostmanCollectionItem>().Count(i => i.Name == "/api/item1" && i.Request.Method == PostmanHttpMethod.GET));
            Assert.Equal(1, result.Items.OfType<PostmanCollectionItem>().Count(i => i.Name == "/api/item1" && i.Request.Method == PostmanHttpMethod.POST));
            Assert.Equal(0, result.Items.OfType<PostmanCollectionItem>().Count(i => i.Name == "/api/item1" && i.Request.Method == PostmanHttpMethod.PUT));
            Assert.Equal(0, result.Items.OfType<PostmanCollectionItem>().Count(i => i.Name == "/api/item1" && i.Request.Method == PostmanHttpMethod.DELETE));
        }

        [Fact]
        public void SwaggerToPostmanConverter_ReturnsCorrectNumberOfCollectionItem_ForSecondPath()
        {
            SwaggerToPostmanConverter converter = new SwaggerToPostmanConverter(_swaggerProviderMock.Object, _operationConverterMock.Object);
            PostmanRootCollection result = converter.GetPostmanCollection("v1", "mysite.com", "http://mysite.com/");

            Assert.Equal(0, result.Items.OfType<PostmanCollectionItem>().Count(i => i.Name == "/api/item2" && i.Request.Method == PostmanHttpMethod.GET));
            Assert.Equal(0, result.Items.OfType<PostmanCollectionItem>().Count(i => i.Name == "/api/item2" && i.Request.Method == PostmanHttpMethod.POST));
            Assert.Equal(1, result.Items.OfType<PostmanCollectionItem>().Count(i => i.Name == "/api/item2" && i.Request.Method == PostmanHttpMethod.PUT));
            Assert.Equal(1, result.Items.OfType<PostmanCollectionItem>().Count(i => i.Name == "/api/item2" && i.Request.Method == PostmanHttpMethod.DELETE));
        }
    }
}
