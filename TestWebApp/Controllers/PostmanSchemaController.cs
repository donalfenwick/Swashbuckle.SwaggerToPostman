using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.SwaggerToPostman.Converters;
using Swashbuckle.SwaggerToPostman.PostmanSchema;

namespace TestWebApp.Controllers
{
    /// <summary>
    /// Example of how to obtain and expose the doc directly from a 
    /// controller instead of from the middleware
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    [Produces("application/json")]
    [Route("api/PostmanSchema")]
    public class PostmanSchemaController : Controller
    {
        private readonly ISwaggerToPostmanConverter postmanConverter;

        public PostmanSchemaController(ISwaggerToPostmanConverter postmanConverter)
        {
            this.postmanConverter = postmanConverter;
        }

        // GET: api/PostmanSchema
        [HttpGet]
        public IActionResult Get()
        {
            string swaggerDocName = "v1";
            PostmanRootCollection collection = postmanConverter.GetPostmanCollection(swaggerDocName, "localhost", "http://localhost:24724/");

            JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
            jsonSettings.NullValueHandling = NullValueHandling.Ignore;
            jsonSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            return Json(collection, jsonSettings);
        }

       

    }
}
