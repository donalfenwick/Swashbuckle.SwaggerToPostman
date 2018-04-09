using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApp.Controllers
{
    /// <summary>
    /// Test values controller to provide endpoints with various 
    /// parameter types within the swagger doc
    /// </summary>
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // simple crud actions

        [HttpGet("getEndnpoint/{id}")]
        [ProducesResponseType(200, Type = typeof(ComplexType))]
        public IActionResult GetComplextypeEndpoint([FromRoute]int id, [FromQuery]string filter = null)
        {
            return Ok(new ComplexType());
        }

        [HttpPost("createEndnpoint")]
        [ProducesResponseType(200, Type = typeof(ComplexType))]
        public IActionResult CreateComplextypeEndpoint([FromBody]ComplexType value)
        {
            return Ok(value);
        }

        [HttpPut("putEndnpoint/{id}")]
        [ProducesResponseType(200, Type = typeof(ComplexType))]
        public IActionResult PutComplextypeEndpoint([FromRoute]int id, [FromBody]ComplexType value)
        {
            return Ok(value);
        }

        [HttpDelete("deleteEndnpoint/{id}")]
        public IActionResult DeleteComplextypeEndpoint([FromRoute]int id)
        {
            return Ok();
        }

        // binding to header

        [HttpGet("getWithHeaderEndpoint/{id}")]
        [ProducesResponseType(200, Type = typeof(ComplexType))]
        public IActionResult GetComplextypeWithHeaderEndpoint([FromHeader(Name = "x-CustomHeader")]string headerValue, [FromRoute]int id, [FromQuery]string filter = null)
        {
            return Ok(new ComplexType());
        }

        // binding post body to simple types like string/int instead of an object

        [HttpPost("setStringBodyEndpoint")]
        [ProducesResponseType(200, Type = typeof(ComplexType))]
        public IActionResult SetStringBody([FromBody]string bodyInput)
        {
            return Ok(new ComplexType());
        }

        [HttpPost("setIntBodyEndpoint")]
        [ProducesResponseType(200, Type = typeof(ComplexType))]
        public IActionResult SetIntBody([FromBody]int bodyInput)
        {
            return Ok(new ComplexType());
        }

        // binding body to collections

        [HttpPost("setStringArrayBodyEndpoint")]
        [ProducesResponseType(200, Type = typeof(ComplexType))]
        public IActionResult SetStringArrayBody([FromBody]string[] bodyInput)
        {
            return Ok(new ComplexType());
        }

        [HttpPost("SetIntArrayBodyEndpoint")]
        [ProducesResponseType(200, Type = typeof(ComplexType))]
        public IActionResult SetIntArrayBody([FromBody]int[] bodyInput)
        {
            return Ok(new ComplexType());
        }

        [HttpPost("SetComplexTypeArrayBodyEndpoint")]
        [ProducesResponseType(200, Type = typeof(ComplexType[]))]
        public IActionResult SetIntArrayBody([FromBody]ComplexType[] bodyInput)
        {
            return Ok(bodyInput);
        }

        [HttpPost("SetComplexTypeListBodyEndpoint")]
        [ProducesResponseType(200, Type = typeof(List<ComplexType>))]
        public IActionResult SetIntArrayBody([FromBody]List<ComplexType> bodyInput)
        {
            return Ok(bodyInput);
        }

        // binding form parameters in a post request

        [HttpPost("postFormDataEndpoint")]
        [ProducesResponseType(200, Type = typeof(ComplexType))]
        public IActionResult PostFormDataEndpoint(
            [FromForm] string TestString,
            [FromForm]int TestInt,
            [FromForm]long TestLong,
            [FromForm]double TestDouble,
            [FromForm]DateTime TestDate
        )
        {
            ComplexType result = new ComplexType()
            {
                TestString = TestString,
                TestInt = TestInt,
                TestLong = TestLong,
                TestDouble = TestDouble,
                TestDate = TestDate
            };
            return Ok(new ComplexType());
        }

    }

    // test types

    public class ComplexType
    {
        public string TestString { get; set; }
        public int TestInt { get; set; }
        public long TestLong { get; set; }
        public double TestDouble { get; set; }
        public DateTime TestDate { get; set; }
        public List<int> IntArray { get; set; }
        public List<string> StringArray { get; set; }
        public NestedComplexType NestedType { get; set; }
        public List<NestedComplexType> ListOfComplexType { get; set; }
    }

    public class NestedComplexType
    {
        public string TestString { get; set; }
        public int TestInt { get; set; }
        public long TestLong { get; set; }
        public double TestDouble { get; set; }
        public DateTime TestDate { get; set; }
        public List<int> IntArray { get; set; }
        public List<string> StringArray { get; set; }
    }
}
