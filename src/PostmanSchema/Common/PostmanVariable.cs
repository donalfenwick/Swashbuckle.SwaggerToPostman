using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Common
{
    [DataContract]
    public class PostmanVariable
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "key")]
        public string Key { get; set; }
        [DataMember(Name = "value")]
        public string Value { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "description")]
        public PostmanDescription Description { get; set; }
        [DataMember(Name = "system")]
        public bool System { get; set; }
        [DataMember(Name = "disabled")]
        public bool Disabled { get; set; }
    }
}
