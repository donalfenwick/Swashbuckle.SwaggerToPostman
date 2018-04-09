using Swashbuckle.SwaggerToPostman.PostmanSchema.Request;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Response
{
    [DataContract]
    public class PostmanResponse
    {
        /// <summary>
        /// A unique, user defined identifier that can be used to refer to this response from requests.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "originalRequest")]
        public PostmanRequest OriginalRequest { get; set; }
        /// <summary>
        /// The time taken by the request to complete. If a number, the unit is milliseconds.
        /// </summary>
        [DataMember(Name = "responseTime")]
        public int responseTime { get; set; }
        /// <summary>
        /// No HTTP request is complete without its headers, and the same is true for a Postman request. This field is an array containing all the headers.
        /// </summary>
        [DataMember(Name = "header")]
        public List<string> Headers { get; set; }
        [DataMember(Name = "cookie")]
        public List<PostmanCookie> Cookie { get; set; }
        /// <summary>
        /// The raw text of the response
        /// </summary>
        [DataMember(Name = "body")]
        public string Body { get; set; }
        /// <summary>
        /// The response status, e.g: '200 OK'
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }
        /// <summary>
        /// The numerical response code, example: 200, 201, 404, etc.
        /// </summary>
        [DataMember(Name = "code")]
        public int Code { get; set; }
    }
}