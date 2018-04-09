using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Common
{
    [DataContract]
    public class PostmanEvent
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "listen")]
        public PostmanCollectionEventListenType Listen { get; set; }
        [DataMember(Name = "script")]
        public PostmanScript Script { get; set; }
        [DataMember(Name = "disabled")]
        public bool  Disabled { get; set; }
    }

    [DataContract]
    public enum PostmanCollectionEventListenType
    {
        [EnumMember] test,
        [EnumMember] perrequest
    }
}
