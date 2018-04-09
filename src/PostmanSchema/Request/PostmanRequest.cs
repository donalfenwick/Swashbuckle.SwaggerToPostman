using Swashbuckle.SwaggerToPostman.PostmanSchema.Auth;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Request
{
    [DataContract]
    public class PostmanRequest
    {
        [DataMember(Name = "url")]
        public PostmanUrl Url { get; set; }
        [DataMember(Name = "auth")]
        public PostmanAuth Auth { get; set; }
        [DataMember(Name = "proxy")]
        public PostmanProxyConfig Proxy { get; set; }
        [DataMember(Name = "certificate")]
        public PostmanCertificate Certificate { get; set; }
        [DataMember(Name = "method")]
        public PostmanHttpMethod Method { get; set; }
        [DataMember(Name = "description")]
        public PostmanDescription Description { get; set; }
        [DataMember(Name = "header")]
        public List<PostmanHeader> Headers { get; set; }
        [DataMember(Name = "body")]
        public PostmanRequestBody Body { get; set; }
    }
}