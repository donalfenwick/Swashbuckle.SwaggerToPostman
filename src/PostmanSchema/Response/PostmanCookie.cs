using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Response
{
    [DataContract]
    public class PostmanCookie
    {
        [DataMember(Name = "domain")]
        public string Domain{ get; set; }
        [DataMember(Name = "expires")]
        public string Expires { get; set; }
        [DataMember(Name = "maxAge")]
        public string MaxAge { get; set; }
        [DataMember(Name = "hostOnly")]
        public bool HostOnly { get; set; }
        [DataMember(Name = "httpOnly")]
        public bool HttpOnly { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "path")]
        public string Path { get; set; }
        [DataMember(Name = "secure")]
        public bool Secure { get; set; }
        [DataMember(Name = "session")]
        public bool Session { get; set; }
        [DataMember(Name = "value")]
        public string Value { get; set; }
        [DataMember(Name = "extensions")]
        public List<object> Extensions { get; set; }
    }
}