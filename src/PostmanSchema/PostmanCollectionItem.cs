using Swashbuckle.SwaggerToPostman.PostmanSchema.Request;
using Swashbuckle.SwaggerToPostman.PostmanSchema.Response;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Swashbuckle.SwaggerToPostman.PostmanSchema
{
    public class PostmanCollectionItem : CollectionItemBase
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "request")]
        public PostmanRequest Request { get; set; }

        [DataMember(Name = "response")]
        public List<PostmanResponse> Responses { get; set; } = new List<PostmanResponse>();

    }
}
