using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman.Converters;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.Converters
{
    [Trait("Category", "ConverterTests")]
    public class HeaderParameterObjectConverterTests
    {

        [Fact]
        public void HeaderParameterObjectConverter_ProducesResultsFromOnlyHeaderTypes()
        {
            List<NonBodyParameter> input = new List<NonBodyParameter>()
            {
                new NonBodyParameter{ In = SwashbuckleParameterTypeConstants.Header, Name = "header1" },
                new NonBodyParameter{ In = SwashbuckleParameterTypeConstants.Header, Name = "header2" },
                new NonBodyParameter{ In = SwashbuckleParameterTypeConstants.Query, Name = "queryParam" },
                new NonBodyParameter{ In = SwashbuckleParameterTypeConstants.Path, Name = "pathParam" },
            };
            HeaderParameterObjectConverter converter = new HeaderParameterObjectConverter();
            List<PostmanHeader> result = converter.Convert(input);

            Assert.Equal(1, result.Count(x => x.Key == "header1"));
            Assert.Equal(1, result.Count(x => x.Key == "header2"));
            Assert.Equal(0, result.Count(x => x.Key == "queryParam"));
            Assert.Equal(0, result.Count(x => x.Key == "pathParam"));
        }

        [Fact]
        public void HeaderParameterObjectConverter_ResultsContainValidContentTypeHeader()
        {
            List<NonBodyParameter> input = new List<NonBodyParameter>()
            {
                new NonBodyParameter{ In = SwashbuckleParameterTypeConstants.Header, Name = "header1" },
                new NonBodyParameter{ In = SwashbuckleParameterTypeConstants.Header, Name = "header2" },
                new NonBodyParameter{ In = SwashbuckleParameterTypeConstants.Query, Name = "queryParam" },
                new NonBodyParameter{ In = SwashbuckleParameterTypeConstants.Path, Name = "pathParam" },
            };
            HeaderParameterObjectConverter converter = new HeaderParameterObjectConverter();
            List<PostmanHeader> result = converter.Convert(input);
            PostmanHeader contentTypeHeader = result.FirstOrDefault(x => x.Key == "Content-Type");

            Assert.NotNull(contentTypeHeader);
            Assert.NotNull(contentTypeHeader.Value);
        }

        [Fact]
        public void HeaderParameterObjectConverter_ResultsContainJsonContentTypeHeader_WithNoFormDataParams()
        {
            List<NonBodyParameter> input = new List<NonBodyParameter>();
            HeaderParameterObjectConverter converter = new HeaderParameterObjectConverter();
            List<PostmanHeader> result = converter.Convert(input);
            PostmanHeader contentTypeHeader = result.FirstOrDefault(x => x.Key == "Content-Type");
            
            Assert.Equal("application/json", contentTypeHeader.Value);
        }

        [Fact]
        public void HeaderParameterObjectConverter_ResultsContainJsonContentTypeHeader_WithOneOrMoreFormDataParams()
        {
            List<NonBodyParameter> input = new List<NonBodyParameter>()
            {
                new NonBodyParameter{ In = SwashbuckleParameterTypeConstants.FormData, Name = "formDataParam1" },
            };
            HeaderParameterObjectConverter converter = new HeaderParameterObjectConverter();
            List<PostmanHeader> result = converter.Convert(input);
            PostmanHeader contentTypeHeader = result.FirstOrDefault(x => x.Key == "Content-Type");

            Assert.Equal("application/x-www-form-urlencoded", contentTypeHeader.Value);
        }
    }
}
