using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Common
{
    [DataContract]
    public class PostmanDescription
    {
        public PostmanDescription() { }
        public PostmanDescription(string content) { this.Content = content; }

        [DataMember(Name = "content")]
        public string Content { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "version")]
        public string Version { get; set; }
    }
}
