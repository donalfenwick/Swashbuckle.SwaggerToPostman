using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman;
using Swashbuckle.SwaggerToPostman.Converters;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.Converters
{
    [Trait("Category", "ConverterTests")]
    public class UrlObjectConverterTests
    {
        private readonly string _validPath;
        private readonly List<IParameter> _validParameters;
        private readonly string _validHost;
        private readonly string _validBasePath;

        public UrlObjectConverterTests()
        {
            _validPath = "/api/Values/postFormDataEndpoint/{id}";
            _validParameters = new List<IParameter>()
            {
                new NonBodyParameter(){ In = SwashbuckleParameterTypeConstants.Path, Name = "id", Format = "int32", Type = "number" },
                new NonBodyParameter(){ In = SwashbuckleParameterTypeConstants.Query, Name = "filter", Format = "string", Type = "string" },
                new NonBodyParameter(){ In = SwashbuckleParameterTypeConstants.Query, Name = "page", Format = "int32", Type = "number" },
                new NonBodyParameter(){ In = SwashbuckleParameterTypeConstants.Header, Name = "x-custom-header", Format = "string", Type = "string" },
            };
            _validHost = "http://mysite.com";
            _validBasePath = "";
        }

        [Fact]
        public void UrlObjectConverter_ProducesExpectedRawUrl()
        { 
            UrlObjectConverter converter = new UrlObjectConverter(new DefaultValueFactory());
            PostmanUrl result = converter.Convert(_validPath, _validParameters, _validHost, _validBasePath);
            Assert.Equal("http://mysite.com/api/Values/postFormDataEndpoint/:id?filter={{filter}}&page={{page}}", result.Raw);
        }

        [Theory]
        [InlineData("mysite.com", "", "verb/action", "http://mysite.com/verb/action")]
        [InlineData("mysite.com", null, "verb/action", "http://mysite.com/verb/action")]
        [InlineData("mysite.com", "", "/verb/action", "http://mysite.com/verb/action")]
        [InlineData("mysite.com", null, "verb/action/", "http://mysite.com/verb/action")]
        [InlineData("mysite.com", "apiroot", "verb/action", "http://mysite.com/apiroot/verb/action")]
        [InlineData("http://mysite.com", "apiroot", "verb/action", "http://mysite.com/apiroot/verb/action")]
        [InlineData("https://mysite.com", "apiroot", "verb/action", "https://mysite.com/apiroot/verb/action")]
        [InlineData("10.0.0.1", "apiroot", "verb/action", "http://10.0.0.1/apiroot/verb/action")]
        [InlineData("https://10.0.0.1", "apiroot", "verb/action", "https://10.0.0.1/apiroot/verb/action")]
        [InlineData("http://mysite.com", "/apiroot", "/verb/action", "http://mysite.com/apiroot/verb/action")]
        [InlineData("http://mysite.com", "apiroot", "verb/action/", "http://mysite.com/apiroot/verb/action")]
        [InlineData("http://mysite.com", "apiroot", "/verb/action", "http://mysite.com/apiroot/verb/action")]
        [InlineData("http://mysite.com/", "apiroot", "verb/action", "http://mysite.com/apiroot/verb/action")]
        [InlineData("localhost:24724", "apiroot", "verb/action", "http://localhost:24724/apiroot/verb/action")]
        [InlineData("mysite.com:24724", "apiroot", "verb/action", "http://mysite.com:24724/apiroot/verb/action")]
        [InlineData("192.168.0.1:24724", "apiroot", "verb/action", "http://192.168.0.1:24724/apiroot/verb/action")]
        [InlineData("", "", "verb/action", "/verb/action")]
        [InlineData(null, "", "verb/action", "/verb/action")]
        [InlineData(null, null, "verb/action", "/verb/action")]
        [InlineData(null, null, "verb/action/", "/verb/action")]
        [InlineData(null, null, "/verb/action/", "/verb/action")]
        [InlineData(null, null, "/verb/action", "/verb/action")]
        public void UrlObjectConverter_ProducesExpectedRawUrlFromHostBasePathAndPath(string host, string basePath, string path, string expectedRawUrl)
        {
            UrlObjectConverter converter = new UrlObjectConverter(new DefaultValueFactory());
            PostmanUrl result = converter.Convert(path, new List<IParameter>(), host, basePath);
            Assert.Equal(expectedRawUrl, result.Raw);
        }

        [Theory]
        [InlineData("mysite.com", "mysite.com")]
        [InlineData("http://mysite.com", "mysite.com")]
        [InlineData("https://mysite.com", "mysite.com")]
        [InlineData("http://mysite.com:2121", "mysite.com")]
        [InlineData("https://mysite.com:2121", "mysite.com")]
        [InlineData("https://localhost:8080", "localhost")]
        [InlineData("https://127.0.0.1:8080", "127.0.0.1")]
        public void UrlObjectConverter_ProducesExpectedHost(string host, string expectedhost)
        {
            UrlObjectConverter converter = new UrlObjectConverter(new DefaultValueFactory());
            PostmanUrl result = converter.Convert(_validPath, _validParameters, host, _validPath);
            Assert.Equal(expectedhost, result.Host);
        }

        [Fact]
        public void UrlObjectConverter_ProducesExpectedPath()
        {
            UrlObjectConverter converter = new UrlObjectConverter(new DefaultValueFactory());
            PostmanUrl result = converter.Convert(_validPath, _validParameters, _validHost, _validBasePath);
            Assert.Equal(new string[] { "api", "Values", "postFormDataEndpoint", ":id" }, result.Path);
        }


        [Theory]
        [InlineData("http://mysite.com", "80")]
        [InlineData("https://mysite.com", "443")]
        [InlineData("http://mysite.com:2121", "2121")]
        [InlineData("https://mysite.com:2121", "2121")]
        public void UrlObjectConverter_ProducesExpectedPort(string host, string expectedPort)
        {
            UrlObjectConverter converter = new UrlObjectConverter(new DefaultValueFactory());
            PostmanUrl result = converter.Convert(_validPath, _validParameters, host, _validBasePath);
            Assert.Equal(expectedPort, result.Port);
        }

        [Theory]
        [InlineData("http://mysite.com", "http")]
        [InlineData("https://mysite.com", "https")]
        [InlineData("http://mysite.com:2121", "http")]
        [InlineData("https://mysite.com:2121", "https")]
        public void UrlObjectConverter_ProducesExpectedProtocol(string host, string expectedProtocol)
        {
            UrlObjectConverter converter = new UrlObjectConverter(new DefaultValueFactory());
            PostmanUrl result = converter.Convert(_validPath, _validParameters, host, _validBasePath);
            Assert.Equal(expectedProtocol, result.Protocol);
        }

        [Theory]
        [InlineData("", "/controllername/actionname", new string[] { "controllername", "actionname" })]
        [InlineData("/", "/controllername/actionname", new string[] { "controllername", "actionname" })]
        [InlineData("/basepath", "/controllername/actionname", new string[] { "basepath","controllername", "actionname" })]
        [InlineData("basepath", "/controllername/actionname", new string[] { "basepath", "controllername", "actionname" })]
        [InlineData("/basepath", "controllername/actionname", new string[] { "basepath", "controllername", "actionname" })]
        [InlineData("basepath", "controllername/actionname", new string[] { "basepath", "controllername", "actionname" })]
        [InlineData("/basepath/", "/controllername/actionname/", new string[] { "basepath", "controllername", "actionname" })]
        public void UrlObjectConverter_ProducesExpectedPathSegments(string basepath, string path, string[] expectedSegments)
        {
            UrlObjectConverter converter = new UrlObjectConverter(new DefaultValueFactory());
            PostmanUrl result = converter.Convert(path, new List<IParameter>(), _validHost, basepath);
            Assert.Equal(expectedSegments, result.Path);
        }

        [Fact]
        public void UrlObjectConverter_ProducesTheSameNumberOfQueryParamsAsInputParmsOfQueryType(){

            int expectedTotal = _validParameters.Count(p => p.In == SwashbuckleParameterTypeConstants.Query);

            UrlObjectConverter converter = new UrlObjectConverter(new DefaultValueFactory());
            PostmanUrl result = converter.Convert(_validPath, _validParameters, _validHost, _validBasePath);
            Assert.Equal(expectedTotal, result.QueryParams.Count);
        }

        [Fact]
        public void UrlObjectConverter_ProducesCorrectQueryParameters()
        {

            int expectedTotal = _validParameters.Count(p => p.In == SwashbuckleParameterTypeConstants.Query);

            UrlObjectConverter converter = new UrlObjectConverter(new DefaultValueFactory());
            PostmanUrl result = converter.Convert(_validPath, _validParameters,_validHost, _validBasePath);
            foreach(PostmanQueryParam resultParam in result.QueryParams)
            {
                Assert.NotNull(resultParam.Key);
                Assert.NotNull(resultParam.Value);
                Assert.NotNull(resultParam.Description);
                var sourceParam = _validParameters.First(x => x.Name == resultParam.Key);
                Assert.Equal(sourceParam.Name, resultParam.Key);
                Assert.Equal(resultParam.Description.Content, sourceParam.Description);
            }

        }

        [Fact]
        public void UrlObjectConverter_ProducesTheSameNumberOfVariablesAsUrlParameters()
        {
            int expectedTotal = _validParameters.Count(p => p.In == SwashbuckleParameterTypeConstants.Query || p.In == SwashbuckleParameterTypeConstants.Path);
            UrlObjectConverter converter = new UrlObjectConverter(new DefaultValueFactory());
            PostmanUrl result = converter.Convert(_validPath, _validParameters, _validHost, _validBasePath);
            Assert.Equal(expectedTotal, result.Variables.Count);
        }



    }
}
