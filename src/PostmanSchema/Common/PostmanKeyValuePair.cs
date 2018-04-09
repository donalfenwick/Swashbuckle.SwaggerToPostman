using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Common
{
    [DataContract]
    public class PostmanKeyValuePair
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
        [DataMember(Name = "value")]
        public string Value { get; set; }
        [DataMember(Name = "disabled")]
        public bool Disabled { get; set; }
        [DataMember(Name = "description")]
        public PostmanDescription Description { get; set; }
    }

    public class PostmanTypedKeyValuePair : PostmanKeyValuePair{

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
