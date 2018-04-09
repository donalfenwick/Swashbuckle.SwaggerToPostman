using Moq;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman.Converters;
using Swashbuckle.SwaggerToPostman.Converters.JsonBuilder;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Request;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Util;
using System.Collections.Generic;
using Xunit;

namespace Tests.Converters
{
    [Trait("Category", "ConverterTests")]
    public class RequestBodyObjectConverterTests
    {
        private readonly BodyParameter _validBodyInput;
        private readonly IDictionary<string, Schema> _validSchemaDefinitions;
        private readonly Mock<IRequestBodyJsonBuilder> _requetBodyBuilderMock;

        public RequestBodyObjectConverterTests()
        {
            _validBodyInput = new BodyParameter()
            {
                Schema = new Schema { Ref = "#/definitions/BodyType" },
                Description = "test description",
            };
            _validSchemaDefinitions = new Dictionary<string, Schema>
            {
                {
                    "BodyType", new Schema
                    {
                        Type = "object",
                        Title = "bodyObjectType",
                        Properties = new Dictionary<string, Schema>()
                        {
                            { "uuid", new Schema { Title ="uuid", Format = "string", Type = "string" } },
                            { "code", new Schema { Title ="code", Format = "string", Type = "string" } }
                        }
                    }
                }
            };
            _requetBodyBuilderMock = new Mock<IRequestBodyJsonBuilder>();
            _requetBodyBuilderMock
                .Setup(m => m.GetJsonResult(It.IsAny<Schema>(), It.IsAny<IDictionary<string, Schema>>()))
                .Returns(new JObject());
        }

        [Fact]
        public void RequestBodyObjectConverter_ProducesExpectedResult_WithValidBodyParam()
        {
            RequestBodyObjectConverter converter = new RequestBodyObjectConverter(_requetBodyBuilderMock.Object, new DefaultValueFactory());
            PostmanRequestBody result = converter.Convert(_validBodyInput, new List<IParameter>(), _validSchemaDefinitions);

            Assert.NotNull(result);
        }

        [Fact]
        public void RequestBodyObjectConverter_ProducesExpectedResult_WithNullBodyParam()
        {
            RequestBodyObjectConverter converter = new RequestBodyObjectConverter(_requetBodyBuilderMock.Object, new DefaultValueFactory());
            PostmanRequestBody result = converter.Convert(null, new List<IParameter>(), _validSchemaDefinitions);

            Assert.NotNull(result);
        }

        [Fact]
        public void RequestBodyObjectConverter_ProducesResultWithRawMode_WithNullBodyParam()
        {
            RequestBodyObjectConverter converter = new RequestBodyObjectConverter(_requetBodyBuilderMock.Object, new DefaultValueFactory());
            PostmanRequestBody result = converter.Convert(null, new List<IParameter>(), _validSchemaDefinitions);

            Assert.Equal(PostmanRequestBodyMode.raw, result.Mode);
        }

        [Fact]
        public void RequestBodyObjectConverter_ProducesResultWithRawMode_WithoutFormDataParameters()
        {
            List<IParameter> parameters = new List<IParameter>();
            RequestBodyObjectConverter converter = new RequestBodyObjectConverter(_requetBodyBuilderMock.Object, new DefaultValueFactory());
            PostmanRequestBody result = converter.Convert(_validBodyInput, parameters, _validSchemaDefinitions);

            Assert.Equal(PostmanRequestBodyMode.raw, result.Mode);
        }

        [Fact]
        public void RequestBodyObjectConverter_ProducesResultWithUrlencodedMode_WithOneOrMoreFormDataParameters()
        {
            List<IParameter> parameters = new List<IParameter>()
            {
                new NonBodyParameter{ In = SwashbuckleParameterTypeConstants.FormData, Name = "testParam"}
            };
            RequestBodyObjectConverter converter = new RequestBodyObjectConverter(_requetBodyBuilderMock.Object, new DefaultValueFactory());
            PostmanRequestBody result = converter.Convert(_validBodyInput, parameters, _validSchemaDefinitions);

            Assert.Equal(PostmanRequestBodyMode.urlencoded, result.Mode);
        }
    }
}
