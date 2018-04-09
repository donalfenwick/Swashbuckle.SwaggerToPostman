using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.SwaggerToPostman.Converters;
using Swashbuckle.SwaggerToPostman.Converters.JsonBuilder;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Converters
{
    [Trait("Category", "ConverterTests")]
    public class RequestBodyJsonBuilderTests
    {
        
        [Theory]
        [InlineData("int32", "integer")]
        [InlineData("int64", "integer")]
        [InlineData("float", "number")]
        [InlineData("double", "number")]
        [InlineData("bool", "boolean")]
        [InlineData("string", "string")]
        [InlineData("string", "date-time")]
        [InlineData("string", "uuid")]
        [InlineData("string", "byte")]
        public void RequestBodyJsonBuilder_PrimitiveTypeInput_ProducesJValue(string schemaFormat, string schemaType)
        {
            Schema schema = CreateSchemaInputForPrimitiveType(name: "test", format: schemaFormat, type: schemaType);
            RequestBodyJsonBuilder converter = new RequestBodyJsonBuilder(new DefaultValueFactory());
            JToken result = converter.GetJsonResult(schema, swaggerDocDefinitions: new Dictionary<string, Schema>());

            Assert.IsType<JValue>(result);
        }

        [Theory]
        [InlineData("int32", "integer", JTokenType.Integer)]
        [InlineData("int64", "integer", JTokenType.Integer)]
        [InlineData("float", "number", JTokenType.Float)]
        [InlineData("double", "number", JTokenType.Float)]
        [InlineData("bool", "boolean", JTokenType.Boolean)]
        [InlineData("string", "string", JTokenType.String)]
        [InlineData("string", "date-time", JTokenType.String)]
        [InlineData("string", "uuid", JTokenType.String)]
        [InlineData("string", "byte", JTokenType.String)]
        public void RequestBodyJsonBuilder_PrimitiveTypeInput_ProducesExpectedJObjectType(string schemaFormat, string schemaType, JTokenType expectedTokenType)
        {
            Schema schema = CreateSchemaInputForPrimitiveType(name: "test", format: schemaFormat, type: schemaType);
            RequestBodyJsonBuilder converter = new RequestBodyJsonBuilder(new DefaultValueFactory());
            JToken result = converter.GetJsonResult(schema, swaggerDocDefinitions: new Dictionary<string, Schema>());
            JValue jsonValue = (JValue)result;

            Assert.Equal(expectedTokenType, jsonValue.Type);
        }

        [Theory]
        [InlineData("int32", "integer", typeof(int))]
        [InlineData("int64", "integer", typeof(long))]
        [InlineData("float", "number", typeof(float))]
        [InlineData("double", "number", typeof(double))]
        [InlineData("bool", "boolean", typeof(bool))]
        [InlineData("string", "string", typeof(string))]
        [InlineData("string", "date-time", typeof(string))]
        [InlineData("string", "uuid", typeof(string))]
        [InlineData("string", "byte", typeof(string))]
        public void RequestBodyJsonBuilder_PrimitiveTypeInput_ProducesExpectedJObjectResultValue(string schemaFormat, string schemaType, Type expectedDataType)
        {
            Schema schema = CreateSchemaInputForPrimitiveType(name: "test", format: schemaFormat, type: schemaType);
            RequestBodyJsonBuilder converter = new RequestBodyJsonBuilder(new DefaultValueFactory());
            JToken result = converter.GetJsonResult(schema, swaggerDocDefinitions: new Dictionary<string, Schema>());

            Assert.IsType<JValue>(result);
            JValue jsonValue = (JValue)result;
            Assert.IsType(expectedDataType, jsonValue.Value);
        }

        [Theory]
        [InlineData("int32", "integer")]
        [InlineData("int64", "integer")]
        [InlineData("float", "number")]
        [InlineData("double", "number")]
        [InlineData("bool", "boolean")]
        [InlineData("string", "string")]
        [InlineData("string", "date-time")]
        [InlineData("string", "uuid")]
        [InlineData("string", "byte")]
        public void RequestBodyJsonBuilder_ArrayTypeInput_ProducesJValue(string itemSchemaFormat, string itemSchemaType)
        {
            Schema schema = CreateSchemaInputForArrayType(name: "test", itemFormat: itemSchemaFormat, itemType: itemSchemaType);
            RequestBodyJsonBuilder converter = new RequestBodyJsonBuilder(new DefaultValueFactory());
            JToken result = converter.GetJsonResult(schema, swaggerDocDefinitions: new Dictionary<string, Schema>());

            Assert.IsType<JArray>(result);
        }

        [Theory]
        [InlineData("int32", "integer")]
        [InlineData("int64", "integer")]
        [InlineData("float", "number")]
        [InlineData("double", "number")]
        [InlineData("bool", "boolean")]
        [InlineData("string", "string")]
        [InlineData("string", "date-time")]
        [InlineData("string", "uuid")]
        [InlineData("string", "byte")]
        public void RequestBodyJsonBuilder_ArrayOfPrimitivesTypeInput_ProducesJArrayOfJValue(string itemSchemaFormat, string itemSchemaType)
        {
            Schema schema = CreateSchemaInputForArrayType(name: "test", itemFormat: itemSchemaFormat, itemType: itemSchemaType);
            RequestBodyJsonBuilder converter = new RequestBodyJsonBuilder(new DefaultValueFactory());
            JToken result = converter.GetJsonResult(schema, swaggerDocDefinitions: new Dictionary<string, Schema>());

            JArray array = (JArray)result;
            Assert.All(array, i => Assert.IsType<JValue>(i));
        }

        [Theory]
        [InlineData("int32", "integer", JTokenType.Integer)]
        [InlineData("int64", "integer", JTokenType.Integer)]
        [InlineData("float", "number", JTokenType.Float)]
        [InlineData("double", "number", JTokenType.Float)]
        [InlineData("bool", "boolean", JTokenType.Boolean)]
        [InlineData("string", "string", JTokenType.String)]
        [InlineData("string", "date-time", JTokenType.String)]
        [InlineData("string", "uuid", JTokenType.String)]
        [InlineData("string", "byte", JTokenType.String)]
        public void RequestBodyJsonBuilder_ArrayTypeInput_ProducesExpectedJObjectType(string itemSchemaFormat, string itemSchemaType, JTokenType expectedItemTokenType)
        {
            Schema schema = CreateSchemaInputForArrayType(name: "test", itemFormat: itemSchemaFormat, itemType: itemSchemaType);
            RequestBodyJsonBuilder converter = new RequestBodyJsonBuilder(new DefaultValueFactory());
            JToken result = converter.GetJsonResult(schema, swaggerDocDefinitions: new Dictionary<string, Schema>());
            
            JArray array = (JArray)result;
            Assert.All(array, i => Assert.Equal(expectedItemTokenType, (i as JValue).Type));
        }

        [Theory]
        [InlineData("int32", "integer", typeof(int))]
        [InlineData("int64", "integer", typeof(long))]
        [InlineData("float", "number", typeof(float))]
        [InlineData("double", "number", typeof(double))]
        [InlineData("bool", "boolean", typeof(bool))]
        [InlineData("string", "string", typeof(string))]
        [InlineData("string", "date-time", typeof(string))]
        [InlineData("string", "uuid", typeof(string))]
        [InlineData("string", "byte", typeof(string))]
        public void RequestBodyJsonBuilder_ArrayTypeInput_ProducesExpectedJObjectResultValue(string itemSchemaFormat, string itemSchemaType, Type expectedItemDataType)
        {
            Schema schema = CreateSchemaInputForArrayType(name: "test", itemFormat: itemSchemaFormat, itemType: itemSchemaType);
            RequestBodyJsonBuilder converter = new RequestBodyJsonBuilder(new DefaultValueFactory());
            JToken result = converter.GetJsonResult(schema, swaggerDocDefinitions: new Dictionary<string, Schema>());


            JArray array = (JArray)result;
            Assert.All(array, i => Assert.IsType(expectedItemDataType, (i as JValue).Value));
        }

        [Fact]
        public void RequestBodyJsonBuilder_ComplexTypeInput_ProducesJObject()
        {
            Schema schema = CreateSchemaInputForComplexType(name: "test");
            RequestBodyJsonBuilder converter = new RequestBodyJsonBuilder(new DefaultValueFactory());
            JToken result = converter.GetJsonResult(schema, swaggerDocDefinitions: new Dictionary<string, Schema>());

            Assert.IsType<JObject>(result);
        }

        [Fact]
        public void RequestBodyJsonBuilder_ComplexTypeInput_ProducesJObjectWithCorrectProperties()
        {
            Schema schema = CreateSchemaInputForComplexType(name: "test");
            RequestBodyJsonBuilder converter = new RequestBodyJsonBuilder(new DefaultValueFactory());
            JObject result = (JObject)converter.GetJsonResult(schema, swaggerDocDefinitions: new Dictionary<string, Schema>());

            Assert.NotNull(result["id"]);
            Assert.IsType<JValue>(result["id"]);
            Assert.NotNull(result["name"]);
            Assert.IsType<JValue>(result["name"]);
        }

        [Fact]
        public void RequestBodyJsonBuilder_ComplexTypeInput_ProducesJObjectWithCorrectPropertyTypes()
        {
            Schema schema = CreateSchemaInputForComplexType(name: "test");
            RequestBodyJsonBuilder converter = new RequestBodyJsonBuilder(new DefaultValueFactory());
            JObject result = (JObject)converter.GetJsonResult(schema, swaggerDocDefinitions: new Dictionary<string, Schema>());
            JValue idElement = result["id"] as JValue;
            JValue nameElement = result["name"] as JValue;

            Assert.IsType<int>(idElement.Value);
            Assert.IsType<string>(nameElement.Value);
        }
        
        [Fact]
        public void RequestBodyJsonBuilder_ComplexTypeInput_ProducesJObjectWithCorrectPropertyTokenTypes()
        {
            Schema schema = CreateSchemaInputForComplexType(name: "test");
            RequestBodyJsonBuilder converter = new RequestBodyJsonBuilder(new DefaultValueFactory());
            JObject result = (JObject)converter.GetJsonResult(schema, swaggerDocDefinitions: new Dictionary<string, Schema>());
            JValue idElement = result["id"] as JValue;
            JValue nameElement = result["name"] as JValue;

            Assert.Equal(JTokenType.Integer,idElement.Type);
            Assert.Equal(JTokenType.String, nameElement.Type);
        }

        [Fact]
        public void RequestBodyJsonBuilder_ComplexTypeWithNestedTypeInput_ProducesJObject()
        {
            var inputResult = CreateSchemaInputForComplexTypeWithNestedTypes();
            RequestBodyJsonBuilder converter = new RequestBodyJsonBuilder(new DefaultValueFactory());
            JToken result = converter.GetJsonResult(inputResult.schema, swaggerDocDefinitions: inputResult.references);

            Assert.IsType<JObject>(result);
        }

        [Fact]
        public void RequestBodyJsonBuilder_ComplexTypeWithNestedTypeInput_ProducesJObjectForNestedChild()
        {
            var inputResult = CreateSchemaInputForComplexTypeWithNestedTypes();
            RequestBodyJsonBuilder converter = new RequestBodyJsonBuilder(new DefaultValueFactory());
            JObject result =(JObject) converter.GetJsonResult(inputResult.schema, swaggerDocDefinitions: inputResult.references);

            Assert.IsType<JObject>(result["nestedChild"]);
        }

        [Fact]
        public void RequestBodyJsonBuilder_ComplexTypeWithNestedTypeInput_ProducesJObjectForNestedChild_WithCorrectProperties()
        {
            var inputResult = CreateSchemaInputForComplexTypeWithNestedTypes();
            RequestBodyJsonBuilder converter = new RequestBodyJsonBuilder(new DefaultValueFactory());
            JObject result = (JObject)converter.GetJsonResult(inputResult.schema, swaggerDocDefinitions: inputResult.references);

            JObject nestedChildObject = result["nestedChild"] as JObject;

            Assert.NotNull(nestedChildObject);
            Assert.NotNull(nestedChildObject["uuid"]);
            Assert.IsType<JValue>(nestedChildObject["uuid"]);
            Assert.NotNull(nestedChildObject["code"]);
            Assert.IsType<JValue>(nestedChildObject["code"]);
        }

        [Fact]
        public void RequestBodyJsonBuilder_ComplexTypeWithNestedTypeInput_ProducesJObjectForNestedChild_WithCorrectPropertyTypes()
        {
            var inputResult = CreateSchemaInputForComplexTypeWithNestedTypes();
            RequestBodyJsonBuilder converter = new RequestBodyJsonBuilder(new DefaultValueFactory());
            JObject result = (JObject)converter.GetJsonResult(inputResult.schema, swaggerDocDefinitions: inputResult.references);

            JObject nestedChildObject = result["nestedChild"] as JObject;
            object uuidValue = (nestedChildObject["uuid"] as JValue).Value;
            object codeValue = (nestedChildObject["code"] as JValue).Value;

            Assert.IsType<string>(uuidValue);
            Assert.IsType<string>(codeValue);
        }





        // utility methods for creating test data... 

        /// <summary>
        /// creates a Schema represention of a primitive type like int, bool or string
        /// </summary>
        private Schema CreateSchemaInputForPrimitiveType(string name, string format, string type)
        {
            Schema schema = new Schema
            {
                Format = format,
                Type = type,
                Title = name
            };
            return schema;
        }

        /// <summary>
        /// creates a Schema represention for an array of primitive types
        /// </summary>
        private Schema CreateSchemaInputForArrayType(string name, string itemFormat, string itemType)
        {
            Schema schema = new Schema
            {
                Format = "array",
                Type = "array",
                Title = name,
                Items = new Schema
                {
                    Format = itemFormat,
                    Type = itemType,
                }
            };
            return schema;
        }

        /// <summary>
        /// Creates a schema representation for a complex type
        /// { id: 123, name: 'string value' }
        /// </summary>
        private Schema CreateSchemaInputForComplexType(string name)
        {
            Schema schema = new Schema
            {
                Format = "TestComplexType",
                Type = "object",
                Title = name,
                Properties = new Dictionary<string, Schema>()
                {
                    { "id", new Schema { Title ="id", Format = "int32", Type = "integer" } },
                    { "name", new Schema { Title ="name", Format = "string", Type = "string" } }
                }
            };
            return schema;
        }

        /// <summary>
        /// Creates a schema representation for a complex type with another nested complex type
        /// { 
        ///     id: 123, 
        ///     name: 'string value',
        ///     nestedChild: {
        ///         uuid: 'string value',
        ///         code: 'string value'
        ///     }
        /// }
        /// </summary>
        private (Schema schema, IDictionary<string, Schema> references) CreateSchemaInputForComplexTypeWithNestedTypes()
        {
            string childComplexTypeName = "ChildComplexType";
            Schema referencedComplexTypeSchema = new Schema
            {
                Format = childComplexTypeName,
                Type = "object",
                Title = childComplexTypeName,
                Properties = new Dictionary<string, Schema>()
                {
                    { "uuid", new Schema { Title ="uuid", Format = "string", Type = "string" } },
                    { "code", new Schema { Title ="code", Format = "string", Type = "string" } }
                }
            };

            Dictionary<string, Schema> references = new Dictionary<string, Schema>()
            {
                { childComplexTypeName, referencedComplexTypeSchema }
            };

            Schema schema = new Schema
            {
                Format = "TestComplexType",
                Type = "object",
                Title = childComplexTypeName,
                Properties = new Dictionary<string, Schema>()
                {
                    { "id", new Schema { Title ="id", Format = "int32", Type = "integer" } },
                    { "name", new Schema { Title ="name", Format = "string", Type = "string" } },
                    // create a reference to the child
                    { "nestedChild", new Schema
                        {
                            Format = childComplexTypeName,
                            Type =  null,// type should be null when adding a reference
                            Title = childComplexTypeName,
                            Ref = $"#/definitions/{childComplexTypeName}"
                        }
                    }
                }
            };
            return (schema, references);
        }

    }
}
