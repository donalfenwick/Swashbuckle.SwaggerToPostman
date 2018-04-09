using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Common
{
    [DataContract]
    public class PostmanScript
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "exec")]
        public string Exec { get; set; }
        [DataMember(Name = "src")]
        public string Src { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
