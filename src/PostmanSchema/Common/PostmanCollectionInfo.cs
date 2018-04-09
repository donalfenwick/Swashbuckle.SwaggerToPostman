using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema.Common
{
    [DataContract]
    public class PostmanCollectionInfo
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "_postman_id")]
        public string PostmanId { get; set; }
        [DataMember(Name = "description")]
        public PostmanDescription Description { get; set; }
        [DataMember(Name = "version")]
        public string Version { get; set; }
        [DataMember(Name = "schema")]
        public string Schema { get; set; }
    }
}