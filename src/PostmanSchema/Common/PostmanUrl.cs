using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Common
{
    [DataContract]
    public class PostmanUrl
    {
        /// <summary>
        /// The string representation of the request URL, including the protocol, host, path, hash, query parameter(s) and path variable(s).
        /// </summary>
        [DataMember(Name = "raw")]
        public string Raw { get; set; }
        /// <summary>
        /// The protocol associated with the request, E.g: 'http'
        /// </summary>
        [DataMember(Name = "protocol")]
        public string Protocol { get; set; }
        /// <summary>
        /// The host for the URL, E.g: api.yourdomain.com. Can be stored as a string or as an array of strings.
        /// </summary>
        [DataMember(Name = "host")]
        public string Host { get; set; }
        /// <summary>
        /// The complete path of the current url, broken down into segments. A segment could be a string, or a path variable.
        /// </summary>
        [DataMember(Name = "path")]
        public string[] Path { get; set; }
        /// <summary>
        /// The port number present in this URL. An empty value implies 80/443 depending on whether the protocol field contains http/https.
        /// </summary>
        [DataMember(Name = "port")]
        public string Port { get; set; }
        /// <summary>
        /// An array of QueryParams, which is basically the query string part of the URL, parsed into separate variables
        /// </summary>
        [DataMember(Name = "query")]
        public List<PostmanQueryParam> QueryParams { get; set; } = new List<PostmanQueryParam>();
        /// <summary>
        /// Contains the URL fragment (if any). Usually this is not transmitted over the network, but it could be useful to store this in some cases.
        /// </summary>
        [DataMember(Name = "hash")]
        public string Hash { get; set; }
        /// <summary>
        /// Postman supports path variables with the syntax /path/:variableName/to/somewhere. These variables are stored in this field.
        /// </summary>
        [DataMember(Name = "variable")]
        public List<PostmanVariable> Variables { get; set; } = new List<PostmanVariable>();
    }
}
