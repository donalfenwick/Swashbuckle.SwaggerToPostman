using Moq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman;
using Swashbuckle.SwaggerToPostman.Converters;
using Swashbuckle.SwaggerToPostman.Converters.JsonBuilder;
using Swashbuckle.SwaggerToPostman.PostmanSchema;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Request;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Util;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Compare;
using Xunit;

namespace Tests.Converters
{
    [Trait("Category", "ConverterTests")]
    public class OperationObjectConverterTests
    {

        private readonly Mock<IUrlObjectConverter> _urlCoverterMock;
        private readonly PostmanUrl _expectedUrlResult;
        private readonly List<IParameter> _validParameters;
        private readonly Mock<IRequestBodyObjectConverter> _bodyConverterMock;
        private readonly Mock<IHeaderParameterObjectConverter> _headerConverterMock;

        private readonly SwaggerDocument _validDoc;
        private readonly Operation _validOperation;

        public OperationObjectConverterTests()
        {
            _validDoc = new SwaggerDocument() { BasePath = "http://mysite.com" };
            _validOperation = new Operation()
            {
                Description = "sample_description",
                OperationId = Guid.NewGuid().ToString(),
                Parameters = _validParameters
            };

            _bodyConverterMock = new Mock<IRequestBodyObjectConverter>();
            _headerConverterMock = new Mock<IHeaderParameterObjectConverter>();
            _validParameters = new List<IParameter>()
            {
                new NonBodyParameter(){ In = SwashbuckleParameterTypeConstants.Path, Name = "id", Format = "int32", Type = "number" },
                new NonBodyParameter(){ In = SwashbuckleParameterTypeConstants.Query, Name = "filter", Format = "string", Type = "string" },
                new NonBodyParameter(){ In = SwashbuckleParameterTypeConstants.Query, Name = "page", Format = "int32", Type = "number" },
                new NonBodyParameter(){ In = SwashbuckleParameterTypeConstants.Header, Name = "x-custom-header", Format = "string", Type = "string" },
            };

            SwaggerDocument docResult = new SwaggerDocument();
            _expectedUrlResult = new PostmanUrl()
            {
                Hash = "",
                Raw = "http://mysite.com/api/action/:id?filter={{filter}}",
                Host = "mysite.com",
                Path = new string[] { "api", "action", ":id" },
                Port = "80",
                Protocol = "http",
                QueryParams = new List<PostmanQueryParam>
                {
                    new PostmanQueryParam("filter", "test")
                },
                Variables = new List<PostmanVariable>()
            };

            _urlCoverterMock = new Mock<IUrlObjectConverter>();
            _urlCoverterMock.Setup(x => x.Convert(It.IsAny<string>(), It.IsAny<List<IParameter>>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_expectedUrlResult);

        }

        [Fact]
        public void RequestBodyObjectConverter_ResultNameMatchesPath()
        {
            OperationObjectConverter converter = new OperationObjectConverter(_urlCoverterMock.Object, _headerConverterMock.Object, _bodyConverterMock.Object, new DefaultValueFactory());
            PostmanCollectionItem result = converter.Convert("/api/action/:id", PostmanHttpMethod.POST, _validOperation, _validDoc);
            Assert.Equal("/api/action/:id", result.Name);
        }

        [Fact]
        public void RequestBodyObjectConverter_ResultHasMatchingDescription()
        {
            OperationObjectConverter converter = new OperationObjectConverter(_urlCoverterMock.Object, _headerConverterMock.Object, _bodyConverterMock.Object, new DefaultValueFactory());
            PostmanCollectionItem result = converter.Convert("/api/action/:id", PostmanHttpMethod.POST, _validOperation, _validDoc);
            Assert.Equal(_validOperation.Description, result.Description.Content);
        }

        [Fact]
        public void RequestBodyObjectConverter_ResultHasMatchingId()
        {
            OperationObjectConverter converter = new OperationObjectConverter(_urlCoverterMock.Object, _headerConverterMock.Object, _bodyConverterMock.Object, new DefaultValueFactory());
            PostmanCollectionItem result = converter.Convert("/api/action/:id", PostmanHttpMethod.POST, _validOperation, _validDoc);
            Assert.Equal(_validOperation.OperationId, result.Id);
        }

        [Fact]
        public void RequestBodyObjectConverter_ResultVariblesArrayIsNotNull()
        {
            OperationObjectConverter converter = new OperationObjectConverter(_urlCoverterMock.Object, _headerConverterMock.Object, _bodyConverterMock.Object, new DefaultValueFactory());
            PostmanCollectionItem result = converter.Convert("/api/action/:id", PostmanHttpMethod.POST, _validOperation, _validDoc);
            Assert.NotNull(result.Variables);
        }

        [Fact]
        public void RequestBodyObjectConverter_ResultRequestIsNotNull()
        {
            OperationObjectConverter converter = new OperationObjectConverter(_urlCoverterMock.Object, _headerConverterMock.Object, _bodyConverterMock.Object, new DefaultValueFactory());
            PostmanCollectionItem result = converter.Convert("/api/action/:id", PostmanHttpMethod.POST, _validOperation, _validDoc);
            Assert.NotNull(result.Request);
        }

        [Fact]
        public void RequestBodyObjectConverter_ResultResponsesArrayIsNotNull()
        {
            OperationObjectConverter converter = new OperationObjectConverter(_urlCoverterMock.Object, _headerConverterMock.Object, _bodyConverterMock.Object, new DefaultValueFactory());
            PostmanCollectionItem result = converter.Convert("/api/action/:id", PostmanHttpMethod.POST, _validOperation, _validDoc);
            Assert.NotNull(result.Responses);
        }

        [Fact]
        public void RequestBodyObjectConverter_ResultEventsArrayIsNotNull()
        {
            OperationObjectConverter converter = new OperationObjectConverter(_urlCoverterMock.Object, _headerConverterMock.Object, _bodyConverterMock.Object, new DefaultValueFactory());
            PostmanCollectionItem result = converter.Convert("/api/action/:id", PostmanHttpMethod.POST, _validOperation, _validDoc);
            Assert.NotNull(result.Events);
        }

        [Fact]
        public void RequestBodyObjectConverter_DoesNotModifyUrlResultFromUrlMapper()
        {
            OperationObjectConverter converter = new OperationObjectConverter(_urlCoverterMock.Object, _headerConverterMock.Object, _bodyConverterMock.Object, new DefaultValueFactory());
            PostmanCollectionItem result = converter.Convert("/api/action/:id", PostmanHttpMethod.POST, _validOperation, _validDoc);
            
            Assert.Equal("", result.Request.Url.Hash);
            Assert.Equal("mysite.com", result.Request.Url.Host);
            Assert.Equal(new string[] { "api", "action", ":id" }, result.Request.Url.Path);
            Assert.Equal("80", result.Request.Url.Port);
            Assert.Equal("http", result.Request.Url.Protocol);
            Assert.Equal(new List<PostmanQueryParam> { new PostmanQueryParam("filter", "test") }, result.Request.Url.QueryParams, new PostmanQueryParamComparer());
            Assert.Equal("http://mysite.com/api/action/:id?filter={{filter}}", result.Request.Url.Raw);
            Assert.Equal(new List<PostmanVariable>(), result.Request.Url.Variables);
            
        }

    }
}
