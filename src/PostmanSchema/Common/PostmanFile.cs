using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Common
{
    [DataContract]
    public class PostmanFile
    {
        [DataMember(Name = "src")]
        public string Src { get; set; }
        [DataMember(Name = "Content")]
        public string Content { get; set; }
    }
}