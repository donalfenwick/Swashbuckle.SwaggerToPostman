using Swashbuckle.SwaggerToPostman.PostmanSchema.Common;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Request
{
    [DataContract]
    public class PostmanRequestBody
    {
        [DataMember(Name = "mode")]
        public PostmanRequestBodyMode? Mode { get; set; } = null;
        [DataMember(Name = "raw")]
        public string Raw { get; set; }
        [DataMember(Name = "urlencoded")]
        public List<PostmanKeyValuePair> UrlEncoded { get; set; }
        [DataMember(Name = "formdata")]
        public List<PostmanTypedKeyValuePair> FormData { get; set; }
        [DataMember(Name = "file")]
        public PostmanFile File { get; set; }
        
    }

    [DataContract]
    public enum PostmanRequestBodyMode
    {
        [EnumMember]raw,
        [EnumMember] urlencoded,
        [EnumMember] formdata,
        [EnumMember] file
    }
}